using System.Collections.Generic;
using Pixie.Markup;
using Pixie.Options;

namespace LeMP.Repl
{
    public static class Options
    {
        private const string IOCategory = "Input and output";

        /// <summary>
        /// The 'do not process macros' option.
        /// </summary>
        public static readonly Option DoNotProcessMacros =
            FlagOption.CreateFlagOption(
                OptionForm.Long("no-process-macros"),
                OptionForm.Short("p"))
            .WithCategory(IOCategory)
            .WithDescription(
                new Sequence(
                    "Turns off macro processing by LeMP."));

        /// <summary>
        /// The 'files' pseudo-option.
        /// </summary>
        public static readonly Option Files =
            FlagOption.CreateFlagOption(
                OptionForm.Long("files"))
            .WithCategory(IOCategory)
            .WithDescription(
                new Sequence(
                    "Select input files by listing their files names. Specify ",
                    Quotation.CreateBoldQuotation("-"),
                    " to read from standard input, which is also what happens by " +
                    "default if no files are opened."));

        /// <summary>
        /// The 'help' option.
        /// </summary>
        public static readonly Option Help =
            FlagOption.CreateFlagOption(
                OptionForm.Long("help"),
                OptionForm.Short("h"))
            .WithDescription("Display a help message and exit.");

        /// <summary>
        /// The 'input language' option.
        /// </summary>
        public static readonly Option InputLanguage =
            ValueOption.CreateStringOption(
                new OptionForm[]
                {
                    OptionForm.Long("input-language"),
                    OptionForm.Short("i")
                },
                "ecs")
            .WithCategory(IOCategory)
            .WithParameter(new SymbolicOptionParameter("language"))
            .WithDescription(
                new Sequence(
                    "Selects an input language, which can be either ",
                    Quotation.CreateBoldQuotation("ecs"),
                    ", ",
                    Quotation.CreateBoldQuotation("les"),
                    ", ",
                    Quotation.CreateBoldQuotation("les2"),
                    " or ",
                    Quotation.CreateBoldQuotation("les3"),
                    ". By default, the input language is EC#."));

        /// <summary>
        /// The 'output language' option.
        /// </summary>
        public static readonly Option OutputLanguage =
            ValueOption.CreateStringOption(
                new OptionForm[]
                {
                    OptionForm.Long("output-language"),
                    OptionForm.Short("o")
                },
                "les")
            .WithCategory(IOCategory)
            .WithParameter(new SymbolicOptionParameter("language"))
            .WithDescription(
                new Sequence(
                    "Selects an output language, which can be either ",
                    Quotation.CreateBoldQuotation("cs"),
                    ", ",
                    Quotation.CreateBoldQuotation("ecs"),
                    ", ",
                    Quotation.CreateBoldQuotation("les"),
                    ", ",
                    Quotation.CreateBoldQuotation("les2"),
                    " or ",
                    Quotation.CreateBoldQuotation("les3"),
                    ". By default, the output language is LES v3."));

        /// <summary>
        /// A list of all options for LeMP-repl.
        /// </summary>
        public static readonly IReadOnlyList<Option> All =
            new Option[]
        {
            DoNotProcessMacros,
            // Files,
            Help,
            InputLanguage,
            OutputLanguage
        };
    }
}
