//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos.DalCatEsquemas:1:27/08/2013[Assembly:1.0:27/08/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Herramientas;
using System.Xml;


namespace Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos
{
    public class DalCatEsquemas : IDisposable
    {
        public List<CatEsquemas> ObtenerLista(string cadenaConexion, string idEsquema, string descripcion)
        {
            var lista = new List<CatEsquemas>();
            Database db = new SqlDatabase(cadenaConexion);
            DbCommand cmd = PreparaComandoBuscar(idEsquema, descripcion, db);
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CatEsquemas catalogo = new CatEsquemas()
                    {
                        Descripcion = reader["Descripcion"].ToString(),
                        TargetNamespace = reader["TargetNamespace"].ToString(),
                        IdEsquema = Convert.ToInt16(reader["IdEsquema"].ToString()),
                        Esquema = reader["Esquema"].ToString()
                    };
                    lista.Add(catalogo);
                }
            }

            return lista;
        }
        public void Guardar(string cadenaConexion, CatEsquemas.Accion accion, CatEsquemas Esquema)
        {
            try
            {
                Database db = new SqlDatabase(cadenaConexion);
                DbCommand cmd = PreparaComandoInsertar((int)accion, Esquema, db);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(string cadenaConexion, Int16 idEsquema)
        {
            try
            {
                Database db = new SqlDatabase(cadenaConexion);
                DbCommand cmd = PreparaComandoEliminar(idEsquema, db);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DbCommand PreparaComandoBuscar(string idEsquema, string descripcion, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerCatEsquemas");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatEsquemas.Campos.IdEsquema), DbType.String, idEsquema);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatEsquemas.Campos.Descripcion), DbType.String, descripcion);
            return cmd;
        }

        private DbCommand PreparaComandoInsertar(int accion, CatEsquemas Esquema, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pGuardarCatEsquemas");
            db.AddInParameter(cmd, "@Accion", DbType.Int16, accion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatEsquemas.Campos.Descripcion), DbType.String, Esquema.Descripcion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatEsquemas.Campos.TargetNamespace), DbType.String, Esquema.TargetNamespace);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatEsquemas.Campos.IdEsquema), DbType.Int16, Esquema.IdEsquema);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatEsquemas.Campos.Esquema), DbType.Xml, Esquema.Esquema);

            return cmd;
        }

        private DbCommand PreparaComandoEliminar(Int16 idEsquema, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pEliminarCatEsquemas");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatEsquemas.Campos.IdEsquema), DbType.Int16, idEsquema);
            return cmd;
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
