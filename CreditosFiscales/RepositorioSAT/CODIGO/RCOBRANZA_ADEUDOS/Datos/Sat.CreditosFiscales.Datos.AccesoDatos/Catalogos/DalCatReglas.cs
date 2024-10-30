//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos.DalCatReglas:1:27/08/2013[Assembly:1.0:27/08/2013])

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
    public class DalCatReglas : IDisposable
    {
        public List<CatReglas> ObtenerLista(string cadenaConexion, string idRegla, string descripcion)
        {
            var lista = new List<CatReglas>();
            Database db = new SqlDatabase(cadenaConexion);
            DbCommand cmd = PreparaComandoBuscar(idRegla, descripcion, db);
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CatReglas catalogo = new CatReglas()
                    {
                        Descripcion = reader["Descripcion"].ToString(),
                        EsValidacion = Convert.ToBoolean(reader["EsValidacion"]),
                        IdRegla = new Guid(reader["IdRegla"].ToString()),
                        Regla = reader["Regla"].ToString()
                    };
                    lista.Add(catalogo);
                }
            }

            return lista;
        }
        public void Guardar(string cadenaConexion, CatReglas.Accion accion, CatReglas Regla)
        {
            try
            {
                Database db = new SqlDatabase(cadenaConexion);
                DbCommand cmd = PreparaComandoInsertar((int)accion, Regla, db);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(string cadenaConexion, Guid idRegla)
        {
            try
            {
                Database db = new SqlDatabase(cadenaConexion);
                DbCommand cmd = PreparaComandoEliminar(idRegla, db);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DbCommand PreparaComandoBuscar(string idRegla, string descripcion, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerCatReglas");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatReglas.Campos.IdRegla), DbType.String, idRegla);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatReglas.Campos.Descripcion), DbType.String, descripcion);
            return cmd;
        }

        private DbCommand PreparaComandoInsertar(int accion, CatReglas Regla, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pGuardarCatReglas");
            db.AddInParameter(cmd, "@Accion", DbType.Int16, accion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatReglas.Campos.Descripcion), DbType.String, Regla.Descripcion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatReglas.Campos.EsValidacion), DbType.Boolean, Regla.EsValidacion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatReglas.Campos.IdRegla), DbType.Guid, Regla.IdRegla);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatReglas.Campos.Regla), DbType.Xml, Regla.Regla);

            return cmd;
        }

        private DbCommand PreparaComandoEliminar(Guid idRegla, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pEliminarCatReglas");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatReglas.Campos.IdRegla), DbType.Guid, idRegla);
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
