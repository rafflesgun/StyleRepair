namespace Michmela44.StyleRepair.Rules.Layout
{
    using EnvDTE;
    using EnvDTE80;

    public static class SA1516
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            selectedError.Navigate();

            Document curDoc = dte.ActiveDocument;
            var textDoc = (TextDocument)curDoc.Object();
            TextSelection selection = textDoc.Selection;

            // Use top point here to take care of collapsed definitions
            var ep = (EditPoint2)selection.TopPoint.CreateEditPoint();

            ep.StartOfLine();
            ep.LineUp();
            while (ep.GetText(ep.LineLength).Replace(" ", string.Empty).StartsWith("//") ||
                   ep.GetText(ep.LineLength).Replace(" ", string.Empty).StartsWith("["))
            {
                ep.LineUp();
            }

            ep.EndOfLine();
            ep.InsertNewLine();
        }
    }
}