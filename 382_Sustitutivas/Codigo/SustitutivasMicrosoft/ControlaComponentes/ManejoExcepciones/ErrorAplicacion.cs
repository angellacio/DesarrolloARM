using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlaComponentes.ManejoExcepciones
{
    public class ErrorAplicacion : ApplicationException
    {
        private string sMensajeLog { get; set; }
        public ErrorAplicacion()
        {
            sMensajeLog = "No se especifico el mensaje de alerta.";
        }

        public ErrorAplicacion(string message) : base(message)
        {
            sMensajeLog = message;
        }

        public ErrorAplicacion(string message, Exception inner) : base(message, inner)
        {
            sMensajeLog = string.Format("Mensaje alerta: {0}  \r\nInner Alerta: {1}", message, inner);
        }

        public void cEscribeLog(string sModulo, int nEvento)
        {
            ManejoLog.ActualizaLog.mensajeAlerta(sMensajeLog);
        }
    }
}
