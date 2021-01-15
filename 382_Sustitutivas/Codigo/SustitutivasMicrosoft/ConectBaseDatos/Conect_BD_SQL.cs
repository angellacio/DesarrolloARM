using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using mLog = ControlaComponentes.ManejoLog;
using mExc = ControlaComponentes.ManejoExcepciones;

namespace ConectBaseDatos
{
    public class Conect_BD_SQL
    {
        private SqlConnection sqlCon { get; set; }
        private SqlCommand sqlCom { get; set; }
        private SqlDataAdapter sqlDatA { get; set; }

        public Conect_BD_SQL(string sConection)
        {
            if (sqlCon == null) sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings[sConection].ConnectionString.Trim());

            if (sqlCon.State != ConnectionState.Open) sqlCon.Open();
        }

        public DataTable ConsultaDataTable(string sCommando)
        {
            DataTable dtResult = null;
            try
            {
                dtResult = ConsultaDataTable(sCommando, null, Array.ListaComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return dtResult;
        }
        public DataTable ConsultaDataTable(string sCommando, List<Ent.entParametro> lstParametro)
        {
            DataTable dtResult = null;
            try
            {
                dtResult = ConsultaDataTable(sCommando, lstParametro, Array.ListaComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return dtResult;
        }
        public DataTable ConsultaDataTable(string sCommando, List<Ent.entParametro> lstParametro, Array.ListaComponentes.BD_TipoComando TipoComando)
        {
            DataTable dtResult = null;
            try
            {
                dtResult = ConsultaDataSet(sCommando, lstParametro, TipoComando).Tables[0];
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return dtResult;
        }

        public DataSet ConsultaDataSet(string sCommando)
        {
            DataSet dsResult = null;
            try
            {
                dsResult = ConsultaDataSet(sCommando, null, Array.ListaComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return dsResult;
        }
        public DataSet ConsultaDataSet(string sCommando, List<Ent.entParametro> lstParametro)
        {
            DataSet dsResult = null;
            try
            {
                dsResult = ConsultaDataSet(sCommando, lstParametro, Array.ListaComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return dsResult;
        }
        public DataSet ConsultaDataSet(string sCommando, List<Ent.entParametro> lstParametro, Array.ListaComponentes.BD_TipoComando TipoComando)
        {
            DataSet dsResult = null;
            try
            {
                dsResult = new DataSet();
                if (sqlCom == null) sqlCom = new SqlCommand(sCommando, sqlCon);

                sqlCom.CommandText = sCommando;
                sqlCom.Parameters.Clear();
                if (lstParametro != null)
                {
                    lstParametro.ForEach(itemP =>
                    {
                        SqlParameter sqlParam = new SqlParameter();

                        sqlParam.ParameterName = itemP.p_Nombre;
                        switch (itemP.p_Tipo)
                        {
                            case Array.ListaComponentes.BD_TipoParametro.Char:
                                sqlParam.SqlDbType = SqlDbType.Char;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.Integer:
                                sqlParam.SqlDbType = SqlDbType.Int;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.sMalInt:
                                sqlParam.SqlDbType = SqlDbType.SmallInt;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.DateTime:
                                sqlParam.SqlDbType = SqlDbType.DateTime;
                                break;
                            default:
                                sqlParam.SqlDbType = SqlDbType.NVarChar;
                                break;
                        }

                        if (itemP.p_Tamaño != null) sqlParam.Size = itemP.p_Tamaño.Value;

                        sqlParam.Value = DBNull.Value;
                        if (itemP.p_Valor != null) sqlParam.Value = itemP.p_Valor;
                        
                        sqlCom.Parameters.Add(sqlParam);
                    });
                }
                sqlCom.CommandType = CommandType.Text;
                if (TipoComando == Array.ListaComponentes.BD_TipoComando.StoreProcedure) sqlCom.CommandType = CommandType.StoredProcedure;

                sqlDatA = new SqlDataAdapter(sqlCom);
                sqlDatA.Fill(dsResult);
            }
            catch (SqlException ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return dsResult;
        }

        public int EjecutaComando(string sCommando)
        {
            int nRenglonesAectados = 0;
            try
            {
                nRenglonesAectados = EjecutaComando(sCommando, null, Array.ListaComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return nRenglonesAectados;
        }
        public int EjecutaComando(string sCommando, List<Ent.entParametro> lstParametro)
        {
            int nRenglonesAectados = 0;
            try
            {
                nRenglonesAectados = EjecutaComando(sCommando, lstParametro, Array.ListaComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return nRenglonesAectados;
        }
        public int EjecutaComando(string sCommando, List<Ent.entParametro> lstParametro, Array.ListaComponentes.BD_TipoComando TipoComando)
        {
            int nRenglonesAectados = 0;
            try
            {
                if (sqlCom == null) sqlCom = new SqlCommand(sCommando, sqlCon);

                sqlCom.CommandText = sCommando;

                sqlCom.Parameters.Clear();
                if (lstParametro != null)
                {
                    lstParametro.ForEach(itemP =>
                    {
                        SqlParameter sqlParam = new SqlParameter();

                        sqlParam.ParameterName = itemP.p_Nombre;
                        switch (itemP.p_Tipo)
                        {
                            case Array.ListaComponentes.BD_TipoParametro.Char:
                                sqlParam.SqlDbType = SqlDbType.Char;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.Integer:
                                sqlParam.SqlDbType = SqlDbType.Int;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.sMalInt:
                                sqlParam.SqlDbType = SqlDbType.SmallInt;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.DateTime:
                                sqlParam.SqlDbType = SqlDbType.DateTime;
                                break;
                            default:
                                sqlParam.SqlDbType = SqlDbType.NVarChar;
                                break;
                        }

                        if (itemP.p_Tamaño != null) sqlParam.Size = itemP.p_Tamaño.Value;

                        sqlParam.Value = DBNull.Value;
                        if (itemP.p_Valor != null) sqlParam.Value = itemP.p_Valor;

                        sqlCom.Parameters.Add(sqlParam);
                    });
                }
                sqlCom.CommandType = CommandType.Text;
                if (TipoComando == Array.ListaComponentes.BD_TipoComando.StoreProcedure) sqlCom.CommandType = CommandType.StoredProcedure;

                nRenglonesAectados = sqlCom.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                throw new mExc.ErrorGeneral(ex.Message);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                throw new mExc.ErrorAplicacion(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                throw new mExc.ErrorGeneral(ex.Message);
            }
            finally
            { }
            return nRenglonesAectados;
        }

        public void Finally()
        {
            if (sqlDatA != null) sqlDatA.Dispose();
            if (sqlCom != null) sqlCom.Dispose();
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
        }
    }
}
