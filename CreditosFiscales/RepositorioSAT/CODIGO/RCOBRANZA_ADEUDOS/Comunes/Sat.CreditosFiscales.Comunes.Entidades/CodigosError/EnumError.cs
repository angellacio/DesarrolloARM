
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.CodigosError:Sat.CreditosFiscales.Comunes.Entidades.CodigosError.CodigosError:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Comunes.Entidades.CodigosError
{

    public enum EnumErroresArca
    {
        ErrorAlInvocarARCA = 1000,
        ErrorAlInvocarArcaContribuyentePuro,
        ErrorAlMapearDocumentoArcaADocumentoCF,
        ErrorAlMapearContribuyentePuro

    }
    public enum EnumErroresTraductor
    {
        ErrorGenerico = 1050,
        ErrorMarcados = 1051,
        ErrorNoMarcados = 1052,
        ErrorConsultaDocumentosEnLC =1053,
        ErrorNoMarcadosDeserializacion = 1054,
    }

    public enum EnumErroresPagos
    {
        ErrorAlIniciarAplicacion = 2000,
        ErrorAlCerrarAplicacion = 2001,
        ErrorEjecucionPagos = 2002,
        ErrorAlObtenerInforBaseDatos = 2003,
        ErrorObtenerArchivos = 2004,
        ErrorProcesandoArchivos = 2005,
        ErrorAplicandoReglasValidacion = 2006,
        ErrorAplicarValidacionDetalle = 2007,
        ErrorCrearArchivoError = 2008,
        ErrorObtenerInfoBaseDeDatos = 2009,
        ErrorGuardarArchivosConError = 2010,
        ErrorValidandoLineasCaptura = 2011,
        ErrorActualizarLineasCaptura = 2012,
        ErrorOrganizarRegistroPorLocal = 2013,
        ErrorInsertarDetallePagos = 2014,
        ErrorObtenerIdProceso = 2015,
        ErrorMoverArchivoConError = 2016,
        ErrorMoverArchivoProcExitoso = 2017,
        ErrorActualizarFechaFin = 2018,
        ErrorCrearArchivoSalida = 2019,
        ErrorCrearProcArchivoSalida = 2020,
        ErrorProcesarTransaccionesVirtuales=2021
    }

    public enum EnumErroresLineaCaptura
    {
        ErrorGenerico = 1100,
        ErrorAlGenerarLineasDeCaptura=1101,
        ErrorAlGenerarLineasDeCapturaMarcados = 1102,
        ErrorAlGenerarLineasDeCapturaNoMarcados,
        ErrorAlGenerarLineasDeCapturaAgrupador=1004,
        ErrorAlGenerarLineasDeCapturaMarcadosTraductor,
        ErrorAlGenerarLineasDeCapturaNoMarcadosTraductor,
        ErrorAlObtenerUsuarioDeudorPuro,
        ErrorAlGenerarLineasDeCapturaNegocio,
        ErrorAlGenerarLineasDeCapturaNegocioSoloMarcados,
        
        
        ErrorAlGenerarArhivo = 1115
    }

    public enum EnumErroresGeneracionFormato
    {
        ErrorGenerico = 1200,
        ErrorInsercion = 1201,
        ErrorListasInfo = 1202,
        ErrorCargaInformacion = 1203,
        ErrorCargaPaginaPublica = 1204,
        ErrorDudoresPurosGenerico = 1205
    }

    public enum EnumErroresIDC
    {
        ErrorAlInvocarIDC = 1300,
        ErrorNoExisteInformacionIDC = 1302,
        ErrorNoExistenRolesIDC = 1303,
        ErrorNoExisteBOID = 1304,
        ErrorNoExisteRazonSocialoNombre=1305,
        ErrorNoExisteALR=1306,
        ErrorNoExisteParametroIdc = 1307,
    }

    public enum EnumErroresInfraestructura
    {
        ErrorAlGuardarPeticiones = 1500,
        ErrorAlConsultarPeticiones = 1501
    }
}


