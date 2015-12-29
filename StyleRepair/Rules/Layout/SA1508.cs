namespace Michmela44.StyleRepair.Rules.Layout
{
    using EnvDTE;
    using EnvDTE80;
    using Objects;

    public class SA1508
    {
        public static void Run(DTE dte, VsError selectedError)
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