using System;
using EnvDTE;
using NArrange.Core;

namespace Michmela44.StyleRepair
{
    internal class Logger : ILogger
    {
        private static Logger _instance;
        internal OutputWindowPane pane;

        private Logger()
        {
        }

        public static Logger Instance
        {
            get { return _instance ?? (_instance = new Logger()); }
        }

        #region ILogger Members

        void ILogger.LogMessage(LogLevel level, string message, params object[] args)
        {
            if (this.pane != null)
            {
                this.pane.OutputString(string.Format(message, args) + Environment.NewLine);
            }
        }

        #endregion
    }
}