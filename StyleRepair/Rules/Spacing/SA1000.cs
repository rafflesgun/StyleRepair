namespace Michmela44.StyleRepair.Rules.Spacing
{
    using System.Collections.Generic;
    using EnvDTE;
    using EnvDTE80;

    /// <summary>
    /// A violation of this rule occurs when the spacing around a keyword is incorrect.
    /// The following C# keywords must always be followed by a single space: catch, fixed, for, foreach, from, group, if, in, into, join, let, lock, orderby, return, select, stackalloc, switch, throw, using, where, while, yield.
    /// The following keywords must not be followed by any space: checked, default, sizeof, typeof, unchecked.
    /// The new keyword should always be followed by a space, unless it is used to create a new array, in which case there should be no space between the new keyword and the opening array bracket.
    /// </summary>
    public class SA1000
    {
        private static readonly List<string> SpaceKeywords = new List<string>
                                                                 {
                                                                     "catch",
                                                                     "fixed",
                                                                     "for",
                                                                     "foreach",
                                                                     "from",
                                                                     "group",
                                                                     "if",
                                                                     "in",
                                                                     "into",
                                                                     "join",
                                                                     "let",
                                                                     "lock",
                                                                     "orderby",
                                                                     "return",
                                                                     "select",
                                                                     "stackalloc",
                                                                     "switch",
                                                                     "throw",
                                                                     "using",
                                                                     "where",
                                                                     "while",
                                                                     "yield"
                                                                 };

        private static readonly List<string> NonSpaceKeywords = new List<string>
                                                                    {
                                                                        "checked",
                                                                        "default",
                                                                        "sizeof",
                                                                        "typeof",
                                                                        "unchecked"
                                                                    };

        public static void Run(DTE dte, ErrorItem selectedError)
        {
            // SA1000: The spacing around the keyword 'fixed' is invalid.	
            string keyword = selectedError.Description.Split(' ')[6].Replace("'", string.Empty);

            if (SpaceKeywords.Contains(keyword))
            {
                ErrorUtilities.RegExUpdate(@"(" + keyword + @")([^\w\s])", @"$1 $2", selectedError, dte);
            }
            else if (NonSpaceKeywords.Contains(keyword))
            {
                ErrorUtilities.RegExUpdate(@"(" + keyword + @")\s+([^\w\s])", @"$1$2", selectedError, dte);
            }
            else if (keyword == "new")
            {
                ErrorUtilities.RegExUpdate(@"(" + keyword + @")([^\w\s\[])", @"$1 $2", selectedError, dte);
                ErrorUtilities.RegExUpdate(@"(" + keyword + @")\s+(\[)", @"$1$2", selectedError, dte);
            }
        }
    }
}