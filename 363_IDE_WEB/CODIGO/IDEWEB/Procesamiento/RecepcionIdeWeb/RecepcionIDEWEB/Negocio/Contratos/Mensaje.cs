using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RecepcionIDEWEB.Negocio.Contratos
{

        [DataContract]
        public class Mensaje
        {

            [DataMember]
            public int IdTipoMensaje
            { get; set; }
            [DataMember]
            public int IdMensaje
            { get; set; }
            [DataMember]
            public string Descripcion
            { get; set; }
        }

}