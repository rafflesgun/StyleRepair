namespace Michmela44.StyleRepair.Rules.Spacing
{
    using System.Text.RegularExpressions;

    using EnvDTE;

    using EnvDTE80;

    public static class SA1008
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            selectedError.Navigate();

           /*
                     * A violation of this rule occurs when the opening parenthesis within a statement is not spaced correctly. 
                     * An opening parenthesis should not be preceded by any whitespace, unless it is the first character on the line, or it is preceded 
                     * by certain C# keywords such as if, while, or for. In addition, an opening parenthesis is allowed to be preceded by whitespace when 
                     * it follows an operator symbol within an expression.
                     * 
                     *An opening parenthesis should not be followed by whitespace, unless it is the last character on the line.
                     */
            ErrorUtilities.RegExUpdateWholeDocument(@"\([^\S\n]+", "(", selectedError, dte);
        }
    }
}