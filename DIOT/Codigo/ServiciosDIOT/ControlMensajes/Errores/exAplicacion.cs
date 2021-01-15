﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlMensajes.Errores
{
    public class exAplicacion : ApplicationException
    {
        private ManejoLog mLog = null;
        private string sMensajeLog { get; set; }
        public exAplicacion()
        {
            sMensajeLog = "No se especifico el mensaje de error.";
        }

        public exAplicacion(string message) : base(message)
        {
            sMensajeLog = message;
        }

        public exAplicacion(string message, Exception inner) : base(message, inner)
        {
            sMensajeLog = string.Format("Mensaje Error: {0}  \r\nInner Excepcion: {1}", message, inner);
        }

        public void cEscribeLog(string sModulo, int nEvento)
        {
            mLog = new ManejoLog(sModulo, nEvento);
            mLog.MensajeWarning(sMensajeLog);
        }
    }
}
