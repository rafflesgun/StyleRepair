namespace Michmela44.StyleRepair.Rules.Layout
{
    using EnvDTE;
    using EnvDTE80;
    using Objects;

    public class SA1505
    {
        public static void Run(DTE dte, VsError selectedError)
        {
            selectedError.Navigate();
            var ep = ErrorUtilities.GetEditPoint(dte);
            ep.LineDown();
            ep.Delete(1);
        }
    }
}