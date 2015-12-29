using System.Text.RegularExpressions;
using EnvDTE;
using EnvDTE80;

namespace Michmela44.StyleRepair.Rules.Readability
{
    public class SA1101
    {
        public static void Run(DTE dte, ErrorItem error)
        {
            error.Navigate();
            EditPoint2 ep = ErrorUtilities.GetEditPoint(dte);
            ep.StartOfLine();
            string variableName = error.Description.Split(" ".ToCharArray())[4];
            string replaceString = ep.GetLines(ep.Line, ep.Line + 1);
            replaceString = Regex.Replace(replaceString, string.Format(@"(\s|\()({0})(\W)", variableName), "$1this.$2$3",
                                          RegexOptions.None);
            ep.ReplaceText(ep.LineLength, replaceString, (int ) vsEPReplaceTextOptions.vsEPReplaceTextAutoformat);
        }
    }
}