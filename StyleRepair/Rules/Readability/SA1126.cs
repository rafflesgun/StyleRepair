using System.Text.RegularExpressions;
using EnvDTE;
using EnvDTE80;
using Michmela44.StyleRepair.Objects;

namespace Michmela44.StyleRepair.Rules.Readability
{
    public class SA1126
    {
        public static void Run(DTE dte, VsError error)
        {
            error.Navigate();
            EditPoint2 ep = ErrorUtilities.GetEditPoint(dte);
            ep.StartOfLine();
            string variableName = error.Description.Split(" ".ToCharArray())[7];
            string replaceString = ep.GetLines(ep.Line, ep.Line + 1);
            replaceString = Regex.Replace(replaceString, string.Format(@"(\s|\(|\!)({0})(\W)", variableName), "$1this.$2$3",
                                          RegexOptions.None);
            ep.ReplaceText(ep.LineLength, replaceString, (int ) vsEPReplaceTextOptions.vsEPReplaceTextAutoformat);
        }
    }
}