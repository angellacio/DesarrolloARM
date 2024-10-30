
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios:Sat.CreditosFiscales.Procesamiento.Servicios.ServicioLog:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Procesamiento.Servicios
{

    public class ServicioLog : IServicioLog
    {
        /// <summary>
        /// Registra un evento en el Log
        /// </summary>
        /// <param name="eventoID">Identificador del evento</param>
        /// <param name="mensaje">Mensaje</param>
        /// <param name="eventoLogTipo">Tipo de evento<see cref="EventLogEntryType"/></param>
        /// <param name="aplicacion">Nombre de la aplicación</param>
        public string EscribirEntradaLog(int eventoID, string mensaje, System.Diagnostics.EventLogEntryType eventoLogTipo, string aplicacion)
        {
            return LogEventos.EscribirEntradaLog(eventoID, mensaje, eventoLogTipo, aplicacion);
        }

       
        public void Test()
        {
            

            ProxyManagerTraductor.TestInvocarAlTraductorMarcados();
            ProxyManagerTraductor.RecuperaDocumentosEnLC("01130004437794627430", "OAND831188N1");


            //ProxyManagerArca.ObtenerInformacionContribuyente("OAND8311188N1", new List<string>() { "LOME690301CN3" });
            ////ProxyManagerTraductor.SerializaYDeserializaSolicitudCreditosFiscales();
            //var catDI= Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos.Catalogo.ObtenerCatalogoDiaInhabil();
            //var catFI = Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos.Catalogo.ObtenerCatalogoFechaINPC();

            

            

            //Encripcion encripcion = new Encripcion();
            //string strEnc = encripcion.ObtieneMD5("12345");

        }
    }
}
