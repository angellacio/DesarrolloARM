using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    public enum EnumErroresProceso
    {
        NoExisteBanco = 1,
        ContenidoInvalido = 2,
        FechaInvalida = 3,
        BancoNoCoincide = 4,
        BancoEncabezado = 5,
        NumeroRegistros = 6,
        SumaImportes = 7,
        FechasOperacion = 8,
        VersionInvalida = 9,
        MontoInvalido = 10,
        SolicitudInvalida = 11,
        SinArchivoE = 12,
        TipoLineaInvalido = 13

    }
}
