using EnvDTE;
using EnvDTE80;
using Michmela44.StyleRepair.Objects;

namespace Michmela44.StyleRepair.Rules.Readability
{
    /// <summary>
    /// Force the user to use built in alias types.
    /// </summary>
    public class SA1121
    {
        /// <summary>
        /// Attempt to fix the error via regular expression updates
        /// </summary>
        /// <param name="dte">Design time environment the update is taking place in</param>
        /// <param name="selectedError">The error that we are trying to fix</param>
        public static void Run(DTE dte, VsError selectedError)
        {
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Array(\s+)", "$1array$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Boolean(\s+)", "$1bool$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Byte(\s+)", "$1byte$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Char(\s+)", "$1char$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Decimal(\s+)", "$1decimal$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Double(\s+)", "$1double$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Int16(\s+)", "$1short$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Int32(\s+)", "$1int$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Int64(\s+)", "$1long$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Object(\s+)", "$1object$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*SByte(\s+)", "$1sbyte$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*Single(\s+)", "$1single$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*UInt16(\s+)", "$1ushort$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*UInt32(\s+)", "$1uint$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*UInt64(\s+)", "$1ulong$2", selectedError, dte);
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+)[System\.]*String(\s+)", "$1string$2", selectedError, dte);
        }
    }
}