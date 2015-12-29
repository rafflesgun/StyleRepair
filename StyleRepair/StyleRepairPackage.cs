using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Michmela44.StyleRepair.Objects;
using Michmela44.StyleRepair.Rules.Documentation;
using Michmela44.StyleRepair.Rules.Layout;
using Michmela44.StyleRepair.Rules.Naming;
using Michmela44.StyleRepair.Rules.Ordering;
using Michmela44.StyleRepair.Rules.Readability;
using Michmela44.StyleRepair.Rules.Spacing;
using Michmela44.StyleRepair.Views;
using Microsoft.VisualStudio.Shell.Interop;

namespace Michmela44.StyleRepair
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidStyleRepairPkgString)]
    public sealed class StyleRepairPackage : Package
    {
        private DTE dte;

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public StyleRepairPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", ToString()));
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", ToString()));
            base.Initialize();
            this.dte = (DTE) GetService(typeof (DTE));

            Logger.Instance.pane = CreatePane("StyleRepair");

            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                var menuCommandID = new CommandID(GuidList.guidStyleRepairCmdSet, (int) PkgCmdIDList.cmdidMyCommand);
                var menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                mcs.AddCommand(menuItem);

                // Create the menu option in the error list window
                var errorListCommand = new CommandID(GuidList.guidErrorListCommandSet,
                                                     (int) PkgCmdIDList.commandIdErrorList);
                var errorMenuItem = new OleMenuCommand(FixStyleCopWarnings, errorListCommand);
                errorMenuItem.BeforeQueryStatus += errorMenuItem_BeforeQueryStatus;
                mcs.AddCommand(errorMenuItem);
            }
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var updateSettings = new UpdateSettings();
            updateSettings.Show();

            // Show a Message Box to prove we were here
            /*IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
            Guid clsid = Guid.Empty;
            int result;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
                       0,
                       ref clsid,
                       "StyleRepair",
                       string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.ToString()),
                       string.Empty,
                       0,
                       OLEMSGBUTTON.OLEMSGBUTTON_OK,
                       OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                       OLEMSGICON.OLEMSGICON_INFO,
                       0,        // false
                       out result));*/
        }

        private List<VsError> GetErrorList()
        {
            var vsErrorList = new List<VsError>();

            Window window = this.dte.Windows.Item(WindowKinds.vsWindowKindErrorList);
            var myErrorList = (ErrorList)window.Object;

            var objer = (object[])myErrorList.SelectedItems;

            if (objer != null)
            {
                foreach (object item in objer)
                {
                    var errorItem = item as ErrorItem;
                    if (errorItem != null)
                    {
                        vsErrorList.Add(new VsError()
                        {
                            Description = errorItem.Description,
                            FileName = errorItem.FileName,
                            Line = errorItem.Line,
                            ErrorItem = errorItem,
                        });
                    }
                }
            }
            else
            {
                return this.GetErrorList_VS2015();
            }

            return vsErrorList;
        }

        private List<VsError> GetErrorList_VS2015()
        {
            var vsErrorList = new List<VsError>();

            var errorList = this.GetService(typeof(SVsErrorList)) as IVsTaskList2;

            if (errorList != null)
            {
                IVsEnumTaskItems enumerator;
                errorList.EnumSelectedItems(out enumerator);
                var arr = new IVsTaskItem[1];

                while (enumerator.Next(1, arr, null) == 0)
                {
                    string description;
                    arr[0].get_Text(out description);

                    string fileName;
                    arr[0].Document(out fileName);

                    int lineNumber;
                    arr[0].Line(out lineNumber);

                    vsErrorList.Add(new VsError()
                    {
                        Description = description,
                        FileName = fileName,
                        Line = lineNumber,
                        ErrorItem = arr[0],
                    });
                }
            }

            return vsErrorList;
        }

        private void FixStyleCopWarnings(object sender, EventArgs e)
        {
            var selectedErrors = new List<StyleCopError>();

            var errorList = this.GetErrorList();

            foreach (var item in errorList)
            {
                var styleCopError = new StyleCopError(this.dte, item.Description.Substring(0, 6), item);
                selectedErrors.Add(styleCopError);
            }

            IOrderedEnumerable<StyleCopError> result = from error in selectedErrors
                                                       orderby error.Error.FileName
                                                       orderby error.Error.Line descending
                                                       select error;

            selectedErrors = result.ToList();

            foreach (StyleCopError item in selectedErrors)
            {
                switch (item.ErrorCode)
                {
                    case "SA1000":
                        SA1000.Run(this.dte, item.Error);
                        break;
                    case "SA1001":
                        SA1001.Run(this.dte, item.Error);
                        break;
                    case "SA1002":
                        SA1002.Run(this.dte, item.Error);
                        break;
                    case "SA1003":
                        SA1003.Run(this.dte, item.Error);
                        break;
                    case "SA1004":
                        SA1004.Run(this.dte, item.Error);
                        break;
                    case "SA1005":
                        SA1005.Run(this.dte, item.Error);
                        break;
                    case "SA1006":
                        SA1006.Run(this.dte, item.Error);
                        break;
                    case "SA1008":
                        SA1008.Run(this.dte, item.Error);
                        break;
                    case "SA1009":
                        SA1009.Run(this.dte, item.Error);
                        break;
                    case "SA1101":
                        SA1101.Run(this.dte, item.Error);
                        break;
                    case "SA1012":
                        SA1012.Run(this.dte, item.Error);
                        break;
                    case "SA1013":
                        SA1013.Run(this.dte, item.Error);
                        break;
                    case "SA1025":
                        SA1025.Run(this.dte, item.Error);
                        break;
                    case "SA1027":
                        SA1027.Run(this.dte, item.Error);
                        break;
                    case "SA1106":
                        SA1106.Run(this.dte, item.Error);
                        break;

                    case "SA1121":
                        SA1121.Run(this.dte, item.Error);
                        break;
                    case "SA1122":
                        SA1122.Run(this.dte, item.Error);
                        break;
                        //case "SA1202":
                        //    Rules.SA1202 errorFixer = new Rules.SA1202();
                        //    errorFixer.Run(this.dte, item.Error);
                        //    break;
                    case "SA1210":
                    case "SA1208":
                        SA1210.Run(this.dte, item.Error);
                        break;
                    case "SA1309":
                        SA1309.Run(this.dte, item.Error);
                        break;
                    case "SA1505":
                        SA1505.Run(this.dte, item.Error);
                        break;
                    case "SA1507":
                        SA1507.Run(this.dte, item.Error);
                        break;
                    case "SA1508":
                        SA1508.Run(this.dte, item.Error);
                        break;
                    case "SA1513":
                        SA1513.Run(this.dte, item.Error);
                        break;
                    case "SA1515":
                    case "SA1516":
                        SA1516.Run(this.dte, item.Error);
                        break;
                    case "SA1200":
                    case "SA1201":
                    case "SA1202":
                    case "SA1203":
                    case "SA1204":
                    case "SA1205":
                    case "SA1206":
                    case "SA1207":
                        SA1200.Run(this.dte, item.Error);
                        break;
                    case "SA1600":
                        SA1600.Run(this.dte, item.Error);
                        break;
                    case "SA1633":
                        SA1633.Run(this.dte, item.Error);
                        break;
                }
            }

            this.dte.ExecuteCommand("DebuggerContextMenus.ScriptProject.RunStyleCop");
        }

        private void errorMenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            var menuItem = sender as OleMenuCommand;
            using (var errorListMenu = new ErrorListProvider(this))
            {
                Window window = this.dte.Windows.Item(WindowKinds.vsWindowKindErrorList);
                var myErrorList = (ErrorList) window.Object;
                if (myErrorList.ErrorItems != null)
                {
                    if (myErrorList.ErrorItems.Count > 0)
                    {
                        menuItem.Enabled = true;
                        menuItem.Visible = true;
                    }
                    else
                    {
                        menuItem.Enabled = false;
                        menuItem.Visible = false;
                    }
                    
                    var errorList = this.GetErrorList();

                    foreach (var item in errorList)
                    {
                        if (item.Description.Substring(0, 2).ToUpper() != "SA")
                        {
                            menuItem.Enabled = false;
                        }
                    }
                }
            }
        }

        private OutputWindowPane CreatePane(string title)
        {
            var dte = (DTE2) GetService(typeof (DTE));
            OutputWindowPanes panes =
                dte.ToolWindows.OutputWindow.OutputWindowPanes;

            try
            {
                // If the pane exists already, return it.
                return panes.Item(title);
            }
            catch (ArgumentException)
            {
                // Create a new pane.
                return panes.Add(title);
            }
        }
    }
}