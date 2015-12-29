namespace Michmela44.StyleRepair.Rules.Spacing
{
    using System.Text.RegularExpressions;
    using EnvDTE;
    using EnvDTE80;

    /// <summary>
    /// A violation of this rule occurs whenever the code contains a tab character.
    /// <para>Tabs should not be used within C# code, because the length of the tab character can vary depending upon the editor being used to view the code. 
    /// This can cause the spacing and indexing of the code to vary from the developer’s original intention, and can in some cases make the code difficult to read.</para>
    /// <para>For these reasons, tabs should not be used, and each level of indentation should consist of four spaces. This will ensure that the code looks the same no matter 
    /// which editor is being used to view the code.</para>
    /// </summary>
    public class SA1027
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            selectedError.Navigate();
            EditPoint2 ep = ErrorUtilities.GetEditPoint(dte);
            ep.StartOfLine();
            string testString = ep.GetLines(ep.Line, ep.Line + 1);
            if (Regex.Match(testString, @"\t").Success)
            {
                testString = Regex.Replace(testString, @"\t", @"    ");
                ep.ReplaceText(ep.LineLength, testString, (int)vsEPReplaceTextOptions.vsEPReplaceTextAutoformat);
            }
        }
    }
}