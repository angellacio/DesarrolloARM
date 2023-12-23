
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos.ClaveComputo:1:12/07/2013[Assembly:1.0:12/07/2013])


using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos
{
    public class ClaveComputo
    {
        private string cadenaConexion = (string)Catalogos.ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");

        public List<CatClaveComputo> ObtenerLista(string claveComputo)
        {
            using (DalClaveComputo dal = new DalClaveComputo())
            {
                return dal.ObtenerLista(cadenaConexion, claveComputo);
            }

        }

        public void Guardar(CatClaveComputo.Accion accion, CatClaveComputo ClaveComputo)
        {
            try
            {
                using (DalClaveComputo dal = new DalClaveComputo())
                {
                    dal.Guardar(cadenaConexion, accion, ClaveComputo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(string claveComputo)
        {
            try
            {
                using (DalClaveComputo dal = new DalClaveComputo())
                {
                    dal.Eliminar(cadenaConexion, claveComputo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
