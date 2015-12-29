namespace Michmela44.StyleRepair.Rules.Documentation
{
    using EnvDTE;
    using EnvDTE80;
    using Objects;

    public class SA1633
    {
        public static void Run(DTE dte, VsError selectedError)
        {
            selectedError.Navigate();
            EditPoint2 editPoint = ErrorUtilities.GetEditPoint(dte);
            editPoint.StartOfDocument();
            string fileName = selectedError.FileName.Substring(
                selectedError.FileName.LastIndexOf(@"\") + 1,
                selectedError.FileName.Length - (selectedError.FileName.LastIndexOf(@"\") + 1));
            editPoint.InsertNewLine();
            editPoint.StartOfDocument();
            editPoint.Insert(
                string.Format(
                @"//-----------------------------------------------------------------------
// <copyright file=""{0}"" company=""{1}"">
//     {2}
// </copyright>
//-----------------------------------------------------------------------",
                fileName,
                StyleRepair.Properties.StyleRepair.Default.CompanyName,
                StyleRepair.Properties.StyleRepair.Default.CopyrightMessage));
        }
    }
}