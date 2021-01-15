
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:Constantes:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Clase contenedora de constantes
    /// </summary>
    public class Constantes
    {
        //Constantes que aplican para la tabla TMensajes
        public const string MSG_ENTIDAD_ANUAL = "ANUAL_TMensajes";
        public const string MSG_ENTIDAD_DIOT = "DIOT_CDOTMensajes";
        public const string MSG_ENTIDAD_CEROS = "CEROS_TMensajes";        


        //Constantes que aplican para la tabla TMsgNotifica
        public const string MSG_NOTIFY_ANUAL = "ANUAL_TMsgNotifica";
        public const string MSG_NOTIFY_DIOT = "DIOT_CDOTNotificacion";


        public const string CONFIG_SIGN_CERTIFICATE_SERIAL_NUMBER = "SAT.DyP.Util.Security::SigningCertificateSerialNumber";
        public static string SELLO_DIGITAL_HOSTNAME = "SAT.DyP.Negocio.Comun::DIGITALSEAL_HOSTNAME";
        public static string SELLO_DIGITAL_SERVICENAME = "SAT.DyP.Negocio.Comun::DIGITALSEAL_SERVICENAME";
    }
}
