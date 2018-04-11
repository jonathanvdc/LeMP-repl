using System;
using Pixie;
using Pixie.Markup;
using Pixie.Options;
using Pixie.Terminal;
using Pixie.Transforms;

namespace LeMP.Repl
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Acquire a log for, well, logging purposes.
            var rawLog = TerminalLog.Acquire();

            var fancyLog = new TransformLog(
                rawLog,
                entry => DiagnosticExtractor.Transform(entry, new Text("LeMP-repl")));

            // Create an option parser.
            var optParser = new GnuOptionSetParser(
                Options.All,
                Options.Files,
                Options.Files.Forms[0]);

            // Parse command-line arguments.
            var parsedOptions = optParser.Parse(args, fancyLog);

            // Optionally display help message.
            if (parsedOptions.GetValue<bool>(Options.Help))
            {
                rawLog.Log(
                    new HelpMessage(
                        "LeMP-repl is a simple tool that takes unprocessed EC#, " +
                        "LES v2 or LES v3 code as input and produces processed or " +
                        "unprocessed EC#, LES v2 or LES v3 code as output.",
                        "LeMP-repl [options-or-files]",
                        Options.All));
                return;
            }
        }
    }
}
