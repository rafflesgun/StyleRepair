using EnvDTE;
using EnvDTE80;

namespace Michmela44.StyleRepair.Objects
{
    public class StyleCopError
    {
        private readonly string errorCode;
        private DTE dte;

        public StyleCopError(DTE dte, string errorCode)
        {
            this.errorCode = errorCode;
            this.dte = dte;
        }

        public ErrorItem Error { get; set; }

        public string ErrorCode
        {
            get { return this.errorCode; }
        }
    }
}