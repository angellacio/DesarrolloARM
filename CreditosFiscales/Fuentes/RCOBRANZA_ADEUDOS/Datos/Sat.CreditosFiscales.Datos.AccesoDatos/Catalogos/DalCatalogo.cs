
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos.DalCatalogo:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Herramientas;

namespace Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos
{
    public class DalCatalogo : IDisposable
    {
        /// <summary>
        /// Obtiene los datos de una ALR con base a la Clave corta
        /// </summary>
        /// <param name="CveCorta">Clave corta de la ALR</param>
        /// <returns>Instancia de <see cref="ALR"/></returns>
        public static ALR ObtenerAlrXCveCorta(string CveCorta)
        {
            //pObtieneAlrXCveCorta
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pObtieneAlrXCveCorta");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ALR.Campos.ClaveCorta), DbType.String, CveCorta);
            using (var reader = db.ExecuteReader(cmd))
            {
                ALR alr = null;
                while (reader.Read())
                {
                    alr = new ALR()
                    {
                        ClaveCorta = (string)reader[MetodosComunes.ObtieneNombreCampo(ALR.Campos.ClaveCorta)],
                        Descripcion = (string)reader[MetodosComunes.ObtieneNombreCampo(ALR.Campos.Descripcion)],
                        Estado = (string)reader[MetodosComunes.ObtieneNombreCampo(ALR.Campos.Estado)],
                        IdAlr = (byte)reader[MetodosComunes.ObtieneNombreCampo(ALR.Campos.IdAlr)],
                        Prefijo = (string)reader[MetodosComunes.ObtieneNombreCampo(ALR.Campos.Prefijo)],
                        RutaInternet = (string)reader[MetodosComunes.ObtieneNombreCampo(ALR.Campos.RutaInternet)],
                        Telefono = (string)reader[MetodosComunes.ObtieneNombreCampo(ALR.Campos.Telefono)]
                    };

                }
                return alr;
            }


        }

        public List<ALR> obtenerCatalogoALR()
        {
            var listaALR = new List<ALR>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pObtenerCatalogoALR");
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ALR alr = new ALR();
                    alr.ClaveCorta = reader["CveCorta"].ToString();
                    alr.Descripcion = reader["Descripcion"].ToString();
                    alr.IdAlr = Convert.ToInt16(reader["IdALR"]);
                    alr.Estado = reader["Estado"].ToString();
                    alr.Prefijo = reader["Prefijo"].ToString();
                    listaALR.Add(alr);
                }
            }

            return listaALR;
        }

        public List<Autoridad> obtenerCatalogoAutoridad()
        {
            var listaAutoridad = new List<Autoridad>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pObtenerCatalogoAutoridad");
            using (var reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Autoridad autoridad = new Autoridad();
                    autoridad.Descripcion = reader["Descripcion"].ToString();
                    autoridad.IdAutoridad = Convert.ToInt16(reader["IdAutoridad"]);
                    listaAutoridad.Add(autoridad);
                }
            }

            return listaAutoridad;
        }

        public static List<CatBanco> ObtenerCatalogoBancos()
        {
            var listaBancos = new List<CatBanco>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pCatalogosBancoObtener");
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    CatBanco catBanco = new CatBanco();
                    catBanco.IdBanco = (int)dataReader["IdBanco"];
                    catBanco.Descripcion = (string)dataReader["Descripcion"];
                    listaBancos.Add(catBanco);
                }
            }

            return listaBancos;
        }

        public List<FechaINPC> ObtenerCatalogoFechaINPC()
        {
            var catFechaINPC = new List<FechaINPC>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pCatFechaINPCConsulta");
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    FechaINPC fechaINPC = new FechaINPC();
                    fechaINPC.IdFecha = Convert.ToInt32(dataReader[MetodosComunes.ObtieneNombreCampo(FechaINPC.Campos.IdFecha)]);
                    fechaINPC.Fecha = (DateTime)dataReader[MetodosComunes.ObtieneNombreCampo(FechaINPC.Campos.Fecha)];
                    catFechaINPC.Add(fechaINPC);
                }
            }

            return catFechaINPC;

        }

        public List<DiaInhabil> ObtenerCatalogoDiaInhabil()
        {
            var catDiaInhabil = new List<DiaInhabil>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pCatDiaInhabilConsulta");
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DiaInhabil diaInhabil = new DiaInhabil();
                    diaInhabil.IdFecha = (int)dataReader[MetodosComunes.ObtieneNombreCampo(DiaInhabil.Campos.IdFecha)];
                    diaInhabil.Fecha = (DateTime)dataReader[MetodosComunes.ObtieneNombreCampo(DiaInhabil.Campos.Fecha)];
                    catDiaInhabil.Add(diaInhabil);
                }
            }

            return catDiaInhabil;
        }

        ///// <summary>
        ///// Función que obtiene todos los valores del catálogo de marcas "CatMarcas"
        ///// </summary>
        ///// <returns>Lista de <see cref="CatMarca"/></returns>
        //public static List<CatMarca> ObtenerCatalogoMarca()
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = db.GetStoredProcCommand("pObtenerCatMarca");
        //    using (var dataReader = db.ExecuteReader(dbCommand))
        //    {
        //        List<CatMarca> marcas = new List<CatMarca>();
        //        while (dataReader.Read())
        //        {
        //            CatMarca marca = new CatMarca()
        //            {
        //                CveMarca = (int)dataReader[CatMarca.Campos.CveMarca.ToString()],
        //                Descripcion = (string)dataReader[CatMarca.Campos.Descripcion.ToString()],
        //                GenerarLC = bool.Parse(dataReader[CatMarca.Campos.GenerarLC.ToString()].ToString()),
        //                MostrarImportes = bool.Parse(dataReader[CatMarca.Campos.MostrarImportes.ToString()].ToString()),
        //                Observacion = (string)dataReader[CatMarca.Campos.Observacion.ToString()]
        //            };
        //            marcas.Add(marca);
        //        }
        //        return marcas;
        //    }

        //}

        public static DescripcionConceptoPeriodicidad ObtenerDescripcionConceptoPeriodicidad(int idConceptoLey, int idPeriodo ,int idPeriodicidad)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pObtieneDescripcionesPresentacionConcepto");
                db.AddInParameter(dbCommand, MetodosComunes.ObtieneNombreParametroSql(DescripcionConceptoPeriodicidad.Campos.DescripcionConcepto), DbType.Int32, idConceptoLey);
                db.AddInParameter(dbCommand, MetodosComunes.ObtieneNombreParametroSql(DescripcionConceptoPeriodicidad.Campos.DescripcionPeriodicidad), DbType.Int32, idPeriodicidad);
                db.AddInParameter(dbCommand, MetodosComunes.ObtieneNombreParametroSql(DescripcionConceptoPeriodicidad.Campos.DescripcionPeriodo), DbType.Int32, idPeriodo);
                using (var dataReader = db.ExecuteReader(dbCommand))
                {
                    DescripcionConceptoPeriodicidad des = new DescripcionConceptoPeriodicidad();
                    while (dataReader.Read())
                    {
                        des.DescripcionConcepto = (string)dataReader[MetodosComunes.ObtieneNombreCampo(DescripcionConceptoPeriodicidad.Campos.DescripcionConcepto)];
                        des.DescripcionPeriodicidad = (string)dataReader[MetodosComunes.ObtieneNombreCampo(DescripcionConceptoPeriodicidad.Campos.DescripcionPeriodicidad)];
                        des.DescripcionPeriodo = (string)dataReader[MetodosComunes.ObtieneNombreCampo(DescripcionConceptoPeriodicidad.Campos.DescripcionPeriodo)];
                    }
                    return des;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
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
