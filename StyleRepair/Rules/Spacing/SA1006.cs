namespace Michmela44.StyleRepair.Rules.Spacing
{
    using EnvDTE;
    using EnvDTE80;
    using Michmela44.StyleRepair.Objects;

    public static class SA1006
    {
        public static void Run(DTE dte, VsError selectedError)
        {
            selectedError.Navigate();

            /*
                     * A violation of this rule occurs when the preprocessor-type keyword in a preprocessor directive is preceded by space
                     */
            ErrorUtilities.RegExUpdateWholeDocument(@"#[^\S\n]+(\w)", "#$1", selectedError, dte);
        }
    }
}