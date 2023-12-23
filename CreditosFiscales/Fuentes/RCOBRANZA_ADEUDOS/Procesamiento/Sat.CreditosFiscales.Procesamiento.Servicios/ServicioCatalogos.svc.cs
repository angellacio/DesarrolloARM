
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios:Sat.CreditosFiscales.Procesamiento.Servicios.ServicioCatalogos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;



namespace Sat.CreditosFiscales.Procesamiento.Servicios
{

    public class ServicioCatalogos : IServicioCatalogos
    {

        public List<E.Autoridad> ObtenerCatalogoAutoridad()
        {
            return Catalogo.obtenerCatalogoAutoridad();
        }

        public E.ALR ObtenerAlrXCveCorta(string CveCorta)
        {
            return Catalogo.ObtenerAlrXCveCorta(CveCorta);
        }


        public E.DescripcionConceptoPeriodicidad ObtenerDescripcionConceptoPeriodicidad(int idConceptoLey, int idPeriodo, int idPeriodicidad)
        {
            return Catalogo.ObtenerDescripcionConceptoPeriodicidad(idConceptoLey, idPeriodo, idPeriodicidad);
        }


        public List<E.ALR> ObtenerCatalogoAlr()
        {
            return Catalogo.obtenerCatalogoALR();
        }


        public object ConsultaConfiguracion(string settingName)
        {
            return ApplicationSettings.ConsultaConfiguracion(settingName);
        }

        #region Clave Computo
        public List<E.CatClaveComputo> ListaClaveComputo(string claveComputo = "")
        {
            try
            {
                return new ClaveComputo().ObtenerLista(claveComputo);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }
        public void GuardarClaveComputo(E.CatClaveComputo.Accion accion, E.CatClaveComputo ClaveComputo)
        {
            try
            {
                new ClaveComputo().Guardar(accion, ClaveComputo);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }
        public void EliminarClaveComputo(string claveComputo)
        {
            try
            {
                new ClaveComputo().Eliminar(claveComputo);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }

        #endregion

        #region Catalogo de Reglas
        public List<E.CatReglas> ListaCatReglas(string idRegla, string descripcion = "")
        {
            try
            {
                return new CatReglas().ObtenerLista(idRegla, descripcion);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }

        public void GuardarRegla(E.CatReglas.Accion accion, E.CatReglas Regla)
        {
            try
            {
                new CatReglas().Guardar(accion, Regla);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }
        public void EliminarRegla(Guid idRegla)
        {
            try
            {
                new CatReglas().Eliminar(idRegla);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }
        #endregion

        #region Catalogo de Esquemas
        public List<E.CatEsquemas> ListaCatEsquemas(string idEsquema, string descripcion = "")
        {
            try
            {
                return new CatEsquemas().ObtenerLista(idEsquema, descripcion);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }

        public void GuardarEsquema(E.CatEsquemas.Accion accion, E.CatEsquemas Esquema)
        {
            try
            {
                new CatEsquemas().Guardar(accion, Esquema);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }
        public void EliminarEsquema(Int16 idEsquema)
        {
            try
            {
                new CatEsquemas().Eliminar(idEsquema);
            }
            catch (Exception ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Message);
                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
        }
        #endregion
    }
}
