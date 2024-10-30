
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Esquemas:SAT.CreditosFiscales.Motor.AccesoDatos.Esquemas.DalEsquemas:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Data;
using SAT.CreditosFiscales.Motor.Entidades;
using SAT.CreditosFiscales.Motor.Entidades.Esquemas;
using Enumeraciones = SAT.CreditosFiscales.Motor.Entidades.Enumeraciones;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Esquemas
{
    /// <summary>
    /// Clase de acceso a datos para obtener información de esquemas
    /// </summary>
    public class DalEsquemas
    {
        private const string SP_CONSULTAXSD = "pConsultaXSD";
        private const string SP_INSERTAESQUEMA = "pInsertaEsquema";

        /// <summary>
        /// Consulta un esquema xsd
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación</param>
        /// <param name="idTipoDocPago">Id del tipo de documento</param>
        /// <param name="direccion">dirección de validación</param>
        /// <returns>Entidad Contrato</returns>
        public static Contrato ConsultaXsd(short idAplicacion, short idTipoDocPago, Enumeraciones.DireccionTraductor direccion)
        {
            Contrato contrato = null;
            DbDataReader rdContrato = null;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_CONSULTAXSD);
            

            db.AddInParameter(cmd, "@pIdAplicacion", System.Data.DbType.Int16, idAplicacion);
            db.AddInParameter(cmd, "@pIdTipoDocPago", System.Data.DbType.Int16, idTipoDocPago);
            db.AddInParameter(cmd, "@pDireccion", System.Data.DbType.Byte, (byte)direccion);

            db.ExecuteReader(cmd);

            using (rdContrato = (DbDataReader)db.ExecuteReader(cmd))
            {

                while (rdContrato.Read())
                {
                    contrato = new Contrato
                    {
                        IdEsquema = Convert.ToInt16(rdContrato["IdEsquema"]),
                        Descripcion = rdContrato["Descripcion"].ToString(),
                        Xsd = rdContrato["Esquema"].ToString(),
                        TargetNamespace = rdContrato["TargetNamespace"] != DBNull.Value ? rdContrato["TargetNamespace"].ToString() : string.Empty
                    };
                }
            }


            return contrato;
        }

        /// <summary>
        /// Inserta un esquema de validación
        /// </summary>
        /// <param name="descripcion">Descripción del esquema</param>
        /// <param name="esquema">Esquema xsd</param>
        /// <param name="targetNamepace">Namespace</param>
        /// <returns>número de registros afectados</returns>
        public static int InsertaEsquema(string descripcion, string esquema, string targetNamepace)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTAESQUEMA);
            int idRegla;

            db.AddInParameter(cmd, "@pDescripcion", System.Data.DbType.String, descripcion);
            db.AddInParameter(cmd, "@pEsquema", System.Data.DbType.String, esquema);
            db.AddInParameter(cmd, "@pTargetNamespace", System.Data.DbType.String, targetNamepace);

            idRegla = Convert.ToInt32(db.ExecuteScalar(cmd));            

            return idRegla;
        }
    }
}
