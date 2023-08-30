using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using mTextEnum = UtileriasComunes.ManejoEnumTextos;
using mExc = UtileriasComunes.ManejoErrores;
using mLog = UtileriasComunes.ManejoLog;
using mCrip = UtileriasComunes.UtileriasEncripta;

namespace UtileriasBaseDatos
{
    public class Conect_BD_SQL
    {
        private SqlConnection sqlCon { get; set; }
        private SqlCommand sqlCom { get; set; }
        private SqlDataAdapter sqlDatA { get; set; }

        public Conect_BD_SQL(string sConection)
        {
            if (sqlCon == null) sqlCon = new SqlConnection(mCrip.EncriptDecript.Desencriptar(ConfigurationManager.ConnectionStrings[sConection].ConnectionString.Trim(), mTextEnum.Encriptador.TipoLlave.CadenaConexion));

            if (sqlCon.State != ConnectionState.Open) sqlCon.Open();
        }

        public DataTable ConsultaDataTable(string sCommando)
        {
            DataTable dtResult = null;
            try
            {
                dtResult = ConsultaDataTable(sCommando, null, EnumTextos.EnumComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw ex;
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw ex;
            }
            finally
            { }
            return dtResult;
        }
        public DataTable ConsultaDataTable(string sCommando, List<Entidades.entParametro> lstParametro)
        {
            DataTable dtResult = null;
            try
            {
                dtResult = ConsultaDataTable(sCommando, lstParametro, EnumTextos.EnumComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw ex;
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw ex;
            }
            finally
            { }
            return dtResult;
        }
        public DataTable ConsultaDataTable(string sCommando, List<Entidades.entParametro> lstParametro, EnumTextos.EnumComponentes.BD_TipoComando TipoComando)
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
                throw ex;
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw ex;
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
                dsResult = ConsultaDataSet(sCommando, null, EnumTextos.EnumComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw ex;
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw ex;
            }
            finally
            { }
            return dsResult;
        }
        public DataSet ConsultaDataSet(string sCommando, List<Entidades.entParametro> lstParametro)
        {
            DataSet dsResult = null;
            try
            {
                dsResult = ConsultaDataSet(sCommando, lstParametro, EnumTextos.EnumComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw ex;
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw ex;
            }
            finally
            { }
            return dsResult;
        }
        public DataSet ConsultaDataSet(string sCommando, List<Entidades.entParametro> lstParametro, EnumTextos.EnumComponentes.BD_TipoComando TipoComando)
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
                        sqlCom.Parameters.Add(AgregarParametro(itemP));
                    });
                }
                sqlCom.CommandType = CommandType.Text;
                if (TipoComando == EnumTextos.EnumComponentes.BD_TipoComando.StoreProcedure) sqlCom.CommandType = CommandType.StoredProcedure;

                sqlDatA = new SqlDataAdapter(sqlCom);
                sqlDatA.Fill(dsResult);
            }
            catch (ApplicationException ex)
            {
                throw new mExc.ErroresAplicacion(mExc.ErroresAplicacion.TipoError.ErrorAplicacion, 0, ex.Message, ex);
            }
            catch (SqlException ex)
            {
                throw new mExc.ErroresAplicacion(mExc.ErroresAplicacion.TipoError.ErrorSQL, ex.Number, "Error al procesar información en la Base de Datos", ex);
            }
            catch (Exception ex)
            {
                throw new mExc.ErroresAplicacion(mExc.ErroresAplicacion.TipoError.ErrorGenerico, -1, "Error al procesar información en la Base de Datos", ex);
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
                nRenglonesAectados = EjecutaComando(sCommando, null, EnumTextos.EnumComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw ex;
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw ex;
            }
            finally
            { }
            return nRenglonesAectados;
        }
        public int EjecutaComando(string sCommando, List<Entidades.entParametro> lstParametro)
        {
            int nRenglonesAectados = 0;
            try
            {
                nRenglonesAectados = EjecutaComando(sCommando, lstParametro, EnumTextos.EnumComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
                Finally();
                throw ex;
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
                Finally();
                throw ex;
            }
            finally
            { }
            return nRenglonesAectados;
        }
        public int EjecutaComando(string sCommando, List<Entidades.entParametro> lstParametro, EnumTextos.EnumComponentes.BD_TipoComando TipoComando)
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
                        sqlCom.Parameters.Add(AgregarParametro(itemP));
                    });
                }
                sqlCom.CommandType = CommandType.Text;
                if (TipoComando == EnumTextos.EnumComponentes.BD_TipoComando.StoreProcedure) sqlCom.CommandType = CommandType.StoredProcedure;

                nRenglonesAectados = sqlCom.ExecuteNonQuery();
            }
            catch (ApplicationException ex)
            {
                throw new mExc.ErroresAplicacion(mExc.ErroresAplicacion.TipoError.ErrorAplicacion, 0, ex.Message, ex);
            }
            catch (SqlException ex)
            {
                throw new mExc.ErroresAplicacion(mExc.ErroresAplicacion.TipoError.ErrorSQL, ex.Number, "Error al procesar información en la Base de Datos", ex);
            }
            catch (Exception ex)
            {
                throw new mExc.ErroresAplicacion(mExc.ErroresAplicacion.TipoError.ErrorGenerico, -1, "Error al procesar información en la Base de Datos", ex);
            }
            finally
            { }
            return nRenglonesAectados;
        }

        private SqlParameter AgregarParametro(Entidades.entParametro itemParam)
        {
            SqlParameter sqlParam = new SqlParameter();

            sqlParam.ParameterName = itemParam.p_Nombre;
            switch (itemParam.p_Tipo)
            {
                case EnumTextos.EnumComponentes.BD_TipoParametro.EnteroNor:
                    sqlParam.SqlDbType = SqlDbType.Int;
                    break;
                case EnumTextos.EnumComponentes.BD_TipoParametro.EnteroLar:
                    sqlParam.SqlDbType = SqlDbType.BigInt;
                    break;
                case EnumTextos.EnumComponentes.BD_TipoParametro.EnteroCor:
                    sqlParam.SqlDbType = SqlDbType.SmallInt;
                    break;
                case EnumTextos.EnumComponentes.BD_TipoParametro.TextoNor:
                    sqlParam.SqlDbType = SqlDbType.NVarChar;
                    break;
                case EnumTextos.EnumComponentes.BD_TipoParametro.TextoCor:
                    sqlParam.SqlDbType = SqlDbType.Char;
                    break;
                case EnumTextos.EnumComponentes.BD_TipoParametro.TextoLar:
                    sqlParam.SqlDbType = SqlDbType.NText;
                    break;
                case EnumTextos.EnumComponentes.BD_TipoParametro.Fecha:
                    sqlParam.SqlDbType = SqlDbType.Date;
                    break;
                case EnumTextos.EnumComponentes.BD_TipoParametro.FechaHora:
                    sqlParam.SqlDbType = SqlDbType.DateTime;
                    break;
                case EnumTextos.EnumComponentes.BD_TipoParametro.SiNo:
                    sqlParam.SqlDbType = SqlDbType.Bit;
                    break;
            }

            if (itemParam.p_Tamaño != null) sqlParam.Size = itemParam.p_Tamaño.Value;

            sqlParam.Value = DBNull.Value;
            if (itemParam.p_Valor != null) sqlParam.Value = itemParam.p_Valor;

            return sqlParam;
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
