
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:ISgiHelperProvider:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Util.Security
{
    public interface ISgiHelperProvider
    {
        bool EsFirmaValida(string numeroSerie, string cadenaOriginal, string selloDigital);
        string GenerarSelloDigital(string cadenaOriginal);
        bool VerificarFIELContribuyente(string rfc);
        CertificateInfo ObtenerCertificado(string numeroSerie);
        string DesencriptarDeclaracion(string nombreArchivo, string contenidoDocumento, int medioPresentacion, string rfcAutenticacion, string rfc);
        void Dispose();
    }
}
