using System;
using System.IO;
using System.Reflection;
using EnvDTE;
using EnvDTE80;
using NArrange.Core;

namespace Michmela44.StyleRepair.Rules.Ordering
{
    public class SA1200
    {
        public static void Run(DTE dte, ErrorItem selectedError)
        {
            var logger = new ConsoleLogger {Trace = true};
            Console.WriteLine();
            Console.WriteLine(new string('_', 60));

            selectedError.Navigate();
            string formatString = Properties.StyleRepair.Default.NArrangeUseRegions
                                      ? @"{0}\NArrangeConfigWithRegions.xml"
                                      : @"{0}\NArrangeConfig.xml";

            var fileArranger = new FileArranger(
                string.Format(formatString, Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)),
                Logger.Instance);

            bool success = fileArranger.Arrange(dte.ActiveDocument.FullName, dte.ActiveDocument.FullName, true);
            if (!success)
            {
                logger.LogMessage(LogLevel.Error, "Unable to NArrange {0}.", dte.ActiveDocument.FullName);
            }
        }
    }
}