using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProDec_ReglaNegocio.ConectBD
{
    public static class Parametros
    {
        public static string DecryptionCertPath
        {
            get { return Utilerias.ConsultarBDConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionCertificatePath"); }
        }
        public static string UrlServicioConfiguracion
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("UrlServicioConfiguracion"); }
        }
        public static string DecryptionKey
        {
            get { return Utilerias.ConsultarBDConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionKey"); }
        }
        public static string DecryptionPassword
        {
            get { return Utilerias.ConsultarBDConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionPassword"); }
        }
        public static string DecryptionKeyPath
        {
            get { return Utilerias.ConsultarBDConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionPrivateKeyPath"); }
        }
        public static string RutaLocal
        {
            get { return Utilerias.ConsultarBDConfiguracion("Sat.DyP.IDE::RutaLocal"); }
        }
    }
}
