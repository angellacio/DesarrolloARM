using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IdeMonitorService.Negocio.Contratos
{
    [DataContract]
    public class EstadisticaEntrada
    {	
        [DataMember]
        public String TipoArchivo { get; set; }
        [DataMember]
        public String MedioRecepcion { get; set; }
        [DataMember]
        public String Periodo { get; set; }
        [DataMember]
        public String fechaInicio { get; set; }
        [DataMember]
        public String fechaFin { get; set; }
        [DataMember]
        public String Rfc { get; set; }
    }
}
    