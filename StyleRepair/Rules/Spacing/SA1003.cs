namespace Michmela44.StyleRepair.Rules.Spacing
{
    using EnvDTE;
    using EnvDTE80;

    /// <summary>
    /// A violation of this rule occurs when the spacing around an operator symbol is incorrect.
    /// The following types of operator symbols must be surrounded by a single space on either side: colons, arithmetic operators, assignment operators, 
    /// conditional operators, logical operators, relational operators, shift operators, and lambda operators
    /// </summary>
    public class SA1003
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            ErrorUtilities.RegExUpdate(@"(?<=\w)=(?=\w)", " = ", selectedError, dte);
        }
    }
}