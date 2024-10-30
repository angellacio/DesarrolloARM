
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos.DalCatMensaje:1:12/07/2012[Assembly:1.0:12/07/2013])

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
    /// Clase de acceso a datos para obtener información de mensajes
    /// </summary>
    public class DalCatMensaje
    {
        public const string SP_CONSULTAMENSAJE = "pConsultaMensajes";

        /// <summary>
        /// Consulta los mensajes de error que regresa la aplicación
        /// </summary>
        /// <param name="Parametro">parámetro a consultar</param>
        /// <returns>Entidad CatMensajes</returns>
        public static Entidades.Catalogos.CatMensajes ConsultaMensaje(string Parametro)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_CONSULTAMENSAJE);
            IDataReader rdMensajes = null;
            Entidades.Catalogos.CatMensajes mensajeError = new Entidades.Catalogos.CatMensajes();

            db.AddInParameter(cmd, "@parametroMensaje", System.Data.DbType.String, Parametro);

            using (rdMensajes = db.ExecuteReader(cmd))
            {

                while (rdMensajes.Read())
                {
                    mensajeError.Parametro = rdMensajes["Parametro"].ToString();
                    mensajeError.Valor = rdMensajes["Valor"].ToString();
                    mensajeError.Codigo = Convert.ToInt32(rdMensajes["Codigo"]);
                }
            }

            return mensajeError;
        }
    }
}
