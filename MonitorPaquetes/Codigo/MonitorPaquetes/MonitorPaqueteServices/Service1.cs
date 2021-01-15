using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace MonitorPaqueteServices
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public void IniciaProceso()
        {
            Negocio Paquetes = new Negocio();
            Paquetes.Execute();
        }

        protected override void OnStart(string[] args)
        {
            var timer = new Timer { AutoReset = true, Interval = 60000 };
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IniciaProceso();
        }
    }
}
