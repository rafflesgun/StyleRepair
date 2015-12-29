using EnvDTE;
using EnvDTE80;

namespace Michmela44.StyleRepair.Objects
{
    public class StyleCopError
    {
        private readonly string errorCode;
        private DTE dte;
        private VsError vsError;

        public StyleCopError(DTE dte, string errorCode, VsError error)
        {
            this.errorCode = errorCode;
            this.dte = dte;
            this.vsError = error;
        }

        public VsError Error
        {
            get { return this.vsError; }
        }

        public string ErrorCode
        {
            get { return this.errorCode; }
        }
    }
}