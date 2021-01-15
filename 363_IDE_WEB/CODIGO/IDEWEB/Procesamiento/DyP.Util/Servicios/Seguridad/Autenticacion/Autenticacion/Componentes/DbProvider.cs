using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAT.DyP.Negocio.Comun.Procesos;
using SAT.DyP.Negocio.Comun.Procesos.Autenticacion;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Security;
using SAT.DyP.Negocio.Comun.Procesos.Seguridad;

namespace SAT.DyP.Presentacion.Seguridad.Autenticacion.Componentes
{
    /// <summary>
    /// Clase que representa el servicio de Autenticación para base de datos contra
    /// la base de datos de SPED
    /// </summary>
    public class DbProvider : IAuthenticationProvider
    {
        #region IAuthenticationProvider Members
        public bool ValidateUser(string userName, string password)
        {
            return ValidateUserModule(userName, password, string.Empty);
        }

        public bool ValidateUserModule(string userName, string password,string profile)
        {
            bool resultado = false;

            try
            {
                VerificarUsuario acceso = new VerificarUsuario();
                resultado = acceso.ValidarAcceso(userName, password, profile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return resultado;
        }

        public string ObtenNombreRazonSocial(string rfc)
        {
            string _nombreRazonSocial = string.Empty;

            Contribuyente _contribuyente = ObtenerDatosContribuyente.Execute(rfc);

            if (_contribuyente.RazonSocial != null && _contribuyente.RazonSocial.Trim().Length > 0)
                _nombreRazonSocial = _contribuyente.RazonSocial;

            return _nombreRazonSocial;
        }

        public UserResponse ObtenDatosUsuario(string rfc)
        {
            Contribuyente user = ObtenerDatosContribuyente.Execute(rfc);

            string mail = string.Empty;
            string razonSocial = string.Empty;
            string password = string.Empty;

            if (user.RazonSocial != null && user.RazonSocial.Trim().Length > 0)
                razonSocial = user.RazonSocial;

            if (user.Email != null && user.Email.Trim().Length > 0)
                mail = user.Email;            

            UserResponse userResponse = new UserResponse(mail, password, razonSocial);
            return userResponse;
        }

        public bool ActualizaMail(string rfc, string email)
        {
            return ActualizaMailContribuyente.Execute(rfc, email);
        }

        public bool VerificarFielContribuyente(string rfc)
        {
          VerificarFIELContribuyente TheHelper = new VerificarFIELContribuyente();
          return TheHelper.Execute(rfc);
        }

        public FIELCertificateInfo ObtenerDatosCertificadoFIEL(string numeroSerie)
        {
            ObtenerDatosCertificadoFIEL processor = new ObtenerDatosCertificadoFIEL();
            CertificateInfo info = processor.Execute(numeroSerie);
            ValidadorCertificado validador = new ValidadorCertificado(info);

            return new FIELCertificateInfo(info.SerialNumber, info.ValidityEnd, validador.EsCertificadoActivo, validador.EsTipoFIEL,validador.EsTipoAutenticacion);
        }

        private bool _ValidarCIECFortalecida;
        public bool ValidarCIECFortalecida
        {
            get
            {
                return _ValidarCIECFortalecida;
            }

            set
            {
                _ValidarCIECFortalecida = value;
            }
        }

        #endregion
    }
}
