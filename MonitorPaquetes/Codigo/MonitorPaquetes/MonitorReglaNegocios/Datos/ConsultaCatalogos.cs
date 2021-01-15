using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MonitorEntidades;

namespace MonitorReglaNegocios.Datos
{
    public class ConsultaCatalogos
    {
        private SqlConnection sqlCon { get; set; }
        private string sConexionString { get { return ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString.Trim(); } }

        public ConsultaCatalogos()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(sConexionString);
            }
        }

        public List<ent_CatalogoSencillo> Consulta_Catalogos(int nOpcion, string sArea)
        {
            SqlCommand sqlCom = null;
            SqlDataReader sqlDR = null;
            List<ent_CatalogoSencillo> result = null;
            try
            {
                result = new List<ent_CatalogoSencillo>() { new ent_CatalogoSencillo() { D000_nId = -1, D000_sId = "-1", D010_sAcronimo = "SO", D020_sDescripcion = "Seleccione una opción.", D030_bEstatus = true } };

                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                sqlCom = new SqlCommand("PWin_Consultas", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.Parameters.Add(new SqlParameter("@nTipoConsulta", SqlDbType.Int)).Value = nOpcion;
                sqlCom.Parameters.Add(new SqlParameter("@nParametro1", SqlDbType.Int)).Value = 0;
                sqlCom.Parameters.Add(new SqlParameter("@sParametro1", SqlDbType.NVarChar)).Value = sArea;
                sqlCom.Parameters.Add(new SqlParameter("@nParametro2", SqlDbType.Int)).Value = 0;
                sqlCom.Parameters.Add(new SqlParameter("@sParametro2", SqlDbType.NVarChar)).Value = "";

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    result.Add(new ent_CatalogoSencillo()
                    {
                        D000_nId = sqlDR.GetInt32(sqlDR.GetOrdinal("nId")),
                        D000_sId = sqlDR.GetString(sqlDR.GetOrdinal("sId")),
                        D010_sAcronimo = sqlDR.GetString(sqlDR.GetOrdinal("sAcronimo")),
                        D020_sDescripcion = sqlDR.GetString(sqlDR.GetOrdinal("sDescripcion")),
                        D030_bEstatus = sqlDR.GetBoolean(sqlDR.GetOrdinal("bEstatus"))
                    });
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        throw new InvalidOperationException("Error al conectarse a la base de datos.", ex);
                    default:
                        throw new Exception("Error en la base de datos.", ex);
                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlDR != null) sqlDR.Close();
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                    sqlCom.Dispose();
                }
                if (sqlCon != null)
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                }
            }
            return result;
        }

        public List<ent_IncidentesPaquetes> Consulta_RMAPaquetes(string sAreas, Boolean bolMuestraTodo)
        {
            SqlCommand sqlCom = null;
            SqlDataReader sqlDR = null;
            List<ent_IncidentesPaquetes> result = null;
            try
            {
                result = new List<ent_IncidentesPaquetes>();

                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                sqlCom = new SqlCommand("PWin_Consultas", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.Parameters.Add(new SqlParameter("@nTipoConsulta", SqlDbType.Int)).Value = 50;
                sqlCom.Parameters.Add(new SqlParameter("@nParametro1", SqlDbType.Int)).Value = bolMuestraTodo ? 1 : 0;
                sqlCom.Parameters.Add(new SqlParameter("@sParametro1", SqlDbType.NVarChar)).Value = sAreas;
                sqlCom.Parameters.Add(new SqlParameter("@nParametro2", SqlDbType.Int)).Value = 0;
                sqlCom.Parameters.Add(new SqlParameter("@sParametro2", SqlDbType.NVarChar)).Value = "";

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    Nullable<DateTime> dtFechaPaquete = null;
                    string sPaquetes = "", sDesarrollador = "", sEstatus = "", sObservaciones = "";
                    Boolean bolDOC_PRU = false, bolNotificado = false;

                    if (sqlDR["Paquete"] != DBNull.Value)
                        sPaquetes = sqlDR.GetString(sqlDR.GetOrdinal("Paquete"));
                    if (sqlDR["Fecha_Registro"] != DBNull.Value)
                        dtFechaPaquete = sqlDR.GetDateTime(sqlDR.GetOrdinal("Fecha_Registro"));
                    if (sqlDR["DOC_PRU"] != DBNull.Value)
                        bolDOC_PRU = sqlDR.GetBoolean(sqlDR.GetOrdinal("DOC_PRU"));
                    if (sqlDR["Notificado"] != DBNull.Value)
                        bolNotificado = sqlDR.GetBoolean(sqlDR.GetOrdinal("Notificado"));
                    if (sqlDR["Desarrollador"] != DBNull.Value)
                        sDesarrollador = sqlDR.GetString(sqlDR.GetOrdinal("Desarrollador"));
                    if (sqlDR["Estado"] != DBNull.Value)
                        sEstatus = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    if (sqlDR["Observaciones"] != DBNull.Value)
                        sObservaciones = sqlDR.GetString(sqlDR.GetOrdinal("Observaciones"));

                    result.Add(new ent_IncidentesPaquetes()
                    {
                        I002_sArea = sqlDR.GetString(sqlDR.GetOrdinal("Area")),
                        I010_sIncidente = sqlDR.GetString(sqlDR.GetOrdinal("Incidente")),
                        I011_fAlta = sqlDR.GetDateTime(sqlDR.GetOrdinal("Fecha")),
                        I012_sDescripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion")),
                        I013_bRetroalimentado = sqlDR.GetBoolean(sqlDR.GetOrdinal("Retroalimentacion")),
                        I020_bPPMC = sqlDR.GetBoolean(sqlDR.GetOrdinal("PPMC")),
                        P010_sPaquete = sPaquetes,
                        P020_fAlta = dtFechaPaquete,
                        P031_bDOC_PRU = bolDOC_PRU,
                        P033_bNotificado = bolNotificado,
                        P032_sObservaciones = sObservaciones,
                        P036_sDesarrollador = sDesarrollador,
                        P42_Estatus = sEstatus,

                        lstRDL = new List<ent_RDLs>()
                    });
                }

                sqlDR.NextResult();
                while (sqlDR.Read())
                {
                    string sPaquete = sqlDR.GetString(sqlDR.GetOrdinal("Paquete"));

                    result.Find(itemI => itemI.P010_sPaquete == sPaquete).lstRDL.Add(new ent_RDLs()
                    {
                        D010_nRDL = sqlDR.GetInt32(sqlDR.GetOrdinal("RDL")),
                        D011_sPaquete = sPaquete,
                        D012_fMovimiento = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaInicio")),
                        D013_nEstatus = sqlDR.GetInt32(sqlDR.GetOrdinal("Id_Estado")),
                        D020_sEstatus = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"))
                    });
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        throw new InvalidOperationException("Error al conectarse a la base de datos.", ex);
                    default:
                        throw new Exception("Error en la base de datos.", ex);
                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlDR != null) sqlDR.Close();
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                    sqlCom.Dispose();
                }
                if (sqlCon != null)
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                }
            }
            return result;
        }
        public List<ent_Incidentes> Consulta_RMA(string sIncidenteP, string sPaqueteP)
        {
            SqlCommand sqlCom = null;
            SqlDataReader sqlDR = null;
            List<ent_Incidentes> result = null;
            try
            {
                result = new List<ent_Incidentes>();

                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                sqlCom = new SqlCommand("PWin_Consultas", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.Parameters.Add(new SqlParameter("@nTipoConsulta", SqlDbType.Int)).Value = 51;
                sqlCom.Parameters.Add(new SqlParameter("@nParametro1", SqlDbType.Int)).Value = 0;
                sqlCom.Parameters.Add(new SqlParameter("@sParametro1", SqlDbType.NVarChar)).Value = sIncidenteP;
                sqlCom.Parameters.Add(new SqlParameter("@nParametro2", SqlDbType.Int)).Value = 0;
                sqlCom.Parameters.Add(new SqlParameter("@sParametro2", SqlDbType.NVarChar)).Value = sPaqueteP;

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    result.Add(new ent_Incidentes()
                    {
                        D001_sIDArea = sqlDR.GetString(sqlDR.GetOrdinal("Id_Area")),
                        D002_sArea = sqlDR.GetString(sqlDR.GetOrdinal("Area")),
                        D010_sIncidente = sqlDR.GetString(sqlDR.GetOrdinal("Incidente")),
                        D011_fAlta = sqlDR.GetDateTime(sqlDR.GetOrdinal("AltaIncidente")),
                        D012_sDescripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion")),
                        D013_bRetroalimentado = sqlDR.GetBoolean(sqlDR.GetOrdinal("Retroalimentacion")),
                        D014_sNotaLibera = sqlDR.GetString(sqlDR.GetOrdinal("NotaLibera")),
                        D015_bPPMC = sqlDR.GetBoolean(sqlDR.GetOrdinal("PPMC")),
                        lstPaquetes = new List<ent_Paquetes>(),
                        lstObservaciones = new List<ent_Observaciones>()
                    });
                }

                sqlDR.NextResult();
                while (sqlDR.Read())
                {
                    string sIncidente = sqlDR.GetString(sqlDR.GetOrdinal("Incidente")).Trim().ToUpper();

                    result.Find(item => item.D010_sIncidente.Trim().ToUpper() == sIncidente).lstPaquetes.Add(new ent_Paquetes()
                    {
                        D000_sIncidente = sIncidente,
                        D010_sPaquete = sqlDR.GetString(sqlDR.GetOrdinal("Paquete")),
                        D020_fAlta = sqlDR.GetDateTime(sqlDR.GetOrdinal("AltaPaquete")),
                        D031_bDOC_PRU = sqlDR.GetBoolean(sqlDR.GetOrdinal("DOC_PRU")),
                        D033_bNotificado = sqlDR.GetBoolean(sqlDR.GetOrdinal("Notificado")),
                        D032_sObservaciones = sqlDR.GetString(sqlDR.GetOrdinal("Observaciones")),
                        D035_nDesarrollador = sqlDR.GetInt32(sqlDR.GetOrdinal("Id_Desa")),
                        D036_sDesarrollador = sqlDR.GetString(sqlDR.GetOrdinal("Nombre")),
                        D034_sEstatus = sqlDR.GetString(sqlDR.GetOrdinal("Estado")),
                        lstRDL = new List<ent_RDLs>(),
                        lstObservaciones = new List<ent_Observaciones>()
                    });
                }

                sqlDR.NextResult();
                while (sqlDR.Read())
                {
                    string sPaquete = sqlDR.GetString(sqlDR.GetOrdinal("Paquete"));

                    foreach (ent_Incidentes itemI in result)
                    {
                        if (itemI.lstPaquetes.FindAll(itemP => itemP.D010_sPaquete == sPaquete).Count > 0)
                        {
                            itemI.lstPaquetes.Find(itemP => itemP.D010_sPaquete == sPaquete).lstRDL.Add(new ent_RDLs()
                            {
                                D010_nRDL = sqlDR.GetInt32(sqlDR.GetOrdinal("RDL")),
                                D011_sPaquete = sPaquete,
                                D012_fMovimiento = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaInicio")),
                                D013_nEstatus = sqlDR.GetInt32(sqlDR.GetOrdinal("Id_Estado")),
                                D020_sEstatus = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"))
                            });
                        }
                    }
                }

                sqlDR.NextResult();
                while (sqlDR.Read())
                {
                    string sIdIncidente = sqlDR.GetString(sqlDR.GetOrdinal("IdIncidente"));
                    result.ForEach(item =>
                        {
                            if (item.D010_sIncidente == sIdIncidente)
                            {
                                item.lstObservaciones.Add(new ent_Observaciones()
                                {
                                    D000_nID = sqlDR.GetInt32(sqlDR.GetOrdinal("ID")),
                                    D001_sIdIncidentes = sIdIncidente,
                                    D002_sIdPaquete = null,
                                    D010_dMensaje = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaCaptura")),
                                    D020_sMensaje = sqlDR.GetString(sqlDR.GetOrdinal("Observaciones"))
                                });
                            }
                        });
                }

                sqlDR.NextResult();
                while (sqlDR.Read())
                {
                    string sIdIncidente = sqlDR.GetString(sqlDR.GetOrdinal("IdIncidente"));
                    string sIdPaquete = sqlDR.GetString(sqlDR.GetOrdinal("IdPaquete"));
                    result.ForEach(itemI =>
                    {
                        if (itemI.D010_sIncidente == sIdIncidente)
                        {
                            itemI.lstPaquetes.ForEach(itemP =>
                            {
                                if (itemP.D010_sPaquete == sIdPaquete)
                                {
                                    itemP.lstObservaciones.Add(new ent_Observaciones()
                                    {
                                        D000_nID = sqlDR.GetInt32(sqlDR.GetOrdinal("ID")),
                                        D001_sIdIncidentes = sIdIncidente,
                                        D002_sIdPaquete = sIdPaquete,
                                        D010_dMensaje = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaCaptura")),
                                        D020_sMensaje = sqlDR.GetString(sqlDR.GetOrdinal("Observaciones"))
                                    });
                                }
                            });
                        }
                    });
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        throw new InvalidOperationException("Error al conectarse a la base de datos.", ex);
                    default:
                        throw new Exception("Error en la base de datos.", ex);
                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlDR != null) sqlDR.Close();
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                    sqlCom.Dispose();
                }
                if (sqlCon != null)
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                }
            }
            return result;
        }
    }
}
