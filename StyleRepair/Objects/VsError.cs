using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;

namespace Michmela44.StyleRepair.Objects
{
    public class VsError
    {
        public string Description { get; set; }

        public string FileName { get; set; }

        public int Line { get; set; }

        public object ErrorItem { get; set; }

        public void Navigate()
        {
            if (this.ErrorItem != null)
            {
                var errorItem = this.ErrorItem as ErrorItem;
                if (errorItem != null)
                {
                    errorItem.Navigate();
                }
                else
                {
                    var taskItem = this.ErrorItem as IVsTaskItem;
                    if (taskItem != null)
                    {
                        taskItem.NavigateTo();
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
            }
        }
    }
}
