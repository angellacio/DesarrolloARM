
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas:SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas.DalReglas:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;
using EntidadRegla = SAT.CreditosFiscales.Motor.Entidades.AdmonRegla;
using EntidadContrato = SAT.CreditosFiscales.Motor.Entidades.Esquemas;
using Enumeraciones = SAT.CreditosFiscales.Motor.Entidades.Enumeraciones;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas
{
    /// <summary>
    /// Clase de acceso a datos de las reglas del motor
    /// </summary>
    public class DalReglas
    {
        private const string SP_CONSULTAREGLAS = "pConsultaReglas";
        private const string SP_OBTIENERECURSOSVALIDACION = "pObtieneRecursosValidacion";
        private const string SP_INSERTAREGLA = "pInsertaRegla";
        private const string SP_INSERTADMONAREGLA = "pInsertaAdmonRegla";

        /// <summary>
        /// Consulta a base de datos las reglas asignadas por Aplicaciónn y Tipo de documento
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación a consultar</param>
        /// <param name="idTipoDocPago">Id Tipo de documento a consultar</param>
        /// <returns>EntidadRegla.ReglasPorDocumento</returns>
        public static EntidadRegla.ReglasPorDocumento ConsultaReglas(short? idAplicacion = null, short? idTipoDocPago = null)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_CONSULTAREGLAS);            

            var lstReglas = new EntidadRegla.ReglasPorDocumento
            {
                IdAplicacion = idAplicacion.HasValue ? idAplicacion.Value : (short)0,
                IdTipoDocPago = idTipoDocPago.HasValue ? idTipoDocPago.Value : (short)0,
                ListaReglas = new EntidadRegla.Reglas()
            };

            db.AddInParameter(cmd, "@pIdAplicacion", System.Data.DbType.Int16, idAplicacion);
            db.AddInParameter(cmd, "@pIdTipoDocPago", System.Data.DbType.Int16, idTipoDocPago);

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    lstReglas.ListaReglas.Add(
                        new EntidadRegla.Regla
                        {
                            IdRegla = new Guid(rdReglas["IdRegla"].ToString()),
                            Descripcion = rdReglas["Descripcion"].ToString(),
                            Xslt = rdReglas["Regla"].ToString(),
                            EsValidacion = Convert.ToBoolean(rdReglas["EsValidacion"]),
                            Secuencia = Convert.ToByte(rdReglas["Secuencia"]),
                            AntesDeInsercion = Convert.ToBoolean(rdReglas["AntesDeInsercion"])
                        }
                        );
                }
            }

            return lstReglas;
        }

        /// <summary>
        /// Consulta todos los esquemas de validación que se aplicarán a la entrada
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación a consultar</param>
        /// <param name="idTipoDocPago">Id Tipo de documento a consultar</param>
        /// <param name="AntesdeInsercion">true si las reglas son antes de aplicar la inserción a base de datos</param>
        /// <returns>EntidadRegla.ReglasPorDocumento</returns>
        public static EntidadRegla.ReglasPorDocumento ConsultaRecursosValidacion(short idAplicacion, short idTipoDocPago, bool AntesdeInsercion, bool EsSIAT)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd;
                cmd = db.GetStoredProcCommand(SP_OBTIENERECURSOSVALIDACION);
            var lstReglas = new EntidadRegla.ReglasPorDocumento
            {
                IdAplicacion = idAplicacion,
                IdTipoDocPago = idTipoDocPago,
                ListaReglas = new EntidadRegla.Reglas(),
                ListaContratos = new EntidadContrato.Contratos(),
                DirectorioReglasNet = string.Empty
            };

            db.AddInParameter(cmd, "@pIdAplicacion", System.Data.DbType.Int16, idAplicacion);
            db.AddInParameter(cmd, "@pIdTipoDocPago", System.Data.DbType.Int16, idTipoDocPago);
            db.AddInParameter(cmd, "@AntesDeInsercion", DbType.Boolean, AntesdeInsercion);
            db.AddInParameter(cmd, "@EsSIAT", DbType.Boolean, EsSIAT);
            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    lstReglas.ListaContratos.Add(
                        new EntidadContrato.Contrato
                        {
                            IdEsquema = Convert.ToInt16(rdReglas["IdEsquema"]),
                            Descripcion = rdReglas["Descripcion"].ToString(),
                            Xsd = rdReglas["Esquema"].ToString(),
                            TargetNamespace = rdReglas["TargetNamespace"] != DBNull.Value ? rdReglas["TargetNamespace"].ToString() : string.Empty,
                            DireccionContrato = (Enumeraciones.DireccionTraductor)Convert.ToByte(rdReglas["IdDireccion"])
                        }
                    );
                }

                rdReglas.NextResult();

                while (rdReglas.Read())
                {

                    lstReglas.ListaReglas.Add(
                        new EntidadRegla.Regla
                        {
                            IdRegla = new Guid(rdReglas["IdRegla"].ToString()),
                            Descripcion = rdReglas["Descripcion"].ToString(),
                            Xslt = rdReglas["Regla"].ToString(),
                            EsValidacion = Convert.ToBoolean(rdReglas["EsValidacion"]),
                            EsNET = Convert.ToBoolean(rdReglas["EsNET"]),
                            Secuencia = Convert.ToByte(rdReglas["Secuencia"]),                            
                        }
                        );
                }

                rdReglas.NextResult();

                while (rdReglas.Read())
                {
                    lstReglas.DirectorioReglasNet = rdReglas["Ruta"].ToString();
                }
            }

            return lstReglas;
        }

        /// <summary>
        /// Consulta todos los esquemas de validación que se aplicarán a la entrada  en SIAT
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación a consultar</param>
        /// <param name="idTipoDocPago">Id Tipo de documento a consultar</param>
        /// <param name="AntesdeInsercion">true si las reglas son antes de aplicar la inserción a base de datos</param>
        /// <returns>EntidadRegla.ReglasPorDocumento</returns>
        public static EntidadRegla.ReglasPorDocumento ConsultaRecursosValidacionSIAT(short idAplicacion, short idTipoDocPago, bool AntesdeInsercion)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENERECURSOSVALIDACION);

            var lstReglas = new EntidadRegla.ReglasPorDocumento
            {
                IdAplicacion = idAplicacion,
                IdTipoDocPago = idTipoDocPago,
                ListaReglas = new EntidadRegla.Reglas(),
                ListaContratos = new EntidadContrato.Contratos(),
                DirectorioReglasNet = string.Empty
            };

            db.AddInParameter(cmd, "@pIdAplicacion", System.Data.DbType.Int16, idAplicacion);
            db.AddInParameter(cmd, "@pIdTipoDocPago", System.Data.DbType.Int16, idTipoDocPago);
            db.AddInParameter(cmd, "@AntesDeInsercion", DbType.Boolean, AntesdeInsercion);

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    lstReglas.ListaContratos.Add(
                        new EntidadContrato.Contrato
                        {
                            IdEsquema = Convert.ToInt16(rdReglas["IdEsquema"]),
                            Descripcion = rdReglas["Descripcion"].ToString(),
                            Xsd = rdReglas["Esquema"].ToString(),
                            TargetNamespace = rdReglas["TargetNamespace"] != DBNull.Value ? rdReglas["TargetNamespace"].ToString() : string.Empty,
                            DireccionContrato = (Enumeraciones.DireccionTraductor)Convert.ToByte(rdReglas["IdDireccion"])
                        }
                    );
                }

                rdReglas.NextResult();

                while (rdReglas.Read())
                {

                    lstReglas.ListaReglas.Add(
                        new EntidadRegla.Regla
                        {
                            IdRegla = new Guid(rdReglas["IdRegla"].ToString()),
                            Descripcion = rdReglas["Descripcion"].ToString(),
                            Xslt = rdReglas["Regla"].ToString(),
                            EsValidacion = Convert.ToBoolean(rdReglas["EsValidacion"]),
                            EsNET = Convert.ToBoolean(rdReglas["EsNET"]),
                            Secuencia = Convert.ToByte(rdReglas["Secuencia"]),
                        }
                        );
                }

                rdReglas.NextResult();

                while (rdReglas.Read())
                {
                    lstReglas.DirectorioReglasNet = rdReglas["Ruta"].ToString();
                }
            }

            return lstReglas;
        }

        /// <summary>
        /// Inserta una regla en base de datos del Motor
        /// </summary>
        /// <param name="descripcion">Descripción de la regla</param>
        /// <param name="regla">Regla Xslt en string</param>
        /// <param name="esValidacion">Indica si la regla es validación = true o transformación = false</param>
        /// <returns>Guid de la regla insertada</returns>
        public static Guid InsertaRegla(string descripcion, string regla, bool esValidacion = false)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTAREGLA);

            db.AddInParameter(cmd, "@descripcion", System.Data.DbType.String, descripcion);
            db.AddInParameter(cmd, "@regla", System.Data.DbType.String, regla);
            db.AddInParameter(cmd, "@esValidacion", System.Data.DbType.Boolean, esValidacion);

            Guid idRegla = new Guid(db.ExecuteScalar(cmd).ToString());

            return idRegla;
        }

        /// <summary>
        /// Inserta un registro en la tabla de AdmonRegla
        /// </summary>
        /// <param name="idRegla">Id de la regla a insertar</param>
        /// <param name="idAplicacion">Id de la aplicación</param>
        /// <param name="idTipoDocumento">Id del tipo de documento</param>
        /// <param name="Secuencia">Secuencia de como se aplicará la regla</param>
        /// <param name="AntesDeInsercion">true si las reglas son antes de aplicar la inserción a base de datos</param>
        /// <returns>int número de registros insertados</returns>
        public static int InsertaAdmonRegla(Guid idRegla, int idAplicacion, int idTipoDocumento, int Secuencia, bool AntesDeInsercion)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTADMONAREGLA);

            db.AddInParameter(cmd, "@idRegla", System.Data.DbType.Guid, idRegla);
            db.AddInParameter(cmd, "@IdAplicacion", System.Data.DbType.Int16, idAplicacion);
            db.AddInParameter(cmd, "@IdTipoDocumento", System.Data.DbType.Int16, idTipoDocumento);
            db.AddInParameter(cmd, "@Secuencia", System.Data.DbType.Int16, Secuencia);
            db.AddInParameter(cmd, "@AntesDeInsercion", System.Data.DbType.Boolean, AntesDeInsercion);

            int registros = Convert.ToInt32(db.ExecuteScalar(cmd));

            return registros;
        }
    }
}
