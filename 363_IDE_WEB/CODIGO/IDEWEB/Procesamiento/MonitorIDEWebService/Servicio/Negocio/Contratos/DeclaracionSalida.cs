using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IdeMonitorService.Negocio.Contratos
{
    [DataContract]
    public class DeclaracionSalida
    {
        [DataMember]
        public int Folio { get; set; }
        [DataMember]
        public String Rfc { get; set; }
        [DataMember]
        public String NombreArchivo { get; set; }
        [DataMember]
        public String Banco { get; set; }
        [DataMember]
        public String TipoArchivo { get; set; }
        [DataMember]
        public String MedioRecepcion { get; set; }
        [DataMember]
        public DateTime fechaRecepcion { get; set; }
        [DataMember]
        public String UltimoEstado { get; set; }
        [DataMember]
        public String Sector { get; set; }
        [DataMember]
        public String Formato { get; set; }
        [DataMember]
        public String MotivoRechazo { get; set; }
        [DataMember]
        public String Estatus { get; set; }
        [DataMember]
        public String EstadoDeclaracion { get; set; }//ANUAL O  MENSUAL
        [DataMember]
        public String RazonSocial { get; set; }
        [DataMember]
        public int? Periodo { get; set; }
        [DataMember]
        public string sPeriodo { get; set; }
        [DataMember]
        public int? Operaciones { get; set; }
        [DataMember]
        public int? Ejercicio { get; set; }
        [DataMember]
        public DateTime fechaModificacion { get; set; }
        [DataMember]
        public String FormaDeclaracion { get; set; }//NORMAL O COMPLEMENTARIA
        [DataMember]
        public String Bitacora { get; set; }
    }

}