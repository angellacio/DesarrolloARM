using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using SAT.DyP.Util.Logging;

namespace IDEWebCargaDeclaracionesDWH
{
    public partial class CargaDeclaracionesDWH : ServiceBase
    {
        private static Timer temporizador = null;

        public CargaDeclaracionesDWH()
        {
            InitializeComponent();
            temporizador = new Timer(15000);
            temporizador.Elapsed += new ElapsedEventHandler(iniciarCargaDeclaraciones);

            if (!System.Diagnostics.EventLog.SourceExists(Constantes.Source))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    Constantes.Source, Constantes.Log);
            }
            eventLog1.Source = Constantes.Source;
            eventLog1.Log = Constantes.Log;

        }

        protected override void OnStart(string[] args)
        {
            if (temporizador != null)
            {
                temporizador.Start();
            }
        }

        protected override void OnStop()
        {
            if (temporizador != null)
            {
                temporizador.Stop();
                GC.KeepAlive(temporizador);
            }
        }

        private static void iniciarCargaDeclaraciones(object source, ElapsedEventArgs args)
        {
            temporizador.Enabled = false;
            try
            {
                DatosDeclaracion datosDeclaracion = new DatosDeclaracion();
                datosDeclaracion = ProcesamientoDWH.ObtenerDeclaracionAProcesar();

                if (datosDeclaracion != null)
                {
                    try
                    {
                        ProcesamientoDWH.declaracion = null;
                        ProcesamientoDWH.IniciarCargaDWH(datosDeclaracion);
                    }
                    catch (Exception ex)
                    {
                        
                        EventLogHelper.WriteEntry(Constantes.Source, Constantes.ErrorIDEWebCargaDeclaracionesDWH + ex.Message, System.Diagnostics.EventLogEntryType.Error, Constantes.Materia);
                        AccesoDatos.ActualizarControlCargaDWH((int)datosDeclaracion.Folio, Constantes.EstatusDWH.Fallido, ex.Message);
                       
                    }
                }                              
                               
            }
            catch (Exception ex)
            {
                EventLogHelper.WriteEntry(Constantes.Source, Constantes.ErrorIDEWebCargaDeclaracionesDWH + ex.Message, System.Diagnostics.EventLogEntryType.Error, Constantes.Materia);                
            }
            finally
            {
                temporizador.Enabled = true;
            }
        }
    }
}
