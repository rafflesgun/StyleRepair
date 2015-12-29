using EnvDTE;
using EnvDTE80;

namespace Michmela44.StyleRepair.Rules.Readability
{
    public static class SA1106
    {
        public static void Run(DTE environment, ErrorItem selectedError)
        {
            selectedError.Navigate();
            // TODO: verify this works
            ErrorUtilities.RegExUpdate(@"(?<=\})\s*;", string.Empty, selectedError, environment);
        }
    }
}