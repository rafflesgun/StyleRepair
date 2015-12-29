using EnvDTE;
using EnvDTE80;

namespace Michmela44.StyleRepair.Rules.Readability
{
    /// <summary>
    /// </summary>
    public class SA1122
    {
        /// <summary>
        /// Attempt to fix the error via regular expression updates
        /// </summary>
        /// <param name="dte">Design time environment the update is taking place in</param>
        /// <param name="selectedError">The error that we are trying to fix</param>
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            ErrorUtilities.RegExUpdateWholeDocument(@"""", "string.Empty", selectedError, dte);
        }
    }
}