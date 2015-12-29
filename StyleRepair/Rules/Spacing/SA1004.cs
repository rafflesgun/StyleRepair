namespace Michmela44.StyleRepair.Rules.Spacing
{
    using EnvDTE;
    using EnvDTE80;

    /// <summary>
    /// A violation of this rule occurs when a line within a documentation header does not begin with a single space.
    /// </summary>
    public class SA1004
    {
        public static void Run(DTE environment, ErrorItem selectedError)
        {
            selectedError.Navigate();
            ErrorUtilities.RegExUpdateWholeDocument(@"\/\/\/([^\/\s])", "/// $1", selectedError, environment);
        }
    }
}