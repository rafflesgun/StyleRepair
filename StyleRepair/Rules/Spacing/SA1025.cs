namespace Michmela44.StyleRepair.Rules.Spacing
{
    using EnvDTE;
    using EnvDTE80;

    public class SA1025
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            ErrorUtilities.RegExUpdate(@"\s{2,}", @" ", selectedError, dte);
        }
    }
}