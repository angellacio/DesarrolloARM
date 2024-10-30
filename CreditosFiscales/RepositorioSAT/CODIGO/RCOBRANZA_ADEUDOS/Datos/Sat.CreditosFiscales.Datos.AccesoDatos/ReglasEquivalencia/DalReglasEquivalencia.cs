using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Herramientas;

namespace Sat.CreditosFiscales.Datos.AccesoDatos.ReglasEquivalencia
{
    public class DalReglasEquivalencia : IDisposable
    {
        public enum TipoCatalogo
        {
            CatAplicacion,
            CatTipoObjeto,
            CatTipoDocumento,
            CatConceptoDyP
        }

        public List<ReglaEquivalencia> ObtieneReglas(string cadenaConexion, int idAplicacion, int idTipoDocumento, int idTipoObjeto, string valorObjeto)
        {
            Database db = new SqlDatabase(cadenaConexion);
            DbConnection conexion = db.CreateConnection();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("pObtieneReglas");
                int i = 0;

                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdAplicacion), DbType.Int32, idAplicacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdTipoDocumento), DbType.Int32, idTipoDocumento);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdTipoObjeto), DbType.Int32, idTipoObjeto);
                if (!string.IsNullOrEmpty(valorObjeto))
                {
                    db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.ValorObjeto), DbType.String, valorObjeto);
                }
                else
                {
                    db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.ValorObjeto), DbType.String, SqlString.Null);
                }

                List<ReglaEquivalencia> reglas = new List<ReglaEquivalencia>();
                var reader = db.ExecuteReader(cmd);
                while (reader.Read())
                {
                    ReglaEquivalencia regla = new ReglaEquivalencia();

                    regla.Activo = (bool)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.Activo)];
                    regla.Descripcion = (string)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.Descripcion)];
                    regla.IdAplicacion = int.Parse(reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.IdAplicacion)].ToString());
                    regla.IdReglaEquivalencia = int.Parse(reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.IdReglaEquivalencia)].ToString());
                    regla.IdTipoDocumento = int.Parse(reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.IdTipoDocumento)].ToString());
                    regla.IdTipoObjeto = int.Parse(reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.IdTipoObjeto)].ToString());
                    regla.NombreAplicacion = (string)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.NombreAplicacion)];
                    regla.NombreDocumento = (string)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.NombreDocumento)];
                    regla.NombreTipoObjeto = (string)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.NombreTipoObjeto)];
                    regla.Secuencia = int.Parse(reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.Secuencia)].ToString());
                    regla.ValorEvaluacion = (string)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.ValorEvaluacion)];
                    regla.ValorObjeto = (string)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.ValorObjeto)];
                    regla.ValorRetorno = (string)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.ValorRetorno)];
                    regla.XPathEvaluacion = (string)reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.XPathEvaluacion)];
                    if (int.TryParse(reader[MetodosComunes.ObtieneNombreCampo(ReglaEquivalencia.Campos.IdPadre)].ToString(),out i))
                    {
                        regla.IdPadre = i;
                    }
                    reglas.Add(regla);

                }
                return reglas;
            }
            catch (Exception err)
            {
                throw (err);
            }
            finally
            {
                if (conexion.State.Equals(ConnectionState.Open))
                {
                    conexion.Close();
                }
            }
        }

        public List<ConceptoEquivalencia> ObtieneConceptos(string cadenaConexion, int idAplicacion, string conceptoOrigen, string conceptoDyP)
        {
            Database db = new SqlDatabase(cadenaConexion);
            DbConnection conexion = db.CreateConnection();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("pObtieneConceptosEquivalencia");

                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.IdAplicacion), DbType.Int32, idAplicacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.ConceptoOrigen), DbType.String, conceptoOrigen);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.ConceptoDyP), DbType.String, conceptoDyP);                

                List<ConceptoEquivalencia> conceptos = new List<ConceptoEquivalencia>();
                var reader = db.ExecuteReader(cmd);
                while (reader.Read())
                {
                    ConceptoEquivalencia concepto = new ConceptoEquivalencia();

                    concepto.Activo = (bool)reader[MetodosComunes.ObtieneNombreCampo(ConceptoEquivalencia.Campos.Activo)];
                    concepto.ConceptoDyP = reader[MetodosComunes.ObtieneNombreCampo(ConceptoEquivalencia.Campos.ConceptoDyP)].ToString();
                    concepto.ConceptoOrigen = reader[MetodosComunes.ObtieneNombreCampo(ConceptoEquivalencia.Campos.ConceptoOrigen)].ToString();
                    concepto.DescripcionOrigen = reader[MetodosComunes.ObtieneNombreCampo(ConceptoEquivalencia.Campos.DescripcionOrigen)].ToString();
                    concepto.DescripcionDyP = reader[MetodosComunes.ObtieneNombreCampo(ConceptoEquivalencia.Campos.DescripcionDyP)].ToString();
                    concepto.DescripcionAplicacion = reader[MetodosComunes.ObtieneNombreCampo(ConceptoEquivalencia.Campos.DescripcionAplicacion)].ToString();
                    concepto.IdAplicacion = int.Parse(reader[MetodosComunes.ObtieneNombreCampo(ConceptoEquivalencia.Campos.IdAplicacion)].ToString());                    
                    conceptos.Add(concepto);

                }
                return conceptos;
            }
            catch (Exception err)
            {
                throw (err);
            }
            finally
            {
                if (conexion.State.Equals(ConnectionState.Open))
                {
                    conexion.Close();
                }
            }
            

            
        }

        public List<CatalogoGenerico> ObtieneCatConceptoDyP(string cadenaConexion)
        {
            return ObtieneCatalogo(cadenaConexion, TipoCatalogo.CatConceptoDyP);
        }

        public List<CatalogoGenerico> ObtieneCatAplicacion(string cadenaConexion)
        {
            return ObtieneCatalogo(cadenaConexion, TipoCatalogo.CatAplicacion);
        }

        public List<CatalogoGenerico> ObtieneCatTipoDocumento(string cadenaConexion)
        {
            return ObtieneCatalogo(cadenaConexion, TipoCatalogo.CatTipoDocumento);
        }

        public List<CatalogoGenerico> ObtieneCatTipoObjeto(string cadenaConexion)
        {
            return ObtieneCatalogo(cadenaConexion, TipoCatalogo.CatTipoObjeto);
        }

        public void ActualizaRegla(string cadenaConexion, ReglaEquivalencia regla)
        {
            Database db = new SqlDatabase(cadenaConexion);
            DbConnection conexion = db.CreateConnection();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("pActualizaReglaEquivalencia");
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdReglaEquivalencia), DbType.Int32, regla.IdReglaEquivalencia);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.Activo), DbType.Boolean, regla.Activo);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.Descripcion), DbType.String, regla.Descripcion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdAplicacion), DbType.Int32, regla.IdAplicacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdTipoDocumento), DbType.Int32, regla.IdTipoDocumento);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdTipoObjeto), DbType.Int32, regla.IdTipoObjeto);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.Secuencia), DbType.Int32, regla.Secuencia);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.ValorEvaluacion), DbType.String, regla.ValorEvaluacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.ValorObjeto), DbType.String, regla.ValorObjeto);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.ValorRetorno), DbType.String, regla.ValorRetorno);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.XPathEvaluacion), DbType.String, regla.XPathEvaluacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdPadre), DbType.Int32, regla.IdPadre<=0?SqlInt32.Null:regla.IdPadre);
                
                db.ExecuteNonQuery(cmd);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaConcepto(string cadenaConexion, ConceptoEquivalencia concepto, ConceptoEquivalencia conceptoOriginal)
        {
            Database db = new SqlDatabase(cadenaConexion);
            DbConnection conexion = db.CreateConnection();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("pActualizaConceptoEquivalencia");
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.Activo), DbType.Boolean, concepto.Activo);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.ConceptoDyP), DbType.String, concepto.ConceptoDyP);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.ConceptoOrigen), DbType.String, concepto.ConceptoOrigen);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.IdAplicacion), DbType.Int32, concepto.IdAplicacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.DescripcionOrigen), DbType.String, concepto.DescripcionOrigen);

                db.AddInParameter(cmd, "@ConceptoDyPOriginal", DbType.String, conceptoOriginal.ConceptoDyP);
                db.AddInParameter(cmd, "@ConceptoOrigenOriginal", DbType.String, conceptoOriginal.ConceptoOrigen);
                db.AddInParameter(cmd, "@IdAplicacionOriginal", DbType.Int32, conceptoOriginal.IdAplicacion);
                
                db.ExecuteNonQuery(cmd);
            }
            catch
            {
                throw;
            }
        }

        public void AgregaRegla(string cadenaConexion, ReglaEquivalencia regla)
        {
            Database db = new SqlDatabase(cadenaConexion);
            DbConnection conexion = db.CreateConnection();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("pInsertaReglaEquivalencia");
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.Activo), DbType.Boolean, regla.Activo);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.Descripcion), DbType.String, regla.Descripcion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdAplicacion), DbType.Int32, regla.IdAplicacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdTipoDocumento), DbType.Int32, regla.IdTipoDocumento);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdTipoObjeto), DbType.Int32, regla.IdTipoObjeto);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.Secuencia), DbType.Int32, regla.Secuencia);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.ValorEvaluacion), DbType.String, regla.ValorEvaluacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.ValorObjeto), DbType.String, regla.ValorObjeto);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.ValorRetorno), DbType.String, regla.ValorRetorno);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.XPathEvaluacion), DbType.String, regla.XPathEvaluacion);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ReglaEquivalencia.Campos.IdPadre), DbType.Int32, regla.IdPadre<=0?SqlInt32.Null:regla.IdPadre);

                db.ExecuteNonQuery(cmd);
            }
                
            catch
            {
                
                throw;
            }

        }

        public void AgregaConcepto(string cadenaConexion, ConceptoEquivalencia concepto)
        {
            Database db = new SqlDatabase(cadenaConexion);
            DbConnection conexion = db.CreateConnection();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("pInsertaConceptoEquivalencia");
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.Activo), DbType.Boolean, concepto.Activo);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.ConceptoDyP), DbType.String, concepto.ConceptoDyP);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.ConceptoOrigen), DbType.String, concepto.ConceptoOrigen);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.DescripcionOrigen), DbType.String, concepto.DescripcionOrigen);
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ConceptoEquivalencia.Campos.IdAplicacion), DbType.Int32, concepto.IdAplicacion);
                
                db.ExecuteNonQuery(cmd);
            }
            catch
            {
                throw;
            }

        }

        private List<CatalogoGenerico> ObtieneCatalogo(string cadenaConexion, TipoCatalogo tipoCatalogo)
        {
            Database db = new SqlDatabase(cadenaConexion);
            DbConnection conexion = db.CreateConnection();
            List<CatalogoGenerico> catAplicacion = new List<CatalogoGenerico>();
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("pObtieneCatAplicacion");
                switch (tipoCatalogo)
                {
                    case TipoCatalogo.CatAplicacion:
                        cmd = db.GetStoredProcCommand("pObtieneCatAplicacion");
                        break;
                    case TipoCatalogo.CatTipoDocumento:
                        cmd = db.GetStoredProcCommand("pObtieneCatTipoDocumento");
                        break;
                    case TipoCatalogo.CatTipoObjeto:
                        cmd = db.GetStoredProcCommand("pObtieneCatTipoObjeto");
                        break;
                    case TipoCatalogo.CatConceptoDyP:
                        cmd = db.GetStoredProcCommand("pObtieneCatConceptoDyP");
                        break;
                }


                var reader = db.ExecuteReader(cmd);
                while (reader.Read())
                {
                    catAplicacion.Add(new CatalogoGenerico()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Descripcion = reader["Descripcion"].ToString()
                        });
                    
                }
            }
            catch
            {

                throw;
            }

            return catAplicacion;
        }

        #region Implementación Dispose
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}


