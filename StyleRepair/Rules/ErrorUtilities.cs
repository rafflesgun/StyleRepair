namespace Michmela44.StyleRepair.Rules
{
    using System.Text.RegularExpressions;
    using EnvDTE;
    using EnvDTE80;
    using Objects;
    public static class ErrorUtilities
    {
        public static EditPoint2 GetEditPoint(DTE dte)
        {
            Document curDoc = dte.ActiveDocument;
            var textDoc = (TextDocument)curDoc.Object();
            TextSelection selection = textDoc.Selection;
            return (EditPoint2)selection.ActivePoint.CreateEditPoint();
        }

        public static void RegExUpdateWholeDocument(string findPattern, string replacePattern, VsError selectedError, DTE dte)
        {
            selectedError.Navigate();
            Document curDoc = dte.ActiveDocument;
            var textDoc = (TextDocument)curDoc.Object();
            textDoc.Selection.SelectAll();

            string allTheText = textDoc.Selection.Text;
            string formattedText = Regex.Replace(allTheText, findPattern, replacePattern, RegexOptions.Multiline);

            textDoc.Selection.Cut();
            textDoc.Selection.Insert(formattedText);
        }

        public static void RegExUpdate(string findPattern, string replacePattern, VsError selectedError, DTE dte)
        {
            selectedError.Navigate();
            EditPoint2 ep = GetEditPoint(dte);
            ep.StartOfLine();
            string textToUpdate = ep.GetLines(ep.Line, ep.Line + 1);
            textToUpdate = Regex.Replace(textToUpdate, findPattern, replacePattern);

            // Using the Autoformat option is cheating a little but saves on a lot of work.  It basically formats 
            // the text as if you were typing it in the IDE
            ep.ReplaceText(ep.LineLength, textToUpdate, (int)vsEPReplaceTextOptions.vsEPReplaceTextAutoformat);
        }
    }
}