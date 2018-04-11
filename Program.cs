using System;
using System.Collections.Generic;
using System.Linq;
using Loyc;
using Loyc.Collections;
using Loyc.Ecs;
using Loyc.Syntax;
using Loyc.Syntax.Les;
using Pixie;
using Pixie.Markup;
using Pixie.Options;
using Pixie.Terminal;
using Pixie.Transforms;

namespace LeMP.Repl
{
    public static class Program
    {
        public static ILog Log { get; private set; }

        public static IMessageSink Sink { get; private set; }

        public static MacroProcessor Processor { get; private set; }

        public static IParsingService Parser { get; private set; }

        public static ILNodePrinter Printer { get; private set; }

        public static void Main(string[] args)
        {
            // Acquire a log for, well, logging purposes.
            var rawLog = TerminalLog.Acquire();

            Log = new TransformLog(
                rawLog,
                entry => DiagnosticExtractor.Transform(entry, new Text("LeMP-repl")));

            // TODO: create an IMessageSink that sends messages to the Pixie log.
            Sink = new SeverityMessageFilter(ConsoleMessageSink.Value, Loyc.Severity.NoteDetail);

            // Create an option parser.
            var optParser = new GnuOptionSetParser(
                Options.All,
                Options.Files,
                Options.Files.Forms[0]);

            // Parse command-line arguments.
            var parsedOptions = optParser.Parse(args, Log);

            // Optionally display help message.
            if (parsedOptions.GetValue<bool>(Options.Help))
            {
                rawLog.Log(
                    new HelpMessage(
                        "LeMP-repl is a simple tool that reads unprocessed EC#, " +
                        "LES v2 or LES v3 code as input and produces processed or " +
                        "unprocessed EC#, LES v2 or LES v3 code as output.",
                        "LeMP-repl [options]",
                        Options.All));
                return;
            }

            // Create a macro processor.
            if (!parsedOptions.GetValue<bool>(Options.DoNotProcessMacros))
            {
                Processor = new MacroProcessor(Sink);
            }

            Parser = GetParser(parsedOptions);
            Printer = GetPrinter(parsedOptions);

            // Start the REPL.
            RunRepl();
        }

        private static VList<LNode> Process(IReadOnlyList<LNode> nodes)
        {
            var vlist = new VList<LNode>(nodes);

            if (Processor == null)
            {
                return vlist;
            }
            else
            {
                return Processor.ProcessSynchronously(vlist);
            }
        }

        private static void RunRepl()
        {
            if (Console.IsInputRedirected)
            {
                Console.WriteLine(Print(Process(Parse(Console.In.ReadToEnd()))));
                return;
            }

            while (true)
            {
                Console.Error.Write("> ");
                var line = Console.ReadLine();
                if (line == null)
                {
                    Console.WriteLine();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                Console.WriteLine(Print(Process(Parse(line))));
            }
        }

        private static IReadOnlyList<LNode> Parse(string input)
        {
            return Parser.Parse(input, Sink);
        }

        private static string Print(VList<LNode> nodes)
        {
            return Printer.Print(nodes, Sink);
        }

        private static IParsingService GetParser(OptionSet options)
        {
            var lang = options.GetValue<string>(Options.InputLanguage);
            switch (lang.ToLowerInvariant())
            {
                case "ecs":
                    return EcsLanguageService.Value;
                case "les":
                    return LesLanguageService.Value;
                case "les2":
                    return Les2LanguageService.Value;
                case "les3":
                    return Les3LanguageService.Value;
                default:
                    Log.Log(
                        new LogEntry(
                            Pixie.Severity.Error,
                            "unknown input language",
                            "input language ",
                            Quotation.CreateBoldQuotation(lang),
                            " could not be matched to a known input language.",
                            new Paragraph("Option usage:"),
                            new Paragraph(
                                new OptionHelp(
                                    Options.InputLanguage,
                                    GnuOptionPrinter.Instance))));
                    return EcsLanguageService.Value;
            }
        }

        private static ILNodePrinter GetPrinter(OptionSet options)
        {
            var lang = options.GetValue<string>(Options.OutputLanguage);
            switch (lang.ToLowerInvariant())
            {
                case "cs":
                    return EcsLanguageService.WithPlainCSharpPrinter;
                case "ecs":
                    return EcsLanguageService.Value;
                case "les":
                    return LesLanguageService.Value;
                case "les2":
                    return Les2LanguageService.Value;
                case "les3":
                    return Les3LanguageService.Value;
                default:
                    Log.Log(
                        new LogEntry(
                            Pixie.Severity.Error,
                            "unknown input language",
                            "output language ",
                            Quotation.CreateBoldQuotation(lang),
                            " could not be matched to a known output language.",
                            new Paragraph("Option usage:"),
                            new Paragraph(
                                new OptionHelp(
                                    Options.OutputLanguage,
                                    GnuOptionPrinter.Instance))));
                    return Les3LanguageService.Value;
            }
        }
    }
}
