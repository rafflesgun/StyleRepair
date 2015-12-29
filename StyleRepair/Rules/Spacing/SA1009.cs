namespace Michmela44.StyleRepair.Rules.Spacing
{
    using System;
    using EnvDTE;
    using EnvDTE80;
    using Michmela44.StyleRepair.Objects;

    public static class SA1009
    {
        public static void Run(DTE environment, VsError selectedError)
        {
            if (selectedError == null)
            {
                throw new ArgumentNullException("selectedError", @"Selected error is null");
            }

            selectedError.Navigate();

            // Replace all items that have spaces before the ending brackets
            // "A closing parenthesis should never be preceded by whitespace."
            ErrorUtilities.RegExUpdateWholeDocument(@"\s+\)", ")", selectedError, environment);

            // "If the closing parenthesis is followed by whitespace, the next non-whitespace character must not be an opening or closing parenthesis or square bracket, 
            //  or a semicolon or comma."
            ErrorUtilities.RegExUpdateWholeDocument(@"\)\s+;", @");", selectedError, environment);
            ErrorUtilities.RegExUpdateWholeDocument(@"\)\s+,", @"),", selectedError, environment);
            ErrorUtilities.RegExUpdateWholeDocument(@"\)\s+\(", @")(", selectedError, environment);
            ErrorUtilities.RegExUpdateWholeDocument(@"\)\s+\)", @"))", selectedError, environment);
            ErrorUtilities.RegExUpdateWholeDocument(@"\)\s+\[", @")\[", selectedError, environment);
            ErrorUtilities.RegExUpdateWholeDocument(@"\)\s+\]", @")\]", selectedError, environment);

            // replace all items that have a cariage return between each line
            ErrorUtilities.RegExUpdateWholeDocument(@"(\s+[a-zA-Z]+\))\s*\r\n(\).*)", "$1$2", selectedError, environment);

            // In most cases, a closing parenthesis should be followed by a single space, unless the closing parenthesis comes at the end of a cast, 
            // or the closing parenthesis is followed by certain types of operator symbols, such as positive signs, negative signs, and colons.
        }
    }
}