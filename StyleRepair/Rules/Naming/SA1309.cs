namespace Michmela44.StyleRepair.Rules.Naming
{
    using System.Linq;
    using EnvDTE;
    using EnvDTE80;

    public static class SA1309
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            selectedError.Navigate();
            EditPoint2 ep = ErrorUtilities.GetEditPoint(dte);            
            ep.StartOfLine();
            string editLine = ep.GetLines(ep.Line, ep.Line + 1);
            editLine = editLine.Replace(";", "");
            string varName = string.Empty;
            if (editLine.IndexOf("=") > -1)
            {                
                varName = editLine.Split("=".ToCharArray()).First().Split(" ".ToCharArray()).Where(item => item.Length > 0).Last();
            }
            else
            {
                varName = editLine.Split(" ".ToCharArray()).Last();
            }
            ep.Parent.Selection.CharRight(Count: editLine.IndexOf(varName));
            dte.ExecuteCommand("Refactor.Rename");
            //// todo
        }
    }
}