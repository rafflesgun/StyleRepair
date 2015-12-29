namespace Michmela44.StyleRepair.Rules.Layout
{
    using EnvDTE;
    using EnvDTE80;
    using Objects;

    public class SA1507
    {
        public static void Run(DTE dte, VsError selectedItem)
        {
            selectedItem.Navigate();
            ErrorUtilities.RegExUpdateWholeDocument(@"^(\s*\t*\r\n\s*\t*){1,}$", string.Empty, selectedItem, dte);
        }
    }
}