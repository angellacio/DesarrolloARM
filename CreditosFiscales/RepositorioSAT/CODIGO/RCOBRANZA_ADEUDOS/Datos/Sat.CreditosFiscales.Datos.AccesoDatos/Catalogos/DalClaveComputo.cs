//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos.DalClaveComputo:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Herramientas;

namespace Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos
{
    public class DalClaveComputo : IDisposable
    {

        public List<CatClaveComputo> ObtenerLista(string cadenaConexion, string claveComputo)
        {
            var lista = new List<CatClaveComputo>();
            Database db = new SqlDatabase(cadenaConexion);
            DbCommand cmd = PreparaComandoBuscar(claveComputo, db);
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CatClaveComputo catalogo = new CatClaveComputo()
                    {
                        ClaveComputo = reader["ClaveComputo"].ToString(),
                        Descripcion = reader["Descripcion"].ToString()
                    };
                    lista.Add(catalogo);
                }
            }

            return lista;
        }

        public void Guardar(string cadenaConexion, CatClaveComputo.Accion accion, CatClaveComputo ClaveComputo)
        {
            try
            {
                Database db = new SqlDatabase(cadenaConexion);
                DbCommand cmd = PreparaComandoInsertar((int)accion, ClaveComputo, db);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void Eliminar(string cadenaConexion, string claveComputo)
        {
            try
            {
                Database db = new SqlDatabase(cadenaConexion);
                DbCommand cmd = PreparaComandoEliminar(claveComputo, db);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DbCommand PreparaComandoInsertar(int accion,CatClaveComputo Catalogo, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pGuardarClaveComputo");
            db.AddInParameter(cmd, "@Accion", DbType.Int16, accion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatClaveComputo.Campos.ClaveComputo), DbType.String, Catalogo.ClaveComputo);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatClaveComputo.Campos.Descripcion), DbType.String, Catalogo.Descripcion);
            return cmd;
        }

        private DbCommand PreparaComandoEliminar(string claveComputo, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pEliminarClaveComputo");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatClaveComputo.Campos.ClaveComputo), DbType.String, claveComputo);
            return cmd;
        }

        private DbCommand PreparaComandoBuscar(string claveComputo, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerClaveComputo");
            if (claveComputo != string.Empty)
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatClaveComputo.Campos.ClaveComputo), DbType.String, claveComputo);
            }
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
