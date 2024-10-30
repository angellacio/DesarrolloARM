
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos.ExcepcionGeneracionLineaCaptura:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;

namespace Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos
{
    public class ExcepcionGeneracionLineaCaptura: Exception
    {
        public ExcepcionGeneracionLineaCaptura(string mensaje)
            : base(mensaje)
        {
        }
    }
}
