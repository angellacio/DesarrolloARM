//@(#)SCADE2(W:SKDN08455AK4:Sat.Scade.Net.IDE.Presentacion.Web:FielMembershipProvider:0:10/Noviembre/2008[Sat.Scade.Net.IDE.Presentacion.Web:0:10/Noviembre/2008]) 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Web.Security;

using SAT.DyP.Presentacion.Seguridad.Membresia;
using SAT.DyP.Util.Web.Security;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{

    public class FielMembershipProvider : IDCMembershipProvider
    {
        string errorMessage = string.Empty;

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return this.errorMessage;
            }
        }

        public override bool ValidateUser(string userName, string serialNumber)
        {
            string mensajeValidacion = string.Empty;
            bool resultado = CertificateHelper.IsValidAutenticationCertificate(serialNumber, out mensajeValidacion);
            errorMessage = mensajeValidacion.Replace("\\n", "<br/>");
            return resultado;

        }
    }
}
