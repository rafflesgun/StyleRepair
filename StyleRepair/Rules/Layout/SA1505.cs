namespace Michmela44.StyleRepair.Rules.Layout
{
    using EnvDTE;
    using EnvDTE80;

    public class SA1505
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            selectedError.Navigate();
            var ep = ErrorUtilities.GetEditPoint(dte);
            ep.LineDown();
            ep.Delete(1);
        }
    }
}