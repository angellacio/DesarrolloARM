using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using sIO = System.IO;
using System.Configuration;
using mesajeLog = ProDec_ReglaNegocio.ManejoErrores;
using manejoArchivo = ProDec_ReglaNegocio.Proceso.ProcesaDeclaracion;

namespace ProDec_Servicio
{
    partial class RecepcionValidacionDeclaraciones : ServiceBase
    {
        public RecepcionValidacionDeclaraciones()
        {
            InitializeComponent();
        }
        private System.ComponentModel.IContainer components = null;
        private System.Timers.Timer temporizador { get; set; }
        private int nInterEjecucion
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["IntervaloEjecucion"].ToString().Trim(), out result);
                return result * 60000;
            }
        }
        private int nMaxIntentos
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["MaxNumIntentos"].ToString().Trim(), out result);
                return result;
            }
        }
        
        private String nomEquipo
        {
            get
            {
                String nEquipo = "";
                nEquipo = Environment.MachineName;
                return nEquipo;
            }
        } //VARIABLE PARA OBTENER EL NOMBRE DEL EQUIPO

        private void temporizador_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            temporizador.Stop();

            IniciarProceso();

            temporizador.Start();
        }

        public void IniciarProceso()
        {
            mesajeLog.IniciaTermina(true, false);
            try
            {

                manejoArchivo.ReserveDeclas(nMaxIntentos, nomEquipo);
            }
            catch (Exception ex)
            {
                mesajeLog.MensajeError("Error al procesar la información.", ex);
            }
            finally { }
            mesajeLog.IniciaTermina(false, false);
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            try
            {
                mesajeLog.IniciaTermina(true, true);
                temporizador = new System.Timers.Timer { AutoReset = true, Interval = nInterEjecucion };
                temporizador.Elapsed += temporizador_Elapsed;
                temporizador.Start();
            }
            catch (Exception ex)
            {
                mesajeLog.MensajeError("No se pudo levantar el servicio", ex);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            temporizador.Stop();
            temporizador.Close();
            temporizador.Dispose();
            mesajeLog.IniciaTermina(false, true);
        }
    }
}
