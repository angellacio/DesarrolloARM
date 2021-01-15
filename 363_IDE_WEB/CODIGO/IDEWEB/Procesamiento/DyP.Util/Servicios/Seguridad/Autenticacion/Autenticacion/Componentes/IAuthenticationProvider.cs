using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Security;

namespace SAT.DyP.Presentacion.Seguridad.Autenticacion.Componentes
{
    /// <summary>
    /// Interface que expone los servicios de autenticación para SCADE
    /// </summary>
    public interface IAuthenticationProvider
    {
        bool ValidateUser(string userName, string password);
        bool ValidateUserModule(string userName,string password,string profile);
        string ObtenNombreRazonSocial(string rfc);
        UserResponse ObtenDatosUsuario(string rfc);
        bool ActualizaMail(string rfc, string email);
        bool VerificarFielContribuyente(string rfc);
        FIELCertificateInfo ObtenerDatosCertificadoFIEL(string certificateSerialNumber);
        bool ValidarCIECFortalecida { set; get; }
    }
}
