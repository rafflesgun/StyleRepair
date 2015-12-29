using EnvDTE;
using EnvDTE80;
using Michmela44.StyleRepair.Objects;

namespace Michmela44.StyleRepair.Rules.Ordering
{
    public static class SA1210
    {
        public static void Run(DTE dte, VsError selectedError)
        {
            selectedError.Navigate();
            dte.ExecuteCommand("Edit.SortUsings");
        }
    }
}