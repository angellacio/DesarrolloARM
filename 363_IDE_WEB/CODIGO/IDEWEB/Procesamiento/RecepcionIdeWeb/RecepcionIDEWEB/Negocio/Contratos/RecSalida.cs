using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RecepcionIDEWEB.Negocio.Contratos
{

        [DataContract]
        public class RecSalida
        {
            [DataMember]
            public int Folio
            { get; set; }
            [DataMember]
            public String Rfc
            { get; set; }
            [DataMember]
            public String Nombre
            { get; set; }
            [DataMember]
            public int Tamano
            { get; set; }
            [DataMember]
            public DateTime Fecha
            { get; set; }

        }
  
}