namespace Michmela44.StyleRepair.Rules.Layout
{
    using EnvDTE;
    using EnvDTE80;


    public static class SA1513
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            selectedError.Navigate();
            EditPoint2 ep = ErrorUtilities.GetEditPoint(dte);
            ep.LineDown();
            ep.StartOfLine();
            ep.InsertNewLine();
        }
    }
}