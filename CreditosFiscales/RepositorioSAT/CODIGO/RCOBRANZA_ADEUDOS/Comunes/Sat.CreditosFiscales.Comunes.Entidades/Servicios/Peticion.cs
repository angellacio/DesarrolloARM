
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Servicios:Sat.CreditosFiscales.Comunes.Entidades.Servicios.Peticion:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios
{
    public class Peticion
    {
        
        public int TipoOrigen { get; set; }
        public string TipoOrigenDescripcion { get; set; }
        public int Accion { get; set; }
        public string AccionDescripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string RFC { get; set; }
        public bool HuboError { get; set; }
        public string XmlPeticion { get; set; }
        public string XmlRespuesta { get; set; }
        public decimal Duracion { get; set; }
        
        public string Observaciones { get; set; }
    }
}
