
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Seguridad:Sat.CreditosFiscales.Comunes.Entidades.Seguridad.InfoContribuyente:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Seguridad
{
    [Serializable]
    [DataContract]
    public class InfoContribuyente
    {
        [DataMember]
        public string RfcVigente { get; set; }

        [DataMember]
        public string RfcSolicitado { get; set; }

        [DataMember]
        public string BoId { get; set; }

        [DataMember]
        public string PersonId { get; set; }

        [DataMember]
        public List<string> Rfcs { get; set; }

        [DataMember]
        public bool PersonaFisica { get; set; }

        [DataMember]
        public int IdALR { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string ApellidoPaterno { get; set; }

        [DataMember]
        public string ApellidoMaterno { get; set; }

        [DataMember]
        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1} {2}", this.Nombre, this.ApellidoPaterno, this.ApellidoMaterno);
            }

            set
            {
            }

        }

        [DataMember]
        public string RazonSocial { get; set; }
    }
}

