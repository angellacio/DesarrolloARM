//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:UpdateEventArgs:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Security.Permissions;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class UpdateEventArgs : EventArgs
    {
        // Fields
        private bool updated;
        private string errorMessage;

        // Methods
        public UpdateEventArgs()
            : this(false)
        {
        }

        public UpdateEventArgs(bool update)
        {
            this.updated = update;
        }

        // Properties
        public bool Updated
        {
            get
            {
                return this.updated;
            }
            set
            {
                this.updated = value;
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }
    }
}
