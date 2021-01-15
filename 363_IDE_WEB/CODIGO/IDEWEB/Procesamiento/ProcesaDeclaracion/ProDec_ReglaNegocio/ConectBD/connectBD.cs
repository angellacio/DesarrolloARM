using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ent = ProDec_ReglaNegocio.Entidades;

namespace ProDec_ReglaNegocio.ConectBD
{
    public class connectBD
    {
        public enum sTipConect
        {
            DyPUtil = 1,
            IDE_WEB = 2
        }
        private sTipConect sTC { get; set; }
        private string sConnect
        {
            get
            {
                string sResult = "";

                switch (sTC)
                {
                    case sTipConect.DyPUtil:
                        sResult = ConfigurationManager.ConnectionStrings["conBDUtil"].ConnectionString.ToString().Trim();
                        break;
                    case sTipConect.IDE_WEB:
                        sResult = ConfigurationManager.ConnectionStrings["conBDIdeWeb"].ConnectionString.ToString().Trim();
                        break;
                    default:
                        throw new ApplicationException("Esta mal configurado el string de coneccion.");
                }

                return sResult;
            }
        }
        private SqlConnection sqlCon { get; set; }

        public connectBD(sTipConect sTipoConnect)
        {
            sTC = sTipoConnect;
        }

        public Ent.catSencillo ConsultaDatos(string StoreProcedure, List<Ent.catParametros> lParametros)
        {
            Ent.catSencillo result = null;
            SqlCommand sqlCom = null;
            SqlDataReader sqlDR = null;
            try
            {
                sqlCon = new SqlConnection(sConnect);

                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                sqlCom = sqlCon.CreateCommand();
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.CommandText = StoreProcedure;
                if (lParametros != null && lParametros.Count > 0)
                {
                    lParametros.ForEach(item =>
                    {
                        sqlCom.Parameters.Add(new SqlParameter(item.sNombre, item.sqlTipo)).Value = item.oDato;
                    });
                }

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    result = new Ent.catSencillo()
                    {
                        sAcronimo = sqlDR.GetString(sqlDR.GetOrdinal("")),
                        sDescripcion = sqlDR.GetString(sqlDR.GetOrdinal("")),
                    };
                }
            }
            catch (Exception ex)
            {
                ManejoErrores.MensajeError("Error.", ex);
            }
            finally
            {
                if (sqlDR != null)
                {
                    sqlDR.Close();
                    sqlDR.Dispose();
                }
                if (sqlCon != null)
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                }
            }
            return result;
        }

        public List<Ent.catSencillo> ConsultaEstados(string StoreProcedure, List<Ent.catParametros> lParametros)
        {
            List<Ent.catSencillo> result = null;
            SqlCommand sqlCom = null;
            SqlDataReader sqlDR = null;
            try
            {
                sqlCon = new SqlConnection(sConnect);
                result = new List<Ent.catSencillo>();

                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                sqlCom = sqlCon.CreateCommand();
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.CommandText = StoreProcedure;
                if (lParametros != null && lParametros.Count > 0)
                {
                    lParametros.ForEach(item =>
                    {
                        sqlCom.Parameters.Add(new SqlParameter(item.sNombre, item.sqlTipo)).Value = item.oDato;
                    });
                }

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    result.Add(new Ent.catSencillo()
                    {
                        nID = int.Parse(sqlDR.GetString(sqlDR.GetOrdinal("IdEstatusRecepcion"))),
                        sID = sqlDR.GetString(sqlDR.GetOrdinal("IdEstatusRecepcion")),
                        sDescripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"))
                    });
                }
            }
            catch (Exception ex)
            {
                ManejoErrores.MensajeError("Error.", ex);
            }
            finally
            {
                if (sqlDR != null)
                {
                    sqlDR.Close();
                    sqlDR.Dispose();
                }
                if (sqlCon != null)
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                }
            }
            return result;
        }

        //METODO PARA RESERVAR LAS DECLARACIONES
        public List<Ent.catConsultaDeclas> ConsultaReservaDeclas(string StoreProcedure, List<Ent.catParametros> lParametros)
        {
            List<Ent.catConsultaDeclas> result = null;
            SqlCommand sqlCom = null;
            SqlDataReader sqlDR = null;

            try
            {
                sqlCon = new SqlConnection(sConnect);
                result = new List<Ent.catConsultaDeclas>();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                sqlCom = sqlCon.CreateCommand();
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.CommandText = StoreProcedure;

                if (lParametros != null && lParametros.Count > 0)
                {
                    lParametros.ForEach(item => { sqlCom.Parameters.Add(new SqlParameter(item.sNombre, item.sqlTipo)).Value = item.oDato; });
                }

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    result.Add(new Ent.catConsultaDeclas()
                    {

                        nFolio = sqlDR.GetInt32(sqlDR.GetOrdinal("Folio")),
                        sNombreArchivo = sqlDR.GetString(sqlDR.GetOrdinal("NombreArchivo")),
                        sNombreFisico = sqlDR.GetString(sqlDR.GetOrdinal("ArchivoFisico")),
                        nMedioRecepcion = sqlDR.GetInt16(sqlDR.GetOrdinal("IdMedioRecepcion"))
                    });
                }
            }
            catch (Exception ex)
            {
                ManejoErrores.MensajeError("Error.", ex);
                throw ex;
            }
            finally
            {
                if (sqlDR != null)
                {
                    sqlDR.Close();
                    sqlDR.Dispose();
                }
                if (sqlCon != null)
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                }
            }
            return result;
        }//FIN METODO ConsultaReservaDeclas

        //ProcesaValidacion
        public void ProcesoValidacion(string StoreProcedure, List<Ent.catParametros> lParametros)
        {
            SqlCommand sqlCom = null;
            try
            {
                sqlCon = new SqlConnection(sConnect);

                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                sqlCom = sqlCon.CreateCommand();
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.CommandText = StoreProcedure;
                if (lParametros != null && lParametros.Count > 0)
                {
                    lParametros.ForEach(item =>
                    {
                        sqlCom.Parameters.Add(new SqlParameter(item.sNombre, item.sqlTipo)).Value = item.oDato;
                    });
                }

                sqlCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ManejoErrores.MensajeError("Error.", ex);
            }
            finally
            {
                if (sqlCon != null)
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                }
            }
        }

    }
}
