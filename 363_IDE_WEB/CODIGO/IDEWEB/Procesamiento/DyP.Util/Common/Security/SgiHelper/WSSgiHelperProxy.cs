//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:WSSgiHelperProxy:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Security.wsSgiHelper;
using SAT.DyP.Util.Configuration;

namespace SAT.DyP.Util.Security
{
    /// <summary>
    /// Clase de interaccion con SgiHelpers mediante WebService
    /// </summary>
    public class WSSgiHelperProxy:ISgiHelperProvider
    {
        private wsSgiHelper.wsSgiHelper _helper = null;
        private string KEY_CONFIG = "SAT.DyP.Util::WS_SGI_HELPER";

        public WSSgiHelperProxy()
        {
            _helper=new SAT.DyP.Util.Security.wsSgiHelper.wsSgiHelper();            
            _helper.Url = ConfigurationManager.ApplicationSettings.ReadSetting(KEY_CONFIG);
        }
        
        public  bool EsFirmaValida(string numeroSerie,string cadenaOriginal,string selloDigital)
        {
            return _helper.EsFirmaValida(numeroSerie, cadenaOriginal, selloDigital);
        }

        public  string GenerarSelloDigital(string cadenaOriginal)
        {
            return _helper.GenerarSelloDigital(cadenaOriginal);
        }

        public  bool VerificarFIELContribuyente(string rfc)
        {
            return _helper.VerificarFIELContribuyente(rfc);
        }

        public  CertificateInfo ObtenerCertificado(string numeroSerie)
        {
            SAT.DyP.Util.Security.wsSgiHelper.CertificateInfo result = null;

            result= _helper.ObtenerCertificado(numeroSerie);

            CertificateInfo certificateInfo = new CertificateInfo();
            
            certificateInfo.SerialNumber = result.SerialNumber;
            certificateInfo.State = (CertificateState)System.Enum.Parse(typeof(CertificateState), result.State.ToString());
            certificateInfo.TaxPayerID = result.TaxPayerID;
            certificateInfo.TaxPayerName = result.TaxPayerName;
            certificateInfo.Type = (CertificateType)result.Type;
            certificateInfo.ValidityEnd = result.ValidityEnd;
            certificateInfo.ValidityStart = result.ValidityStart;

            return certificateInfo;
        }

        public  string DesencriptarDeclaracion(string nombreArchivo,string contenidoDocumento,int medioPresentacion,string rfcAutenticacion,string rfc)
        {
            return _helper.DesencriptarDeclaracion(nombreArchivo, contenidoDocumento, medioPresentacion, rfcAutenticacion, rfc);
        }

        public void Dispose()
        {
            if(_helper!=null)
               _helper.Dispose();
        }
            
    }
}
