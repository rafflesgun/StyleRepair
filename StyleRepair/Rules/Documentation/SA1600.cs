namespace Michmela44.StyleRepair.Rules.Documentation
{
    using System;
    using EnvDTE;
    using EnvDTE80;

    /// <summary>
    /// Object that details how to fix a SA1600 StyleCop warning
    /// </summary>
    public class SA1600
    {
        /// <summary>
        /// Fix error SA1600
        /// </summary>
        /// <param name="dte">Current design environment</param>
        /// <param name="selectedError">Error selected by the user to fix.</param>
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            EditPoint2 ep = ErrorUtilities.GetEditPoint(dte);
            selectedError.Navigate();
            var selection = (TextSelection)dte.ActiveDocument.Selection;
            var propertyElement = (CodeProperty)selection.ActivePoint.CodeElement[vsCMElement.vsCMElementProperty];
            if (propertyElement != null)
            {
                ProcessPropertyUpdate(propertyElement, ep);
            }
        }

        /// <summary>
        /// Create the text for a property using the property name and the accessors
        /// </summary>
        /// <param name="elem">Property we are updating</param>
        /// <param name="ep">The point we are inserting the text</param>
        private static void ProcessPropertyUpdate(CodeProperty elem, EditPoint2 ep)
        {
            string descriptionText = string.Empty;
            if (elem.Getter != null)
            {
                switch (elem.Getter.Access)
                {
                    case vsCMAccess.vsCMAccessPrivate:
                        break;
                    default:
                        descriptionText = "Gets";
                        break;
                }
            }

            if (elem.Setter != null)
            {
                switch (elem.Setter.Access)
                {
                    case vsCMAccess.vsCMAccessPrivate:
                        break;
                    default:
                        if (descriptionText.Length > 0)
                        {
                            descriptionText += " or sets";
                        }
                        else
                        {
                            descriptionText = "Sets";
                        }

                        break;
                }
            }

            descriptionText += " " + elem.Name;
            TextPoint tp = elem.GetStartPoint();
            ep.MoveToPoint(tp);
            string spacer = string.Empty;
            for (int i = 0; i < tp.LineCharOffset - 1; i++)
            {
                spacer += " ";
            }

            ep.LineUp();
            ep.Insert(Environment.NewLine);
            ep.Insert(spacer + "/// <summary>");
            ep.Insert(Environment.NewLine + spacer + string.Format("/// {0}", descriptionText));
            ep.Insert(Environment.NewLine + spacer + "/// </summary>");
            ep.Insert(spacer);
        }
    }
}