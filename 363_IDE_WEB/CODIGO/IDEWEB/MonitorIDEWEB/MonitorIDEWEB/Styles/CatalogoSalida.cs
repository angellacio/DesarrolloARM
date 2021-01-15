using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IdeMonitorService.Negocio.Contratos
{
    [DataContract]
    public class CatalogoSalida
    {
        [DataMember]
        public String IdCatalogo { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
    }
}