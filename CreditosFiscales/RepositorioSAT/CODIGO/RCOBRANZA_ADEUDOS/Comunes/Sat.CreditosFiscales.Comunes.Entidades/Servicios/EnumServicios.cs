
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Servicios:Sat.CreditosFiscales.Comunes.Entidades.Servicios.EnumTipoOrigen:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios
{
    public enum EnumTipoOrigen
    {
        ARCA = 1,
        Traductor
    }
    public enum EnumAccionesArca
    {
        ObtenerInformacionContribuyente = 1,
        ObtenerInformacionContribuyentePuro
    }
    public enum EnumAccionesTraductor
    {
        GenerarLC = 1,
        ConsultarLCMarcados,
        RecuperaResolucuionesEnLC,
        Imprimir
    }

    public enum EnumAlertas
    {   
    }
    public enum EnumInformacion
    {
    }
}
