using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using IBM.Data.Informix;
using mLog = ControlaComponentes.ManejoLog;
using mExc = ControlaComponentes.ManejoExcepciones;


namespace ConectBaseDatos
{
    public class Conect_BD_IBM
    {
        private IfxConnection ifxCon { get; set; }
        private IfxCommand ifxCom { get; set; }
        private IfxDataAdapter ifxDatA { get; set; }

        public Conect_BD_IBM(string sConection)
        {
            if (ifxCon == null) ifxCon = new IfxConnection(ConfigurationManager.ConnectionStrings[sConection].ConnectionString.Trim());

            if (ifxCon.State != ConnectionState.Open) ifxCon.Open();
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
                if (ifxCom == null) ifxCom = new IfxCommand(sCommando, ifxCon);

                ifxCom.CommandText = sCommando;
                ifxCom.Parameters.Clear();
                if (lstParametro != null)
                {
                    lstParametro.ForEach(itemP =>
                    {
                        IfxParameter ifxParam = new IfxParameter();

                        ifxParam.ParameterName = itemP.p_Nombre;
                        switch (itemP.p_Tipo)
                        {
                            case Array.ListaComponentes.BD_TipoParametro.Char:
                                ifxParam.IfxType = IfxType.Char;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.Integer:
                                ifxParam.IfxType = IfxType.Integer;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.sMalInt:
                                ifxParam.IfxType = IfxType.SmallInt;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.DateTime:
                                ifxParam.IfxType = IfxType.DateTime;
                                break;
                            default:
                                ifxParam.IfxType = IfxType.NVarChar;
                                break;
                        }

                        if (itemP.p_Tamaño != null) ifxParam.Size = itemP.p_Tamaño.Value;

                        ifxParam.Value = DBNull.Value;
                        if (itemP.p_Valor != null) ifxParam.Value = itemP.p_Valor;

                        ifxCom.Parameters.Add(ifxParam);
                    });
                }
                ifxCom.CommandType = CommandType.Text;
                if (TipoComando == Array.ListaComponentes.BD_TipoComando.StoreProcedure) ifxCom.CommandType = CommandType.StoredProcedure;

                ifxDatA = new IfxDataAdapter(ifxCom);
                ifxDatA.Fill(dsResult);
            }
            catch (IfxException ex)
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
                if (ifxCom == null) ifxCom = new IfxCommand(sCommando, ifxCon);

                ifxCom.CommandText = sCommando;

                ifxCom.Parameters.Clear();
                if (lstParametro != null)
                {
                    lstParametro.ForEach(itemP =>
                    {
                        IfxParameter ifxParam = new IfxParameter();

                        ifxParam.ParameterName = itemP.p_Nombre;
                        switch (itemP.p_Tipo)
                        {
                            case Array.ListaComponentes.BD_TipoParametro.Char:
                                ifxParam.IfxType = IfxType.Char;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.Integer:
                                ifxParam.IfxType = IfxType.Integer;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.sMalInt:
                                ifxParam.IfxType = IfxType.SmallInt;
                                break;
                            case Array.ListaComponentes.BD_TipoParametro.DateTime:
                                ifxParam.IfxType = IfxType.DateTime;
                                break;
                            default:
                                ifxParam.IfxType = IfxType.NVarChar;
                                break;
                        }

                        if (itemP.p_Tamaño != null) ifxParam.Size = itemP.p_Tamaño.Value;

                        ifxParam.Value = DBNull.Value;
                        if (itemP.p_Valor != null) ifxParam.Value = itemP.p_Valor;

                        ifxCom.Parameters.Add(ifxParam);
                    });
                }
                ifxCom.CommandType = CommandType.Text;
                if (TipoComando == Array.ListaComponentes.BD_TipoComando.StoreProcedure) ifxCom.CommandType = CommandType.StoredProcedure;

                nRenglonesAectados = ifxCom.ExecuteNonQuery();
            }
            catch (IfxException ex)
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
            if (ifxDatA != null) ifxDatA.Dispose();
            if (ifxCom != null) ifxCom.Dispose();
            if (ifxCon != null)
            {
                ifxCon.Close();
                ifxCon.Dispose();
            }
        }
    }
}
