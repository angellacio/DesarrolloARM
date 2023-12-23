
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraFormato:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraFormato.GeneraFormato:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraFormato
{
    public class GeneraFormato
    {
        public Int64 GuardaSolicitud(Solicitud solicitud)
        {
            try
            {
                using (DalGeneracionFormato dal = new DalGeneracionFormato())
                {
                    return dal.GuardaSolicitud(solicitud);
                }
            }           
            catch (ExcepcionGeneracionFormato err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresGeneracionFormato.ErrorListasInfo, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
            catch (Exception err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresGeneracionFormato.ErrorGenerico, err);
                throw new ExcepcionTipificada("No se logró generar la solicitud, por favor intentelo más tarde", err, ticket);
            }
        }

        public Solicitud ObtenerSolicitud(Int64 idSolicitud)
        {
            Solicitud solicitud = null;
            try
            {
                using (DalSolicitud dal = new DalSolicitud())
                {
                    solicitud = dal.ObtenerSolicitud(idSolicitud);
                }
            }
            catch (ExcepcionGeneracionFormato err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresGeneracionFormato.ErrorListasInfo, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
            catch (Exception err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresGeneracionFormato.ErrorGenerico, err);
                throw new ExcepcionTipificada("No se logró obtener la solicitud, por favor intentelo más tarde", err, ticket);
            }
            return solicitud;
        }
       
    }
}
