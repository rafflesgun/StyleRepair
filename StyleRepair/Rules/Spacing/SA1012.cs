namespace Michmela44.StyleRepair.Rules.Spacing
{
    using EnvDTE;
    using EnvDTE80;
    using Michmela44.StyleRepair.Objects;

    public static class SA1012
    {
        public static void Run(DTE dte, VsError selectedError)
        {
            ErrorUtilities.RegExUpdate(@"\{(?=\S)", @"{ ", selectedError, dte);
        }
    }
}