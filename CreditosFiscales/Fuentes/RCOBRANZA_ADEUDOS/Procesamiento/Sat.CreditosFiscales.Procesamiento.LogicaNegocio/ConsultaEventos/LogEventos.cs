﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Datos.AccesoDatos.ConsultaEventos;
using Sat.CreditosFiscales.Datos.AccesoDatos.Servicios;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor;


//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ConsultaEventos:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ConsultaEventos.LogEventos:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ConsultaEventos
{
    /// <summary>
    /// Clase para el manejo del log de eventos
    /// </summary>
    public class LogEventos
    {
        /// <summary>
        /// Clase que consulta la información de la tabla AppLog
        /// </summary>
        /// <param name="aplicacion">Nombre de la aplicación</param>
        /// <param name="porTicket">Ticket del error</param>
        /// <param name="porFechaInicio">Fecha incial para la busqueda</param>
        /// <param name="porFechaFin">Fecha final para la busqueda</param>
        /// <returns></returns>
        public static List<LogEvento> BuscarEventos(string aplicacion, string porTicket, string porFechaInicio, string porFechaFin)
        {
            string cadenaDeConexion = string.Empty;
            switch (aplicacion)
            {
                case "Traductor":
                    cadenaDeConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
                    break;
                case "Templates":
                    cadenaDeConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrTemplates");
                    break;
                default:
                    cadenaDeConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrCreditosFiscales");
                    break;
            }

            return new DalLogEventos().BuscarEventos(cadenaDeConexion, porTicket, porFechaInicio, porFechaFin);
        }

        /// <summary>
        /// Valida que el usuario y password ingresados puedan consultar las paginas de los logs
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        public static bool VerificaAcceso(string usuario, string contraseña)
        {
            return new DalLogEventos().VerificaAcceso(usuario, contraseña);
        }

        public static bool CambiaPassword(string usuario, string contraseña)
        {
            return new DalLogEventos().CambiaPassword(usuario, contraseña);
        }

        /// <summary>
        /// Busca los registros de las peticiones a otros sistemas en la tabla Peticion de acuerdo a los criterios de busqueda ingresados
        /// </summary>
        /// <param name="porRfc">Rfc del controbuyente</param>
        /// <param name="porFechaInicio">Fecha inicial</param>
        /// <param name="porFechaFin">Fecha final</param>
        /// <param name="conError">Indica si deberan recuperar unicamente las peticiones con error</param>
        /// <returns>Peticiones que cumplen con el criterio de busqueda</returns>
        public static List<Peticion> BuscarPeticiones(string porRfc, string porFechaInicio, string porFechaFin, int conError)
        {
            return new DalLogEventos().BuscarPeticiones(porRfc, porFechaInicio, porFechaFin, conError);
        }

        /// <summary>
        /// Busca los registros de procesamiento del traductor  de acuerdo a los criterios de busqueda ingresados
        /// </summary>
        /// <param name="porIdAplicacion">Identificador de la aplicación</param>
        /// <param name="porIdTipoDocumento">Identificador del tipo de documento</param>
        /// <param name="porFechaInicio">Fecha incial</param>
        /// <param name="porFechaFin">Fecha final</param>
        /// <param name="conError">Indica si deberan recuperar unicamente las peticiones con error</param>
        /// <returns></returns>
        public static List<TraductorBitacora> BuscarEnBitacora(int porIdAplicacion, int porIdTipoDocumento, DateTime porFechaInicio, DateTime porFechaFin, int conError, string porIdProcesamiento, string porRfc, int porIdPaso)
        {
            string cadenaDeConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            return new DalLogEventos().BuscarBitacora(cadenaDeConexion, porIdAplicacion, porIdTipoDocumento, porFechaInicio, porFechaFin, conError, porIdProcesamiento, porRfc, porIdPaso);
        }

        /// <summary>
        /// Ontiene los catalogos para mostrar los filtros de busqueda
        /// </summary>
        /// <param name="idCatalogo">Identificador del catálogo</param>
        /// <returns>Catálogo</returns>
        public static Dictionary<int, string> ObtenerCatalogo(int idCatalogo)
        {
            string cadenaDeConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            return new DalLogEventos().ObtenerCatalogo(cadenaDeConexion, idCatalogo);

        }


    }
}
