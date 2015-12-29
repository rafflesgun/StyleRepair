namespace Michmela44.StyleRepair.Rules.Spacing
{
    using System.Text.RegularExpressions;
    using EnvDTE;
    using EnvDTE80;
    using Michmela44.StyleRepair.Objects;

    /// <summary>
    /// A violation of this rule occurs when the spacing around a comma is incorrect.
    /// <para>A comma should always be followed by a single space, unless it is the last character on the line, and a comma should never be preceded by any whitespace, unless it is the first character on the line.</para>
    /// </summary>
    public class SA1001
    {
        public static void Run(DTE dte, VsError selectedError)
        {
            selectedError.Navigate();
            EditPoint2 ep = ErrorUtilities.GetEditPoint(dte);
            ep.StartOfLine();
            string testString = ep.GetLines(ep.Line, ep.Line + 1);
            if (Regex.Match(testString, @"([\S])\s+,").Success || Regex.Match(testString, @",([\S])").Success)
            {
                testString = Regex.Replace(testString, @"([\S])\s+,", @"$1,");
                testString = Regex.Replace(testString, @",([\S])", @", $1");
                ep.ReplaceText(ep.LineLength, testString, (int)vsEPReplaceTextOptions.vsEPReplaceTextAutoformat);
            }
        }
    }
}