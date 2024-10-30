
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Servicios:SAT.CreditosFiscales.Motor.Servicios.ICreditosFiscales:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SAT.CreditosFiscales.Motor.Servicios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICreditosFiscales" in both code and config file together.
    [ServiceContract]
    public interface ICreditosFiscales
    {
        /// <summary>
        /// Genera una línea de captura
        /// </summary>
        /// <param name="idAplicacion">Aplicación que envía la petición</param>
        /// <param name="idTipoDoc">Tipo de documento que se envía</param>
        /// <param name="documento">Documento Xml con los datos necesarios</param>        
        /// <returns>string con forma XML que contiene la lista de errores o datos linea captura</returns>
        [OperationContract]      
        string ObtieneLineaCaptura(short idAplicacion, short idTipoDoc, string documento);

        /// <summary>
        /// Genera una línea de captura y pdf
        /// </summary>
        /// <param name="idAplicacion">Aplicación que envía la petición</param>
        /// <param name="idTipoDoc">Tipo de documento que se envía</param>
        /// <param name="documento">Documento Xml con los datos necesarios</param>
        /// <param name="generarPDF">generaPDF = true si requiere generar PDF ó false si no lo requiere</param>               
        [OperationContract]
        string ObtieneLineaCapturaConPDF(short idAplicacion, short idTipoDoc, string documento);

        /// <summary>
        /// Genera una Línea de Captura que se guardará en una imagen com formato PNG.
        /// </summary>
        /// <param name="idAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
        /// <param name="idTipoDoc">Identificador único (ID) del Tipo de Documento que se envía.</param>
        /// <param name="documento">Cadena de caracter en formato XML con los datos necesarios para generar la Línea de Captura.</param>
        [OperationContract]
        string ObtieneLineaCapturaConImagen(short idAplicacion, short idTipoDoc, string documento);

        /// <summary>
        /// Consulta las líneas de captura generadas por documento
        /// </summary>
        /// <param name="documentos">Documentos a buscar</param>
        /// <returns>Líneas de captura generadas</returns>
        [OperationContract]
        string ConsultaLineasCapturaXDocumento(string documentos);

        /// <summary>
        /// Obtiene los documentos de una línea de captura asignada
        /// </summary>
        /// <param name="lineaCaptura">línea de captura a buscar</param>
        /// <returns>Datos del documento de la línea de captura asignada</returns>
        [OperationContract]
        string ObtieneDocumentosEnLineaCaptura(string lineaCaptura);

        [OperationContract]
        string ObtieneXMLOriginal(string Folio);

        [OperationContract]
        string ObtieneListaFolios(string RFC);

        [OperationContract]
        string ObtienePDF(string LineaCaptura);
        [OperationContract]
        String ObtieneConsultaFormatos(string ADRRFC, string Folio, string LineaDeCaptura, string No_de_Resolucion, int rango_de_emision_FechaPago, DateTime fecha_ini, DateTime fecha_fin);
        [OperationContract]
        string ObtienePDFXFolioyLineadeCaptura(string Folio, string LineaCaptura);
    }
}
