namespace Michmela44.StyleRepair.Rules.Layout
{
    using EnvDTE;
    using EnvDTE80;

    public class SA1508
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            selectedError.Navigate();
            var ep = ErrorUtilities.GetEditPoint(dte);
            ep.LineUp();
            while (ep.LineLength == 0)
            {
                ep.Delete(1);
                ep.LineUp();
            }
        }
    }
}