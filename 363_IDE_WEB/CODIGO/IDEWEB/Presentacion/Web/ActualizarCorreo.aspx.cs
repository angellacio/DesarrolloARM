using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sat.Scade.Net.IDE.Presentacion.Web;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public partial class ActualizarCorreo : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Email1.RfcText = base.GetCurrentMembershipUser().RFC;
            }

        }
    }
}