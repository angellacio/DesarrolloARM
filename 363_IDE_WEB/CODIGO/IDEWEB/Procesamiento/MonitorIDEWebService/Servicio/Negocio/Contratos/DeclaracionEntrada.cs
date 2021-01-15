using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IdeMonitorService.Negocio.Contratos
{
    [DataContract]
    public class DeclaracionEntrada
    {
        [DataMember]
        public String Estatus { get; set; }
        [DataMember]
        public int Folio { get; set; }
        [DataMember]
        public String NombreArchivo { get; set; }
        [DataMember]
        public String Rfc { get; set; }
        [DataMember]
        public String Banco { get; set; }
        [DataMember]
        public String TipoArchivo { get; set; }
        [DataMember]
        public String MedioRecepcion { get; set; }
        [DataMember]
        public String fechaInicio { get; set; }
        [DataMember]
        public String fechaFin { get; set; }
        [DataMember]
        public String UltimoEstado { get; set; }
        [DataMember]
        public String Sector { get; set; }
        [DataMember]
        public String Formato { get; set; }
        [DataMember]
        public String MotivoRechazo { get; set; }
        [DataMember]
        public String RazonSocial { get; set; }
        [DataMember]
        public int Periodo { get; set; }
        [DataMember]
        public int Ejercicio { get; set; }
        [DataMember]
        public int Operaciones { get; set; }
        [DataMember]
        public String FormaDeclaracion { get; set; }
        [DataMember]
        public String EstadoDeclaracion { get; set; }
        [DataMember]
        public int Inicio { get; set; }
        [DataMember]
        public int Bloque { get; set; }
    }
}