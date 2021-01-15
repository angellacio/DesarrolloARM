using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.NetworkInformation;
using System.Data;
using System.Reflection;
using ent = SAT.SUS.Entidades;
using mLog = ControlaComponentes.ManejoLog;
using mExc = ControlaComponentes.ManejoExcepciones;
using mBD = ConectBaseDatos;


namespace SAT.SUS.Negocio
{
    public class RealizaOperacionesIntegracion
    {
        private static int pr_nRenglonesReserva
        {
            get
            {
                int nResult = 0;

                int.TryParse(ConfigurationManager.AppSettings["ServicioRenglonesProcesa"].ToString().Trim(), out nResult);

                return nResult;
            }
        }
        private static string pr_sEquipo
        {
            get
            {
                string sResult = Environment.MachineName;
                if (ConfigurationManager.AppSettings["EquipoNombre"].ToString().Trim() != "")
                    sResult = ConfigurationManager.AppSettings["EquipoNombre"].ToString().Trim();
                return sResult;
            }
        }
        public static string pu_sIP
        {
            get
            {
                string sResult = ConfigurationManager.AppSettings["EquipoIP"].ToString().Trim();
                if (sResult == "")
                {
                    IPHostEntry host;
                    host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress ip in host.AddressList)
                    {
                        if (ip.AddressFamily.ToString() == "InterNetwork")
                        {
                            sResult = ip.ToString();
                        }
                    }
                }
                return sResult;
            }
        }
        public static Boolean pu_bolAuditoria
        {
            get
            {
                Boolean sResult = true;
                if (ConfigurationManager.AppSettings["ServicioMicroAuditable"].ToString().Trim() != "")
                    sResult = (ConfigurationManager.AppSettings["ServicioMicroAuditable"].ToString().ToUpper().Trim() == "1");
                return sResult;
            }
        }
        public static string pu_sUsuarioAplicativo
        {
            get
            {
                string sResult = "usrSustitutivas";
                if (ConfigurationManager.AppSettings["ServicioUsuarioEjecuta"].ToString().Trim() != "")
                    sResult = ConfigurationManager.AppSettings["ServicioUsuarioEjecuta"].ToString().Trim();
                return sResult;
            }
        }
        private static int pr_nIntentos
        {
            get
            {
                int nResult = 0;

                int.TryParse(ConfigurationManager.AppSettings["ServicioNumeroReintentos"].ToString().Trim(), out nResult);

                return nResult;
            }
        }
        //private mBD.Conect_BD_SQL conBD { get; set; }
        private mBD.Conect_BD_IBM conBD { get; set; }

        public List<ent.entDatosConsultar> Informacion_ReservaObten()
        {
            List<ent.entDatosConsultar> lstRenglonesAfectados = null;
            List<mBD.Ent.entParametro> lstParametros = null;
            DataTable dtRenglones = null;
            try
            {
                lstRenglonesAfectados = new List<ent.entDatosConsultar>();
                //lstParametros = new List<ConectBaseDatos.Ent.entParametro>()
                //{
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@nReglonesReserva", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  pr_nRenglonesReserva },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@sEquipo", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 250, p_Valor =  pr_sEquipo },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@nIntentos", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor = pr_nIntentos }
                //};
                //if (conBD == null) conBD = new mBD.Conect_BD_SQL("conSqlTransaccion");
                lstParametros = new List<ConectBaseDatos.Ent.entParametro>()
                {
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "pe_nReglonesReserva", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  pr_nRenglonesReserva },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "pe_sEquipo", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 250, p_Valor =  pr_sEquipo },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "pe_nIntentos", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor = pr_nIntentos }
                };
                if (conBD == null) conBD = new mBD.Conect_BD_IBM("conInfTransaccion");


                dtRenglones = conBD.ConsultaDataTable("spServioMicrosoftObten", lstParametros, mBD.Array.ListaComponentes.BD_TipoComando.StoreProcedure);

                foreach (DataRow ItemDR in dtRenglones.Rows)
                {
                    lstRenglonesAfectados.Add(new ent.entDatosConsultar(ItemDR));
                }
            }
            catch (mExc.ErrorAplicacion ex) { throw ex; }
            catch (mExc.ErrorGeneral ex) { throw ex; }
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
            {
                if (conBD != null)
                {
                    conBD.Finally();
                    conBD = null;
                }
            }
            return lstRenglonesAfectados;
        }

        public int ActualizaRegistros(int nIdSusWSM, int nRenglones, int nEstatus, String sPagado, String sLineaCaptura, String sTipoOperacion, String sMensaje, String sNumOperBanco, String sFoperacion, String sHoperacion, String sNumOpeDecla, String sPresentacion, DateTime dModificacion, int nRenEstatus, Nullable<DateTime> dOperacion, Nullable<DateTime> dPresentacion)
        {
            List<mBD.Ent.entParametro> lstParametros = null;
            int nRenglonesAfectados = 0;
            try
            {
                //lstParametros = new List<ConectBaseDatos.Ent.entParametro>()
                //{
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_nIdSusWSM", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  nIdSusWSM },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_nRenglones", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  nRenglones },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@nEstatus", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  nEstatus },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sPagado", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 2, p_Valor =  sPagado },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sLineaCaptura", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 20, p_Valor =  sLineaCaptura },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sTipoOperacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 2, p_Valor =  sTipoOperacion },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sMensaje", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 250, p_Valor =  sMensaje },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sNumOperBanco", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 14, p_Valor =  sNumOperBanco },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sFoperacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 10, p_Valor =  sFoperacion },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sHoperacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 5, p_Valor =  sHoperacion },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sNumOpeDecla", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 14, p_Valor =  sNumOpeDecla },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_sPresentacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 8, p_Valor =  sPresentacion },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_dModificacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.DateTime, p_Valor =  dModificacion },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_nEstatus", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  nRenEstatus },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_dOperacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.DateTime, p_Valor =  dOperacion },
                //    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "@p_dPresentacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.DateTime, p_Valor =  dPresentacion }
                //};
                //if (conBD == null) conBD = new mBD.Conect_BD_SQL("conSqlTransaccion");
                lstParametros = new List<ConectBaseDatos.Ent.entParametro>()
                {
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_nIdSusWSM", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  nIdSusWSM },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_nRenglones", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  nRenglones },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_nGEstatus", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  nEstatus },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sPagado", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 2, p_Valor =  sPagado },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sLineaCaptura", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 20, p_Valor =  sLineaCaptura },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sTipoOperacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 2, p_Valor =  sTipoOperacion },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sMensaje", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 250, p_Valor =  sMensaje },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sNumOperBanco", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 14, p_Valor =  sNumOperBanco },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sFOperacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 10, p_Valor =  sFoperacion },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sHOperacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 5, p_Valor =  sHoperacion },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sNumOpeDecla", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 14, p_Valor =  sNumOpeDecla },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_sPresentacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.nVarChar, p_Tamaño = 8, p_Valor =  sPresentacion },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_dModificacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.DateTime, p_Valor =  dModificacion },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_nEstatus", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.Integer, p_Valor =  nRenEstatus },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_dOperacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.DateTime, p_Valor =  dOperacion },
                    new ConectBaseDatos.Ent.entParametro() { p_Nombre = "p_dPresentacion", p_Tipo = mBD.Array.ListaComponentes.BD_TipoParametro.DateTime, p_Valor =  dPresentacion }
                };
                if (conBD == null) conBD = new mBD.Conect_BD_IBM("conInfTransaccion");

                nRenglonesAfectados = conBD.EjecutaComando("spServioMicrosoftActualiza", lstParametros, mBD.Array.ListaComponentes.BD_TipoComando.StoreProcedure);
            }
            catch (mExc.ErrorAplicacion ex) { throw ex; }
            catch (mExc.ErrorGeneral ex) { throw ex; }
            catch (ApplicationException ex)
            {
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
            }
            catch (Exception ex)
            {
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
            }
            finally
            {
                mLog.ActualizaLog.mensajeAuditoria(string.Format(" -------------------------------------- Actualizando Datos BD ({16}). SP spServioMicrosoftActualiza :: nIdSusWSM {0} :: nRenglones {1} :: nEstatus {2} :: sPagado {3} :: sLineaCaptura {4} :: sTipoOperacion {5} :: sMensaje {6} :: sNumOperBanco {7} :: sFoperacion {8} :: sHoperacion {9} :: sNumOpeDecla {10} :: sPresentacion {11} :: dModificacion {12} :: nRenEstatus {13} :: dOperacion {14} :: dPresentacion {15}", nIdSusWSM, nRenglones, nEstatus, sPagado, sLineaCaptura, sTipoOperacion, sMensaje, sNumOperBanco, sFoperacion, sHoperacion, sNumOpeDecla, sPresentacion, dModificacion, nRenEstatus, dOperacion, dPresentacion, nRenglonesAfectados));
            }
            return nRenglonesAfectados;
        }
        public void Finaliza()
        {
            if (conBD != null)
            {
                conBD.Finally();
                conBD = null;
            }
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
