
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos.ExcepcionGeneracionFormato:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos
{
    /// <summary>
    /// Excepción para tipificar las validaciones no cumplidas en el proceso de almacenamiento de la solicitud de generación de formato de pago.
    /// </summary>
    public class ExcepcionGeneracionFormato: Exception
    {
        public ExcepcionGeneracionFormato(string mensaje)
            : base(mensaje)
        {
        }
    }
}
