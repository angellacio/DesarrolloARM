using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MonitorEntidades;

namespace MonitorReglaNegocios.Datos
{
    public class ModificaInformacion
    {
        private SqlConnection sqlCon { get; set; }
        private string sConexionString { get { return ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString.Trim(); } }

        public ModificaInformacion()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(sConexionString);
            }
        }

        public void ActualizaDatos(string nIncidente, string sPaquete, Nullable<Boolean> bRetro, Nullable<Boolean> bDocPru, string sIdentificador, string sIdentificadorPaq, string sNotLib, string sArea, Nullable<int> nDesarrollador, Nullable<Boolean> bPPMC, string nIncidenteNew, string sPaqueteNew, Nullable<Boolean> bNotificado, string sTObservacionesI, string sTObservaciones, string sPAqObservaciones)
        {
            SqlCommand sqlCom = null;
            SqlDataReader sqlDR = null;
            List<ent_Incidentes> result = null;
            try
            {
                result = new List<ent_Incidentes>();

                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                sqlCom = new SqlCommand("PWin_ActualizaSeguimiento", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.Parameters.Add(new SqlParameter("@Incidente", SqlDbType.NVarChar)).Value = nIncidente;

                if (sPaquete != null) sqlCom.Parameters.Add(new SqlParameter("@Paquete", SqlDbType.NVarChar)).Value = sPaquete; else sqlCom.Parameters.Add(new SqlParameter("@Paquete", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (bRetro != null) sqlCom.Parameters.Add(new SqlParameter("@Retroalimentado", SqlDbType.Bit)).Value = bRetro; else sqlCom.Parameters.Add(new SqlParameter("@Retroalimentado", SqlDbType.Bit)).Value = DBNull.Value;
                if (bDocPru != null) sqlCom.Parameters.Add(new SqlParameter("@DocPru", SqlDbType.Bit)).Value = bDocPru; else sqlCom.Parameters.Add(new SqlParameter("@DocPru", SqlDbType.Bit)).Value = DBNull.Value;
                if (sIdentificador != null) sqlCom.Parameters.Add(new SqlParameter("@Identificador", SqlDbType.NVarChar)).Value = sIdentificador; else sqlCom.Parameters.Add(new SqlParameter("@Identificador", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (sIdentificadorPaq != null) sqlCom.Parameters.Add(new SqlParameter("@IdentificadorPaq", SqlDbType.NVarChar)).Value = sIdentificadorPaq; else sqlCom.Parameters.Add(new SqlParameter("@IdentificadorPaq", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (sNotLib != null) sqlCom.Parameters.Add(new SqlParameter("@NotLiberar", SqlDbType.NVarChar)).Value = sNotLib; else sqlCom.Parameters.Add(new SqlParameter("@NotLiberar", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (sArea != null) sqlCom.Parameters.Add(new SqlParameter("@sArea", SqlDbType.NVarChar)).Value = sArea; else sqlCom.Parameters.Add(new SqlParameter("@sArea", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (nDesarrollador != null) sqlCom.Parameters.Add(new SqlParameter("@nDesarrollador", SqlDbType.Int)).Value = nDesarrollador; else sqlCom.Parameters.Add(new SqlParameter("@nDesarrollador", SqlDbType.Int)).Value = DBNull.Value;
                if (bPPMC != null) sqlCom.Parameters.Add(new SqlParameter("@PPMC", SqlDbType.Bit)).Value = bPPMC; else sqlCom.Parameters.Add(new SqlParameter("@PPMC", SqlDbType.Bit)).Value = DBNull.Value;
                if (nIncidenteNew != null) sqlCom.Parameters.Add(new SqlParameter("@IncidenteNew", SqlDbType.NVarChar)).Value = nIncidenteNew; else sqlCom.Parameters.Add(new SqlParameter("@IncidenteNew", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (sPaqueteNew != null) sqlCom.Parameters.Add(new SqlParameter("@PaqueteNew", SqlDbType.NVarChar)).Value = sPaqueteNew; else sqlCom.Parameters.Add(new SqlParameter("@PaqueteNew", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (bNotificado != null) sqlCom.Parameters.Add(new SqlParameter("@Notificado", SqlDbType.Bit)).Value = bNotificado; else sqlCom.Parameters.Add(new SqlParameter("@Notificado", SqlDbType.Bit)).Value = DBNull.Value;
                if (sTObservaciones != null) sqlCom.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.NVarChar)).Value = sTObservaciones; else sqlCom.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (sTObservacionesI != null) sqlCom.Parameters.Add(new SqlParameter("@ObservacionesIns", SqlDbType.NVarChar)).Value = sTObservacionesI; else sqlCom.Parameters.Add(new SqlParameter("@ObservacionesIns", SqlDbType.NVarChar)).Value = DBNull.Value;
                if (sPAqObservaciones != null) sqlCom.Parameters.Add(new SqlParameter("@PaqObservaciones", SqlDbType.NVarChar)).Value = sPAqObservaciones; else sqlCom.Parameters.Add(new SqlParameter("@PaqObservaciones", SqlDbType.NVarChar)).Value = DBNull.Value;


                sqlCom.ExecuteNonQuery();
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
        }

        public List<ent_Observaciones> ManejoDatosObservaciones(int nId, string nIncidente, string sPaquete, string sObservaciones, int nAcciones)
        {
            SqlCommand sqlCom = null;
            SqlDataReader sqlDR = null;
            List<ent_Observaciones> result = null;
            try
            {
                result = new List<ent_Observaciones>();

                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                sqlCom = new SqlCommand("ManejoObservaciones", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;

                sqlCom.Parameters.Add(new SqlParameter("@IdObservacion", SqlDbType.Int)).Value = nId;
                sqlCom.Parameters.Add(new SqlParameter("@Incidente", SqlDbType.NVarChar)).Value = nIncidente;
                if (sPaquete != null) sqlCom.Parameters.Add(new SqlParameter("@Paquete", SqlDbType.NVarChar)).Value = sPaquete; else sqlCom.Parameters.Add(new SqlParameter("@Paquete", SqlDbType.NVarChar)).Value = DBNull.Value;
                sqlCom.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.NVarChar)).Value = sObservaciones;
                sqlCom.Parameters.Add(new SqlParameter("@TipoMov", SqlDbType.Int)).Value = nAcciones;

                sqlDR = sqlCom.ExecuteReader();
                while (sqlDR.Read())
                {
                    string sPaqueteL = null;
                    if (sqlDR["IdPaquete"] != DBNull.Value) sPaqueteL = sqlDR.GetString(sqlDR.GetOrdinal("IdPaquete"));

                    result.Add(new ent_Observaciones()
                    {
                        D000_nID = sqlDR.GetInt32(sqlDR.GetOrdinal("ID")),
                        D001_sIdIncidentes = sqlDR.GetString(sqlDR.GetOrdinal("IdIncidente")),
                        D002_sIdPaquete = sPaqueteL,
                        D010_dMensaje = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaCaptura")),
                        D020_sMensaje = sqlDR.GetString(sqlDR.GetOrdinal("Observaciones"))
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
