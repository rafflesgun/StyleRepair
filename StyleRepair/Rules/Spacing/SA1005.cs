namespace Michmela44.StyleRepair.Rules.Spacing
{
    using EnvDTE;
    using EnvDTE80;
    using Michmela44.StyleRepair.Objects;

    public static class SA1005
    {
        public static void Run(DTE environment, VsError selectedError)
        {
            selectedError.Navigate();
            ErrorUtilities.RegExUpdateWholeDocument(@"\/\/([^\/\s])", "// $1", selectedError, environment);
        }
    }
}