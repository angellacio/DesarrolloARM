
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos.Aplicacion:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos
{
    /// <summary>
    /// Clase de acceso a datos de la aplicación
    /// </summary>
    public class Aplicacion
    {
        public const string SP_CONSULTAAPLICACIONES = "pObtenerAplicaciones";

        /// <summary>
        /// Consulta el catálogo de aplicaciones del motor de aplicación
        /// </summary>
        /// <returns>Lista de aplicaciones</returns>
        public static List<Entidades.Catalogos.Aplicacion> ConsultaAplicaciones()
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_CONSULTAAPLICACIONES);
            IDataReader rdAplicacion = null;
            List<Entidades.Catalogos.Aplicacion> lstAplicaciones = new List<Entidades.Catalogos.Aplicacion>();

            using (rdAplicacion = db.ExecuteReader(cmd))
            {

                while (rdAplicacion.Read())
                {

                    lstAplicaciones.Add(
                        new Entidades.Catalogos.Aplicacion
                        {
                            IdAplicacion = Convert.ToInt32(rdAplicacion["IdAplicacion"]),
                            Nombre = rdAplicacion["Nombre"].ToString()
                        }
                        );
                }
            }

            return lstAplicaciones;
        }
    }
}
