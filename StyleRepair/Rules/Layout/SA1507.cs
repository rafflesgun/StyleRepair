namespace Michmela44.StyleRepair.Rules.Layout
{
    using EnvDTE;
    using EnvDTE80;

    public class SA1507
    {
        public static void Run(DTE dte, ErrorItem selectedItem)
        {
            selectedItem.Navigate();
            ErrorUtilities.RegExUpdateWholeDocument(@"^(\s*\t*\r\n\s*\t*){1,}$", string.Empty, selectedItem, dte);
        }
    }
}