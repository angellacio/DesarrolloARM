
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.GeneraLineaCaptura:Sat.CreditosFiscales.Datos.AccesoDatos.GeneraLineaCaptura.DalLineaCaptura:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Herramientas;

namespace Sat.CreditosFiscales.Datos.AccesoDatos.GeneraLineaCaptura
{
    /// <summary>
    /// Clase para el manejo de la líneas de captura en base de datos
    /// </summary>
    public class DalLineaCaptura : IDisposable
    {
        /// <summary>
        /// Guarda la información obtenida de las líneas de captura de una solicitud, y genera la relación entre el IdSolicitud, IdDocumento, IdLineaCaptura
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <param name="listaLineaCaptura">Lista de líneas de captura dentro de la solicitud <see cref="LineaCaptura"/></param>
        public void GuardarLineasCaptura(Int64 idSolicitud, List<LineaCaptura> listaLineaCaptura)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection conexion = db.CreateConnection();
            conexion.Open();
            DbTransaction transaccion = null;
            try
            {
                transaccion = conexion.BeginTransaction();
                foreach (var lineaCaptura in listaLineaCaptura)
                {
                    foreach (var doc in lineaCaptura.Documentos)
                    {
                        DbCommand cmd = PreparaComandoInsercionLineaCaptura(idSolicitud, doc.IdDocumento, lineaCaptura, db);
                        db.ExecuteNonQuery(cmd, transaccion);
                    }
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw ex;
            }
            finally
            {
                if (conexion.State.Equals(ConnectionState.Open))
                {
                    conexion.Close();
                }
            }
        }
        /// <summary>
        /// Método para preparar el procedimiento de inserción de las líneas de captura
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <param name="idDocumento">Identificador del documento</param>
        /// <param name="lineaCaptura">Línea de captura <see cref="LineaCaptura"/></param>
        /// <param name="db">Base de datos a ejecutar</param>
        /// <returns>Comando de ejecución</returns>
        private DbCommand PreparaComandoInsercionLineaCaptura(Int64 idSolicitud, Int64 idDocumento, LineaCaptura lineaCaptura, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pInsertaLineaCaptura");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Solicitud.Campos.IdSolicitud), DbType.Int64, idSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdDocumento), DbType.Int64, idDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(LineaCaptura.Campos.FechaEmision), DbType.DateTime, lineaCaptura.FechaEmision);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(LineaCaptura.Campos.FechaPago), DbType.DateTime, lineaCaptura.FechaPago);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(LineaCaptura.Campos.FechaVencimiento), DbType.DateTime, lineaCaptura.FechaVencimiento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(LineaCaptura.Campos.ImporteTotal), DbType.Decimal, lineaCaptura.ImporteTotal);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(LineaCaptura.Campos.Linea), DbType.String, lineaCaptura.Linea);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(LineaCaptura.Campos.Folio), DbType.String, lineaCaptura.Folio);
            
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
