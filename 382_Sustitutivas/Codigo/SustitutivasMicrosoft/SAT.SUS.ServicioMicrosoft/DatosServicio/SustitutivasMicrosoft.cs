using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Timers;
using mLog = ControlaComponentes.ManejoLog;
using mExc = ControlaComponentes.ManejoExcepciones;
using ent = SAT.SUS.Entidades;
using rN = SAT.SUS.Negocio;

namespace SAT.SUS.ServicioMicrosoft.DatosServicio
{
    partial class SustitutivasMicrosoft : ServiceBase
    {
        public double nTiempoEjecucion
        {
            get
            {
                int nMinutos = 0;
                double dbResult = 0;

                if (!int.TryParse(ConfigurationManager.AppSettings["ServicioTiempoEjecucion"].ToString(), out nMinutos)) nMinutos = 1;
                dbResult = (60 * 1000) * nMinutos;

                return dbResult;
            }
        }
        public Timer tmSustitutivasMicrosoft { get; set; }

        public SustitutivasMicrosoft()
        {
            InitializeComponent();
            tmSustitutivasMicrosoft = new Timer { AutoReset = true, Interval = nTiempoEjecucion };
        }

        protected override void OnStart(string[] args)
        {
            mLog.ActualizaLog.mensajeStarFin(mLog.ActualizaLog.AccionProceso.InicioServicio);
            tmSustitutivasMicrosoft.Elapsed += tmSustitutivasMicrosoft_Elapsed;
            tmSustitutivasMicrosoft.Start();
        }

        protected override void OnStop()
        {
            tmSustitutivasMicrosoft.Stop();
            tmSustitutivasMicrosoft.Close();
            tmSustitutivasMicrosoft.Dispose();
            mLog.ActualizaLog.mensajeStarFin(mLog.ActualizaLog.AccionProceso.FinalServicio);
        }

        private void tmSustitutivasMicrosoft_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmSustitutivasMicrosoft.Stop();

            IniciaServicio();

            tmSustitutivasMicrosoft.Start();
        }

        public void IniciaServicio()
        {
            List<ent.entDatosConsultar> lstProcesar = null;
            rN.RealizaOperacionesIntegracion rnManejoInformacion = null;
            DateTime dModificacion = DateTime.Now;
            try
            {
                rnManejoInformacion = new rN.RealizaOperacionesIntegracion();
                mLog.ActualizaLog.mensajeStarFin(mLog.ActualizaLog.AccionProceso.InicioProceso);

                lstProcesar = rnManejoInformacion.Informacion_ReservaObten();
                rnManejoInformacion.Finaliza();

                mLog.ActualizaLog.mensajeAuditoria(string.Format("Se van a procesar {0} regitros.", lstProcesar.Count));

                lstProcesar.ForEach(ItemPro =>
                {
                    mLog.ActualizaLog.mensajeAuditoria(string.Format("Procesar Microsoft {0}.", ItemPro.ToString()));
                    ItemPro.entRespuesta = ConsultaServicioMicrosoft(ItemPro);
                });


                lstProcesar.ForEach(ItemPro =>
                {
                    mLog.ActualizaLog.mensajeAuditoria(string.Format("Actualiza Base de Datos {0}.", ItemPro.ToString()));
                    ItemPro.entRespuesta.lstPagos.ForEach(itemPag =>
                    {
                        rnManejoInformacion.ActualizaRegistros(ItemPro.nId, ItemPro.entRespuesta.lstPagos.Count, -1, itemPag.sPagado,
                            itemPag.sLineaCaptura, itemPag.sTipoOperacion, itemPag.sMensaje, itemPag.sNumOperBanco, itemPag.sFoperacion,
                            itemPag.sHoperacion, itemPag.sNumOpeDecla, itemPag.sFPresentacion, dModificacion, itemPag.nEstatus,
                            itemPag.dOperacion, itemPag.dPresentacion);
                    });
                    rnManejoInformacion.ActualizaRegistros(ItemPro.nId, ItemPro.entRespuesta.lstPagos.Count, ItemPro.entRespuesta.nEstatus, "",
                            "", "", "", "", "", "", "", "", dModificacion, 0, null, null);
                });

            }
            catch (mExc.ErrorAplicacion) { }
            catch (mExc.ErrorGeneral) { }
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
                if (rnManejoInformacion != null) rnManejoInformacion.Finaliza();
                mLog.ActualizaLog.mensajeStarFin(mLog.ActualizaLog.AccionProceso.FinalProceso);
            }
        }

        private ent.entDatosRespuesta ConsultaServicioMicrosoft(ent.entDatosConsultar itemProcesar)
        {
            ent.entDatosRespuesta entRespuestaWS = null;
            consPagosMicrosoft.ConsultaServicioClient consInfMicrosoft = null;
            consPagosMicrosoft.ConsultaAnteriorFiltroRequest infRequest = null;
            consPagosMicrosoft.ListaOperaciones infResponse = null;
            try
            {
                entRespuestaWS = new ent.entDatosRespuesta();

                infRequest = new consPagosMicrosoft.ConsultaAnteriorFiltroRequest()
                {
                    Auditable = rN.RealizaOperacionesIntegracion.pu_bolAuditoria,
                    UsuarioOperacion = rN.RealizaOperacionesIntegracion.pu_sUsuarioAplicativo,
                    ServidorOrigen = rN.RealizaOperacionesIntegracion.pu_sIP,
                    Item = new consPagosMicrosoft.ConsultaAnteriorPagadaFiltro()
                    {
                        RFC = string.Format("               {0}", itemProcesar.RFC.Trim()).Substring(string.Format("               {0}", itemProcesar.RFC.Trim()).Length - 13, 13),
                        Ejercicio = itemProcesar.Ejercicio.ToString(),
                        Periodo = itemProcesar.Periodo,
                        FechaRecepcion = string.Format("{0:yyyyMMdd}", itemProcesar.FechaRecepcion),
                        Banco = itemProcesar.Banco,
                        Concepto = itemProcesar.Concepto
                    }
                };
                consInfMicrosoft = new consPagosMicrosoft.ConsultaServicioClient();

                infResponse = consInfMicrosoft.ConsultarPagoAnterior(infRequest);

                entRespuestaWS.nEstatus = 2;
                entRespuestaWS.sMensaje = "";
                foreach (consPagosMicrosoft.OperacionPagoAnterior pA in infResponse.Operaciones)
                {
                    entRespuestaWS.lstPagos.Add(new ent.entDatosPago()
                    {
                        sPagado = pA.Pagado.Trim(),
                        sLineaCaptura = pA.LineaCaptura.Trim(),
                        sTipoOperacion = pA.TipoOperacion.Trim(),
                        sMensaje = pA.Mensaje.Trim(),
                        sNumOperBanco = pA.NumOpeBanco.Trim(),
                        sFoperacion = pA.FechaOperacion.Trim(),
                        sHoperacion = pA.HoraOperacion.Trim(),
                        sNumOpeDecla = pA.NumOpeDecla.Trim(),
                        sFPresentacion = pA.FechaPresentacion.Trim()
                    });
                }
            }
            catch (mExc.ErrorAplicacion) { }
            catch (mExc.ErrorGeneral) { }
            catch (ApplicationException ex)
            {
                entRespuestaWS.nEstatus = 3;
                entRespuestaWS.sMensaje = ex.Message;
                mLog.ActualizaLog.mensajeAlerta(ex.Message);
            }
            catch (Exception ex)
            {
                entRespuestaWS.nEstatus = 3;
                entRespuestaWS.sMensaje = ex.Message;
                mLog.ActualizaLog.mensajeError(ex.ToString().Trim());
            }
            finally
            {
                mLog.ActualizaLog.mensajeAuditoria(string.Format(" -------------------------------------- Resultado Microsoft. {0}", entRespuestaWS.ToString()));
            }
            return entRespuestaWS;
        }
        
    }
}
