using System;

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor
{
    public class TraductorBitacora
    {
        public string IdProcesamiento { set; get; }
        public DateTime FechaRegistro { set; get; }
        public int IdPasoProceso { set; get; }
        public string Observaciones { set; get; }
        public string Mensaje { set; get; }
        public string Errores { set; get; }
        public string Aplicacion { set; get; }
        public string TipoDocumento { set; get; }
        public string Duracion { set; get; }
    }
}
