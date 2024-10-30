
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato:Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato.DalSolicitud:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Comunes.Entidades;


namespace Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato
{
    /// <summary>
    /// Clase para el manejo de las solicitudes en base de datos
    /// </summary>
    public class DalSolicitud : IDisposable
    {
        /// <summary>
        /// Obtiene los datos del contribuyente de acuerdo a la solicitud registrada
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <returns><see cref="Usuario"/></returns>
        public Usuario ObtenerUsuarioSolicitud(Int64 idSolicitud)
        {
            var usuario = new Usuario();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PreparaComandoObtenerUsuarioSolicitud(idSolicitud, db);
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    usuario.Nombres = reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.Nombres)].ToString();
                    usuario.ApellidoPaterno = reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.ApellidoPaterno)].ToString();
                    usuario.ApellidoMaterno = reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.ApellidoMaterno)].ToString();
                    usuario.Rfc = reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.Rfc)].ToString();
                    usuario.RazonSocial = reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.RazonSocial)].ToString();
                    usuario.IdALR = Convert.ToByte(reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.IdALR)]);
                    usuario.PersonaFisica = Convert.ToInt16(reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.PersonaFisica)]) == 1 ? true : false;
                    usuario.IdPersonaMAT = reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.IdPersonaMAT)].ToString();
                    usuario.IdTipoLineaMAT = Convert.ToByte(reader[MetodosComunes.ObtieneNombreCampo(Usuario.Campos.IdTipoLineaMAT)]);
                }
            }

            return usuario;
        }

        /// <summary>
        /// Obtiene los datos genericos de un deudor puro
        /// </summary>
        /// <returns><see cref="Usuario"/></returns>
        public Usuario ObtenerDatosGenericosDeudorPuro()
        {
            var usuario = new Usuario();
            List<ApplicationSetting> listaSettings = new Catalogos.DalApplicationSettings().RecuperaGrupoConfiguracion("Generico");
            if (listaSettings.Count > 0)
            {
                foreach (ApplicationSetting setting in listaSettings)
                {
                    if (setting.SettingName.Contains("Rfc")) usuario.Rfc = setting.SettingValue.ToString();
                    if (setting.SettingName.Contains("BoId")) usuario.BoId = setting.SettingValue.ToString();
                }
            }

            return usuario;
        }
        /// <summary>
        /// Obtiene los datos de los documentos relacionados a una solicitud realizada por un contribuyente
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <returns><see cref="Solicitud"/>></returns>
        public Solicitud ObtenerSolicitud(Int64 idSolicitud)
        {
            var solicitud = new Solicitud();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PreparaComandoObtieneSolicitudDocumentosDeterminantes(idSolicitud, db);

            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    solicitud.IdSolicitud = Convert.ToInt64(reader["IdSolicitud"]);
                    DocumentoDeterminante doc = new DocumentoDeterminante();


                    doc.FechaDocumento = Convert.ToDateTime(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.FechaDocumento)]); ;
                    doc.IdALR = Convert.ToByte(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.IdALR)]); ;
                    doc.IdAutoridad = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.IdAutoridad)]);
                    doc.IdDocumento = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.IdDocumento)]); ;
                    doc.IdResolucionMAT = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.IdResolucionMAT)].ToString();
                    doc.IdSolicitud = idSolicitud;
                    doc.ImporteActualizacion = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.ImporteActualizacion)]); ;
                    doc.ImporteDescuentos = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.ImporteDescuentos)]); ;
                    doc.ImporteHistorico = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.ImporteHistorico)]); ;
                    doc.NumDocumento = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.NumDocumento)].ToString();
                    doc.Rfc = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.Rfc)].ToString();
                    doc.FormaPago = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.FormaPago)] != DBNull.Value ?
                        reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.FormaPago)].ToString():string.Empty;
                    doc.SaldoInformativo = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.SaldoInformativo)] != DBNull.Value ?
                        Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.SaldoInformativo)]) : 0;

                    doc.FechaNotificacion = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.FechaNotificacion)] != DBNull.Value ?
                        Convert.ToDateTime(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminante.Campos.FechaNotificacion)]) : DateTime.MinValue;

                    doc.Marcado = Convert.ToInt16(reader["Marcado"]) == 1;
                    // Si el documento es NO MARCADO obtenemos los conceptos padre e hijo
                    if (!doc.Marcado)
                    {
                        doc.Conceptos = ObtenerListaDeConceptos(doc.IdSolicitud, doc.IdDocumento);
                    }
                    solicitud.Documentos.Add(doc);
                }
            }



            return solicitud;
        }




        private List<DocumentoDeterminanteConcepto> ObtenerListaDeConceptos(Int64 idSolicitud, Int32 idDocumento)
        {
            var conceptos = new List<DocumentoDeterminanteConcepto>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PreparaComandoObtenerListaDeConceptos(idSolicitud, idDocumento, db);
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DocumentoDeterminanteConcepto concepto = new DocumentoDeterminanteConcepto();
                    concepto.ConceptosHijo = new List<DocumentoDeterminanteConceptoHijo>();
                    concepto.CreditoARCA = Convert.ToString(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.CreditoARCA)]);
                    concepto.CreditoSIR = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.CreditoSIR)]);
                    concepto.Ejercicio = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.Ejercicio)]);
                    DateTime? dt = null;

                    concepto.FechaCausacion = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.FechaCausacion)] != DBNull.Value ?
                        Convert.ToDateTime(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.FechaCausacion)]) : dt;

                    concepto.FechaNotificacion = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.FechaNotificacion)] != DBNull.Value ?
                        Convert.ToDateTime(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.FechaNotificacion)]) : dt;

                    concepto.IdConcepto = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.IdConcepto)]);
                    concepto.IdConceptoMAT = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.IdConceptoMAT)].ToString();
                    concepto.IdDocumento = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.IdDocumento)]);
                    concepto.IdDocumentoConcepto = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.IdDocumentoConcepto)]);
                    concepto.IdMotivo = Convert.ToString(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.IdMotivo)]);
                    concepto.IdPeriodicidad = Convert.ToByte(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.idPeriodicidad)]);
                    concepto.IdPeriodo = Convert.ToByte(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.IdPeriodo)]);
                    concepto.IdSolicitud = Convert.ToInt64(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.IdSolicitud)]);
                    concepto.IdTipoConcepto = Convert.ToByte(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.idTipoConcepto)]);
                    concepto.ImporteCondonacion = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.ImporteCondonacion)]);
                    concepto.ImporteDescuentos = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.ImporteDescuentos)]);
                    concepto.ImporteHistorico = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.ImporteHistorico)]);
                    concepto.ImportePagar = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.ImportePagar)]);
                    concepto.ImporteParteActualizada = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.ImporteParteActualizada)]);
                    //concepto.ImporteRecargos = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConcepto.Campos.ImporteRecargos)]);

                    concepto.Descuentos = ObtenerListaDeDescuentosPorConcepto(concepto.IdSolicitud, concepto.IdDocumento, concepto.IdDocumentoConcepto);

                    concepto.ConceptosHijo = ObtenerListaDeConceptosHijo(concepto.IdSolicitud, concepto.IdDocumento, concepto.IdDocumentoConcepto);

                    conceptos.Add(concepto);

                }
            }

            return conceptos;
        }

        private List<DescuentoConcepto> ObtenerListaDeDescuentosPorConcepto(Int64 idSolicitud, Int32 idDocumento, Int32 idDocumentoConcepto)
        {
            var listaDescuentos = new List<DescuentoConcepto>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PreparaComandoObtenerListaDeDescuentosPorConcepto(idSolicitud, idDocumento, idDocumentoConcepto, db);
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var descuento = new DescuentoConcepto();
                    descuento.IdDescuento = reader[MetodosComunes.ObtieneNombreCampo(DescuentoConcepto.Campos.IdDescuento)].ToString();
                    descuento.IdDocumento = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DescuentoConcepto.Campos.IdDocumento)]);
                    descuento.IdDocumentoConcepto = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DescuentoConcepto.Campos.IdDocumentoConcepto)]);
                    descuento.IdSolicitud = Convert.ToInt64(reader[MetodosComunes.ObtieneNombreCampo(DescuentoConcepto.Campos.IdSolicitud)]);
                    descuento.ImporteDescuento = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DescuentoConcepto.Campos.ImporteDescuento)]);

                    listaDescuentos.Add(descuento);
                }
            }

            return listaDescuentos;
        }

        private List<DocumentoDeterminanteConceptoHijo> ObtenerListaDeConceptosHijo(Int64 idSolicitud, Int32 idDocumento, Int32 idDocumentoConcepto)
        {
            var conceptosHijo = new List<DocumentoDeterminanteConceptoHijo>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PreparaComandoObtenerListaDeConceptosHijo(idSolicitud, idDocumento, idDocumentoConcepto, db);
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {

                    var conceptoHijo = new DocumentoDeterminanteConceptoHijo();
                    conceptoHijo.CreditoSIR = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.CreditoSIR)]);
                    //conceptoHijo.Ejercicio = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.Ejercicio)]);
                    DateTime? dt = null;

                    conceptoHijo.FechaCausacion = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.FechaCausacion)] != DBNull.Value ?
                        Convert.ToDateTime(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.FechaCausacion)]) : dt;

                    conceptoHijo.FechaNotificacion = reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.FechaNotificacion)] != DBNull.Value ?
                        Convert.ToDateTime(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.FechaNotificacion)]) : dt;

                    conceptoHijo.IdConcepto = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.IdConcepto)]);
                    conceptoHijo.IdDocumento = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.IdDocumento)]);
                    conceptoHijo.IdDocumentoConcepto = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.IdDocumentoConcepto)]);
                    conceptoHijo.IdMotivo = Convert.ToString(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.IdMotivo)]);
                    //RV
                    conceptoHijo.IdPeriodicidad = Convert.ToByte(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.IdPeriodicidad)]);
                    conceptoHijo.IdPeriodo = Convert.ToByte(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.IdPeriodo)]);
                    conceptoHijo.IdSolicitud = Convert.ToInt64(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.IdSolicitud)]);
                    conceptoHijo.ImporteCondonacion = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.ImporteCondonacion)]);
                    conceptoHijo.ImporteDescuentos = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.ImporteDescuentos)]);
                    conceptoHijo.ImporteHistorico = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.ImporteHistorico)]);
                    conceptoHijo.ImportePagar = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.ImportePagar)]);
                    conceptoHijo.ImporteParteActualizada = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.ImporteParteActualizada)]);
                    //conceptoHijo.ImporteRecargos = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DocumentoDeterminanteConceptoHijo.Campos.ImporteRecargos)]);

                    conceptoHijo.Descuentos = ObtenerListaDeDescuentosPorConceptoHijo(
                        conceptoHijo.IdSolicitud,
                        conceptoHijo.IdDocumento,
                        conceptoHijo.IdDocumentoConcepto,
                        conceptoHijo.IdConcepto);

                    conceptosHijo.Add(conceptoHijo);

                }
            }
            return conceptosHijo;
        }

        private List<DescuentoConceptoHijo> ObtenerListaDeDescuentosPorConceptoHijo(Int64 idSolicitud, Int32 idDocumento, Int32 idDocumentoConcepto, Int32 idConcepto)
        {
            var listaDescuentos = new List<DescuentoConceptoHijo>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PreparaComandoObtenerListaDeDescuentosPorConceptoHijo(idSolicitud, idDocumento, idDocumentoConcepto, idConcepto, db);
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var descuento = new DescuentoConceptoHijo();
                    descuento.IdDescuento = reader[MetodosComunes.ObtieneNombreCampo(DescuentoConceptoHijo.Campos.IdDescuento)].ToString();
                    descuento.IdDocumento = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DescuentoConceptoHijo.Campos.IdDocumento)]);
                    descuento.IdDocumentoConcepto = Convert.ToInt32(reader[MetodosComunes.ObtieneNombreCampo(DescuentoConceptoHijo.Campos.IdDocumentoConcepto)]);
                    descuento.IdSolicitud = Convert.ToInt64(reader[MetodosComunes.ObtieneNombreCampo(DescuentoConceptoHijo.Campos.IdSolicitud)]);
                    descuento.ImporteDescuento = Convert.ToDecimal(reader[MetodosComunes.ObtieneNombreCampo(DescuentoConceptoHijo.Campos.ImporteDescuento)]);

                    listaDescuentos.Add(descuento);
                }
            }

            return listaDescuentos;
        }

        private DbCommand PreparaComandoObtieneSolicitudDocumentosDeterminantes(Int64 idSolicitud, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerSolicitudDocumentosDeterminantes");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdSolicitud), DbType.Int64, idSolicitud);
            return cmd;
        }

        private DbCommand PreparaComandoObtenerListaDeConceptos(Int64 idSolicitud, Int32 idDocumento, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerDocumentoDeterminanteConceptos");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdSolicitud), DbType.Int64, idSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdDocumento), DbType.Int32, idDocumento);
            return cmd;
        }

        private DbCommand PreparaComandoObtenerListaDeDescuentosPorConcepto(Int64 idSolicitud, Int32 idDocumento, Int32 idDocumentoConcepto, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerDescuentosPorConcepto");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConcepto.Campos.IdSolicitud), DbType.Int64, idSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConcepto.Campos.IdDocumento), DbType.Int32, idDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConcepto.Campos.IdDocumentoConcepto), DbType.Int32, idDocumentoConcepto);
            return cmd;
        }

        private DbCommand PreparaComandoObtenerListaDeConceptosHijo(Int64 idSolicitud, Int32 idDocumento, Int32 idDocumentoConcepto, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerDocumentoDeterminanteConceptoHijos");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdSolicitud), DbType.Int64, idSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdDocumento), DbType.Int32, idDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdDocumentoConcepto), DbType.Int32, idDocumentoConcepto);
            return cmd;
        }

        private DbCommand PreparaComandoObtenerListaDeDescuentosPorConceptoHijo(Int64 idSolicitud, Int32 idDocumento, Int32 idDocumentoConcepto, Int32 idConcepto, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerDescuentosPorConceptoHijo");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdSolicitud), DbType.Int64, idSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdDocumento), DbType.Int32, idDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdDocumentoConcepto), DbType.Int32, idDocumentoConcepto);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdConcepto), DbType.Int32, idConcepto);
            return cmd;
        }

        private DbCommand PreparaComandoObtenerDocumentoDeterminanteMarcados(Int64 idSolicitud, Int32 idDocumento, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerDocumentoDeterminanteMarcados");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdSolicitud), DbType.Int64, idSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdDocumento), DbType.Int32, idDocumento);
            return cmd;
        }

        private DbCommand PreparaComandoObtenerUsuarioSolicitud(Int64 idSolicitud, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pObtenerUsuarioSolicitud");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Solicitud.Campos.IdSolicitud), DbType.Int64, idSolicitud);
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
