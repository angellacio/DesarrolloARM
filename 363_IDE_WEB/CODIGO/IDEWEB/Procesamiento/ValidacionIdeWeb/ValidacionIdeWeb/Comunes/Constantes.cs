using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionIdeWeb
{
    public static class Constantes
    {
        public static class Parametros
        {
            public const string BaseDatosIde = "Sat.DyP.IDE::BaseDatos";
        }

        public static class MensajeError
        {
            //MENSAJES DE ERROR EN VALIDACIÓN DEL NOMBRE DEL ARCHIVO
            public const int NomArchNull = 1;
            public const int NomArchLongitudNoValida = 2;
            public const int NomArchMensualAnualNoValida = 3;
            public const int NomArchRfcNoValido = 4;
            public const int NomArchPeriodoNoValido = 5;
            public const int NomArchEjercicioNoValido = 6;
            public const int NomArchTipoDeclaracionNoValido = 7;
            public const int NomArchNumeroComplementariaNoValido = 8;
            public const string NomArchFormatoNoValido = "formato no valido";

            //MENSAJES DE ERROR EN VALIDACIÓN ARCHIVO
            public const int ArchNull = 1;
            public const int ArchNoExite = 10;
            public const int ArchTamanioNoValido = 11;
            public const int ArchRfcNoValido = 12;
            public const int ArchEjercicioNoValido = 13;
            public const int ArchPeriodoNoValido = 14;
            public const int ArchDeclaracionNoCorrespondeAlContenido = 15;
            public const int ArchTipoDeclaracionNoCorrespondeAlContenido = 16;
            public const int ValidarAnioVersion = 17;
            public const int ValidaMontoExcedenteDepósitosNoCoincide = 18;
            public const int ValidaMontoOperacionesRegistrosDetalleMontoExcedenteDeclarado = 19;
            public const int ValidaMontoOperacionesRegistrosDetalleImpuestoDeterminado = 20;

            //MENSAJE DE ERROR DE RFC IGUAL

            public const int RfcAutenticadoNoCoincideNombreArchivo = 9;
        }

        public static class ExpresionRegular
        {
            public const string Rfc = "Sat.DyP.IDE::ExpresionRegular.Rfc";
            public const string FechaRfc = "Sat.DyP.IDE::ExpresionRegular.FechaRfc";
            public const string Periodo = "Sat.DyP.IDE::ExpresionRegular.Periodo";
            public const string Ejercicio = "Sat.DyP.IDE::ExpresionRegular.Ejercicio";
            public const string TipoDeclaracion = "Sat.DyP.IDE::ExpresionRegular.TipoDeclaracion";
            public const string NumeroComplementaria = "Sat.DyP.IDE::ExpresionRegular.NumeroComplementaria";
            public const string SinPeriodo = "Sat.DyP.IDE::ExpresionRegular.SinPeriodo";
            public const string DeclaracionMensual = "Sat.DyP.IDE::ExpresionRegular.DeclaracionMensual";
            public const string DeclaracionAnual = "Sat.DyP.IDE::ExpresionRegular.DeclaracionAnual";
            public const string DeclaracionNormal = "Sat.DyP.IDE::ExpresionRegular.DeclaracionNormal";
            public const string DeclaracionComplementaria = "Sat.DyP.IDE::ExpresionRegular.DeclaracionComplementaria";
            public const string AcuseRechazoAnual = "Sat.DyP.IDE::ExpresionRegular.AcuseRechazoAnual";
            public const string AcuseAceptacionAnual = "Sat.DyP.IDE::ExpresionRegular.AcuseAceptacionAnual";
            public const string AcuseRechazoMensual = "Sat.DyP.IDE::ExpresionRegular.AcuseRechazoMensual";
            public const string AcuseAceptacionMensual = "Sat.DyP.IDE::ExpresionRegular.AcuseAceptacionMensual";
            public const string NumeroNombreAcuse = "Sat.DyP.IDE::ExpresionRegular.NumeroNombreAcuse";
        }

        public static class Estatus
        {
            public const int rechazada = 5;
            public const int aceptada = 4;
            public const int almacenadaEnBaseDatos = 1;
            public const int recibidaProcesoValidacion = 0;
            public const int validacionConcluida = 2;
            public const int sellada = 3;
        }

        public static class Validar
        {
            public const string idTipoMensaje = "Sat.DyP.IDE::TipoMensajeValidacion";
            public const string pathXsdValidar = "Sat.DyP.IDE::Validar.pathXsdValidar";
            public const string pathXsdValidar3 = "Sat.DyP.IDE::Validar.pathXsdValidar.3";
            public const string pathXsltPlantilla = "Sat.DyP.IDE::Validar.pathXsltPlantilla";
            public const string longitudCharsNombre = "Sat.DyP.IDE::Validar.longitudCharsNombre";
            public const string formatoXML = "Sat.DyP.IDE::Validar.formatoXML";
            public const string formatoZIP = "Sat.DyP.IDE::Validar.formatoZIP";
            public const string sizeMaxArchivoInternet = "Sat.DyP.IDE::Validar.sizeMaxArchivoInternet";
            public const string sizeMaxArchivoInternetZIP = "Sat.DyP.IDE::Validar.sizeMaxArchivoInternetZIP";
            public const string sizeMaxArchivoModulo = "Sat.DyP.IDE::Validar.sizeMaxArchivoModulo";
            public const string sizeMaxArchivoModuloZIP = "Sat.DyP.IDE::Validar.sizeMaxArchivoModuloZIP";
            public const string sizeMinArchivoInternet = "Sat.DyP.IDE::Validar.sizeMinArchivoInternet";
            public const string sizeMinArchivoInternetZIP = "Sat.DyP.IDE::Validar.sizeMinArchivoInternetZIP";
            public const string sizeMinArchivoModulo = "Sat.DyP.IDE::Validar.sizeMinArchivoModulo";
            public const string sizeMinArchivoModuloZIP = "Sat.DyP.IDE::Validar.sizeMinArchivoModuloZIP";
            public const string ejercicioMinimo = "Sat.DyP.IDE::Validar.ejercicioMinimo";
            public const string fileshare = "Sat.DyP.IDE::RutaLocal";
            public const string directorioArchivosUnZip = "Sat.DyP.IDE::Validar.directorioArchivosUnZip";
            public const string versionAcuse = "Sat.DyP.IDE::Validar.versionAcuse";
            public const string pathAcuses = "Sat.DyP.IDE::Validar.pathAcuses";
            public const string claveErrorAcuse = "Sat.DyP.IDE::Validar.claveErrorAcuse";
            public const string medioRecepcionInternet = "Sat.DyP.IDE::Internet";
            public const string medioRecepcionModulo = "Sat.DyP.IDE::Modulo";
            public const string acuseEnumNormal = "Normal";
            public const string acuesEnumComplementaria = "Complementaria";
            public const string subjectAceptada = "Acuse de Aceptación de la Declaración ";
            public const string subjectRechazada = "Acuse de Rechazo de la Declaración ";
            public const int tipoArchivoDelcaracion = 1;
            public const string directorioEsquemas = "Esquemas";
            

        }

        public static class ElementoXml
        {
            public const string DeclaracionInformativaMensualIDE = "DeclaracionInformativaMensualIDE";
            public const string DeclaracionInformativaAnualIDE = "DeclaracionInformativaAnualIDE";
            public const string DeclaracionInformativaAnualISR = "DeclaracionInformativaAnualISR";
            public const string Complementaria = "Complementaria";
            public const string Normal = "Normal";
            public const string Totales = "Totales";
        }

        public static class AtributoXml
        {
            public const string RfcDeclarante = "rfcDeclarante";
            public const string Ejercicio = "ejercicio";
            public const string Periodo = "periodo";
            public const string OpAnterior = "opAnterior";
            public const string Denominacion = "denominacion";
            public const string Version = "version";
            public const string ImporteCheques = "importeCheques";
            public const string ImporteExcedenteDepositos = "importeExcedenteDepositos";
            public const string ImporteDeterminadoDepositos = "importeDeterminadoDepositos";
            public const string ImporteRecaudadoDepositos = "importeRecaudadoDepositos";
            public const string ImportePendienteDepositos = "importePendienteDepositos";
            public const string ImporteRemanenteDepositos = "importeRemanenteDepositos";
            public const string ImporteEnterado = "importeEnterado";
            public const string OperacionesRelacionadas = "operacionesRelacionadas";
        }

        public static class ElementoXmlAcuse
        {
            public const string AcuseAceptacionAnualIDE = "AcuseAceptacionAnualIDE";
            public const string AcuseAceptacionMensualIDE = "AcuseAceptacionMensualIDE";
            public const string AcuseRechazoAnualIDE = "AcuseRechazoAnualIDE";
            public const string AcuseRechazoMensualIDE = "AcuseRechazoMensualIDE";
            public const string Error = "Error";
        }

        public static class AtributoXmlAcuse
        {
            public const string Version = "version";
            public const string Rfc = "rfc";
            public const string Denominacion = "denominacion";
            public const string FechaPresentacion = "fechaPresentacion";
            public const string FolioRecepcion = "folioRecepcion";
            public const string NumeroOperacion = "numeroOperacion";
            public const string NombreArchivo = "nombreArchivo";
            public const string TamanoArchivo = "tamanoArchivo";
            public const string Ejercicio = "ejercicio";
            public const string Periodo = "periodo";
            public const string Tipo = "tipo";
            public const string TotalRecaudado = "totalRecaudado";
            public const string TotalEnterado = "totalEnterado";
            public const string Sello = "sello";
            public const string FechaRechazo = "fechaRechazo";
            public const string Clave = "clave";
            public const string Descripcion = "descripcion";
            public const string FechaHoraEmisionAcuse = "fechaHoraEmisionAcuse";
            public const string CadenaOriginal = "cadenaOriginal";
        }

        public static class Excepcion
        {
            public const string selloDigital = "Error en el sistema, no se generó el Sello Digital.";
            public const string envioMail = "No se envió el correo de notificación, debido que los datos estan incompletos.";
            public const string archivoZipNoValido = "No es un archivo .zip válido.";
            public const string contenidoArchivoZipNoProcesable = "El contendio del archivo .zip no es procesable (No contiene un archivo .xml).";
            public const string errorProcesoDescompresion = "Ocurrió un error en el proceso de descompresión.";
            public const string validacionNoConcluida = "La validación no fue concluida.";
        }
    }
}