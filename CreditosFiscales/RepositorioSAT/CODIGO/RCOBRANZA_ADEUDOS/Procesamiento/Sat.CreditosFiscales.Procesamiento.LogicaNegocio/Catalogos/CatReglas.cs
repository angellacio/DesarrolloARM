//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos.CatReglas:1:27/08/2013[Assembly:1.0:27/08/2013])


using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos;
using E = Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos
{
    public class CatReglas
    {
        private string cadenaConexion = (string)Catalogos.ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");

        public List<E.CatReglas> ObtenerLista(string idRegla, string descripcion)
        {
            using (DalCatReglas dal = new DalCatReglas())
            {
                return dal.ObtenerLista(cadenaConexion, idRegla, descripcion);
            }

        }

        public void Guardar(E.CatReglas.Accion accion, E.CatReglas Regla)
        {
            try
            {
                using (DalCatReglas dal = new DalCatReglas())
                {
                    dal.Guardar(cadenaConexion, accion, Regla);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(Guid idRegla)
        {
            try
            {
                using (DalCatReglas dal = new DalCatReglas())
                {
                    dal.Eliminar(cadenaConexion, idRegla);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
