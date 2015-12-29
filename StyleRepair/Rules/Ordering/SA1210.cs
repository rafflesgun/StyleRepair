using EnvDTE;
using EnvDTE80;

namespace Michmela44.StyleRepair.Rules.Ordering
{
    public static class SA1210
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            selectedError.Navigate();
            dte.ExecuteCommand("Edit.SortUsings");
        }
    }
}