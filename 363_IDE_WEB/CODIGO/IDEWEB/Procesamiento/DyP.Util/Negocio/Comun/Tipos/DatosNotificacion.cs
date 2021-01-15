
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:DatosNotificacion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase que representa la entidad datos de notificación
    /// </summary>
    [Serializable]    
    public abstract class DatosNotificacion
    {          
        private string _rfc;

        [XmlElement("RFC")]
        public string RFC
        {
            set { _rfc = value; }
            get { return _rfc; }
        }

        private string _rfcAu;

        [XmlElement("RFCAutenticacion")]
        public string RFCAutenticacion
        {
            set { _rfcAu = value; }
            get { return _rfcAu; }
        }    

        private string _razonSocial;

        [XmlElement("RazonSocial")]
        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }
        
        private string _fechaPresentacion;

        [XmlElement("FechaPresentacion")]
        public string FechaPresentacion
        {
            get { return _fechaPresentacion; }
            set { _fechaPresentacion = value; }
        }

        private string _horaPresentacion;

        [XmlElement("HoraPresentacion")]
        public string HoraPresentacion
        {
            set { _horaPresentacion = value; }
            get { return _horaPresentacion; }
        }

        private long _folio;

        [XmlElement("FolioProceso")]
        public long Folio
        {
            get { return _folio; }
            set { _folio = value; }
        }

        private string _numeroOperacion;

        [XmlElement("NumeroOperacion")]
        public string NumeroOperacion
        {
            get { return _numeroOperacion; }
            set { _numeroOperacion = value; }
        }

        private string _cadenaOriginal;

        [XmlElement("CadenaOriginal")]
        public string CadenaOriginal
        {
            get { return _cadenaOriginal; }
            set { _cadenaOriginal = value; }
        }

        private string _selloDigital;

        [XmlElement("SelloDigital")]
        public string SelloDigital
        {
            get { return _selloDigital; }
            set { _selloDigital = value; }
        }
    }
}
