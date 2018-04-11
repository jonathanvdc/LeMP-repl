using System.Collections.Generic;
using Pixie.Markup;
using Pixie.Options;

namespace LeMP.Repl
{
    public static class Options
    {
        private const string IOCategory = "Input and output";

        /// <summary>
        /// The 'files' option.
        /// </summary>
        public static readonly Option Files =
            SequenceOption.CreateStringOption(
                OptionForm.Long("files"))
            .WithCategory(IOCategory)
            .WithDescription(
                new Sequence(
                    "Select input files by listing their files names. Specify ",
                    Quotation.CreateBoldQuotation("-"),
                    " to read from standard input, which is also what happens by " +
                    "default if no files are opened."))
            .WithParameters(new SymbolicOptionParameter("files-names", true));

        /// <summary>
        /// The 'help' option.
        /// </summary>
        public static readonly Option Help =
            FlagOption.CreateFlagOption(
                OptionForm.Long("help"),
                OptionForm.Short("h"))
            .WithDescription("Display a help message and exit.");

        /// <summary>
        /// A list of all options for LeMP-repl.
        /// </summary>
        public static readonly IReadOnlyList<Option> All =
            new Option[]
        {
            Files,
            Help
        };
    }
}
