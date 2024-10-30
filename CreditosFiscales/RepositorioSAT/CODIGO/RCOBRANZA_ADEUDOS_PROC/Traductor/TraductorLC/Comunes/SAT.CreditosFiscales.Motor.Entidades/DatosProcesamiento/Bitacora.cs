using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;

namespace SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento
{
    public class Bitacora
    {
        public ResultadoReglas entidad { get; set; }

        public Enumeraciones.PasosTraductor paso { get; set; }

        public bool escribirMensaje { get; set; }

        public string observaciones { get; set; }

        public Errores listaErrores { get; set; }

        public decimal duracion { get; set; }
    }
}
