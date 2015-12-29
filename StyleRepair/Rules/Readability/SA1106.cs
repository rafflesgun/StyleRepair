using EnvDTE;
using EnvDTE80;
using Michmela44.StyleRepair.Objects;

namespace Michmela44.StyleRepair.Rules.Readability
{
    public static class SA1106
    {
        public static void Run(DTE environment, VsError selectedError)
        {
            selectedError.Navigate();
            // TODO: verify this works
            ErrorUtilities.RegExUpdate(@"(?<=\})\s*;", string.Empty, selectedError, environment);
        }
    }
}