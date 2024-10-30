
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos.DalTipoDocumento:1:12/07/2012[Assembly:1.0:12/07/2013])

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
    /// Clase de acceso a datos para obtener información de tipo de documento
    /// </summary>
    public class DalTipoDocumento
    {
        public const string SP_CONSULTATIPODOCUMENTOS = "pObtenerTipoDocumentos";

        /// <summary>
        /// Consulta el catálogo de Tipo de Documentos
        /// </summary>
        /// <returns>Lista de tipo de documentos </returns>
        public static List<Entidades.Catalogos.CatTipoDocumento> ConsultaTipoDocumentos()
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_CONSULTATIPODOCUMENTOS);
            IDataReader rdTipoDocumentos = null;
            List<Entidades.Catalogos.CatTipoDocumento> lstTipoDocumentos = new List<Entidades.Catalogos.CatTipoDocumento>();

            using (rdTipoDocumentos = db.ExecuteReader(cmd))
            {

                while (rdTipoDocumentos.Read())
                {

                    lstTipoDocumentos.Add(
                        new Entidades.Catalogos.CatTipoDocumento
                        {
                            IdTipoDocumento = Convert.ToInt32(rdTipoDocumentos["IdTipoDocumento"]),
                            Nombre = rdTipoDocumentos["Nombre"].ToString()
                        }
                        );
                }
            }

            return lstTipoDocumentos;
        }
    }
}
