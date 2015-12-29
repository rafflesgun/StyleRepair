// Guids.cs
// MUST match guids.h

using System;

namespace Michmela44.StyleRepair
{
    internal static class GuidList
    {
        public const string guidStyleRepairPkgString = "fb16c8ce-ef9a-42f1-bc4d-8fe682e2cef9";
        public const string guidStyleRepairCmdSetString = "28e9376e-ae07-4ed9-a436-529c5a97437e";

        public const string guidErrorListString = "BEF5B7A7-BE4E-4AC1-90B7-FB60A8F05196";

        public static readonly Guid guidStyleRepairCmdSet = new Guid(guidStyleRepairCmdSetString);
        public static readonly Guid guidErrorListCommandSet = new Guid(guidErrorListString);
    };
}