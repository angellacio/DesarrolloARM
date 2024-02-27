using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    public class TblControlPagosDet
    {
       public long IdArchivo { get; set; }
        public int NumLinea { get; set; }
        public string Consecutivo { get; set; }
        public string LineaCaptura { get; set; }
        public string FechaPago { get; set; }
        public string HoraPago { get; set; }
        public string Importe { get; set; }
        public string NumOperacion { get; set; }
        public int MedioRecepcion { get; set; }
        public string Version { get; set; }
        public int TipoPago { get; set; }
        public int  IdEstado { get; set; }
        public long IdZip { get; set; }
        public string  NombreXML { get; set; }
        public int IdError { get; set; }
        //      public object NombreArchivo { get; set; }
        public string idProcesamiento { get; set; }

    }
}
