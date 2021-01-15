using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IdeMonitorService.Negocio.Contratos
{
    [DataContract]
    public class EstadisticaSalida
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
        [DataMember]
        public int Valor { get; set; }
    }
}