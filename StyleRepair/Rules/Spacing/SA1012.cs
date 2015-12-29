namespace Michmela44.StyleRepair.Rules.Spacing
{
    using EnvDTE;
    using EnvDTE80;

    public static class SA1012
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            ErrorUtilities.RegExUpdate(@"\{(?=\S)", @"{ ", selectedError, dte);
        }
    }
}