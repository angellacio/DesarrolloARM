//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos.CatEsquemas:1:27/08/2013[Assembly:1.0:27/08/2013])


using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos;
using E = Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos
{
    public class CatEsquemas
    {
        private string cadenaConexion = (string)Catalogos.ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");

        public List<E.CatEsquemas> ObtenerLista(string idEsquema, string descripcion)
        {
            using (DalCatEsquemas dal = new DalCatEsquemas())
            {
                return dal.ObtenerLista(cadenaConexion, idEsquema, descripcion);
            }

        }

        public void Guardar(E.CatEsquemas.Accion accion, E.CatEsquemas Esquema)
        {
            try
            {
                using (DalCatEsquemas dal = new DalCatEsquemas())
                {
                    dal.Guardar(cadenaConexion, accion, Esquema);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(Int16 idEsquema)
        {
            try
            {
                using (DalCatEsquemas dal = new DalCatEsquemas())
                {
                    dal.Eliminar(cadenaConexion, idEsquema);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
