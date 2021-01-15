using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RecepcionIDEWEB.Negocio.Contratos
{
        [DataContract]
        public class RecVerifica
        {
            [DataMember]
            public int Existencia
            { get; set; }
            [DataMember]
            public int Folio
            { get; set; }
            [DataMember]
            public DateTime Fecha
            { get; set; }

        }

}