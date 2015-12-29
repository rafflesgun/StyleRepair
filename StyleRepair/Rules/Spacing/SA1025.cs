namespace Michmela44.StyleRepair.Rules.Spacing
{
    using EnvDTE;
    using EnvDTE80;
    using Michmela44.StyleRepair.Objects;

    public class SA1025
    {
        public static void Run(DTE dte, VsError selectedError)
        {
            ErrorUtilities.RegExUpdate(@"\s{2,}", @" ", selectedError, dte);
        }
    }
}