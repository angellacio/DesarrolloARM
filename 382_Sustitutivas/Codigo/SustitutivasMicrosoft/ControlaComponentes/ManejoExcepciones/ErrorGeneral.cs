using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlaComponentes.ManejoExcepciones
{
    public class ErrorGeneral : Exception
    {
        private string sMensajeLog { get; set; }
        public ErrorGeneral()
        {
            sMensajeLog = "No se especifico el mensaje de error.";
        }

        public ErrorGeneral(string message) : base(message)
        {
            sMensajeLog = message;
        }

        public ErrorGeneral(string message, Exception inner) : base(message, inner)
        {
            sMensajeLog = string.Format("Mensaje Error: {0}  \r\nInner Excepcion: {1}", message, inner);
        }

        public void cEscribeLog(string sModulo, int nEvento)
        {
            ManejoLog.ActualizaLog.mensajeError(sMensajeLog);
        }

    }
}
