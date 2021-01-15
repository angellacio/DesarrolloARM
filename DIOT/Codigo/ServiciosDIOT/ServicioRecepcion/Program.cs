using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using cManLog = ControlMensajes;
using ex = ControlMensajes.Errores;
using ServRN = RecepcionRN.DyPManejoDatosC;
using ServEN = RecepcionEnt.DyPManejoDatosC;
using AppRN = RecepcionRN.DyPDeclaraciones;
using AppEN = RecepcionEnt.DyPDeclaraciones;

namespace ServicioRecepcion
{
    class Program
    {
        private static cManLog.ManejoLog log { get; set; }
        private static string sSours { get { return ConfigurationManager.AppSettings["RegLogSours"].ToString(); } }
        private static int nSours { get { return 0; } }

        private static void MuestraMensaje(string sMensaje)
        {
            MuestraMensaje(sMensaje, false);
        }
        private static void MuestraMensaje(string sMensaje, Boolean bAudit)
        {
            Console.WriteLine(sMensaje);
            if (bAudit) log.MensajeAuditoria(sMensaje);
            else log.Mensaje(sMensaje);
        }

        private static ServEN.entCatalogoServicios ConsultaEstadoServicio(int nServico)
        {
            ServEN.entCatalogoServicios result = null;
            ServRN.ConsultaDatosConfiguracion consDatos = null;
            try
            {
                consDatos = new ServRN.ConsultaDatosConfiguracion();
                result = consDatos.ConsultaServicio(nServico);

                if (result == null) result = new ServEN.entCatalogoServicios(nServico);
            }
            catch (Exception ex)
            {
                log.MensajeError(ex.ToString());
            }
            finally
            {

            }
            return result;
        }

        private static void TimerInicializa(int nMinutos)
        {
            Boolean bolEjecutaS = false;
            DateTime dtNow;
            int nCount = 0;
            try
            {
                bolEjecutaS = ConsultaEstadoServicio(1).bolEstado;

                while (bolEjecutaS)
                {
                    dtNow = DateTime.Now;

                    MuestraMensaje(string.Format(" ** Inicio Proceso - Recepción {0} ** ", nCount), true);
                    nCount++;

                    IniciaProceso(dtNow, nCount);

                    MuestraMensaje(string.Format(" ** Termino Proceso - Recepción {0} ** ", (nCount - 1)));

                    bolEjecutaS = ConsultaEstadoServicio(1).bolEstado;
                    if (bolEjecutaS) Thread.Sleep(nMinutos * (60 * 1000));
                }
            }
            catch (Exception ex)
            {
                log.MensajeError(ex.ToString());
            }
            finally
            {

            }
        }

        private static void IniciaProceso(DateTime dtNow, int nRepit)
        {
            List<AppEN.entDeclaracion> lstDeclaraciones = null;
            try
            {
                lstDeclaraciones = AppRN.ConsultaDeclaraciones.ConsultaProcesar();

                MuestraMensaje(string.Format("Proceso {0:HH:mm:ss.fff}. Repeticion {1}. RenglonesE {2}.", dtNow, nRepit, lstDeclaraciones.Count), true);

                lstDeclaraciones.ForEach(item =>
                {
                    MuestraMensaje(string.Format("Se inicia a procesar el archivo {0} :: {1}.", item.nIdProceso, item.sNombreArchivo), true);
                    byte[] byFile = AppRN.Files.TransformaArchivo.ToArrayArchivo(item.sNombreArchivo);

                    MuestraMensaje(string.Format("Archivo {0} :: {1} enviado a BD.", item.nIdProceso, item.sNombreArchivo), true);
                    if (byFile != null) AppRN.ConsultaDeclaraciones.GuardaDeclaracion(item.nIdProceso, item.sNombreArchivo, byFile);
                    
                });
            }
            catch (Exception ex)
            {
                log.MensajeError(ex.ToString());
            }
            finally { }

        }

        static void Main(string[] args)
        {
            try
            {
                log = new cManLog.ManejoLog(sSours, nSours);
                TimerInicializa(1);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                log.MensajeError(ex.ToString());
            }
            finally
            { }
        }

    }
}
