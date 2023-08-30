using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtileriasComunes.ManejoErrores
{
    public class ErroresAplicacion : Exception
    {
        public ErroresAplicacion(TipoError tipoerror, int idError, string mensajeUsuario) : base(mensajeUsuario)
        {
            tipoError = tipoerror;
            IdError = idError;
            MensajeError = mensajeUsuario;
            MensajeLog = mensajeUsuario;
            MensajeUsuario = mensajeUsuario;
        }
        public ErroresAplicacion(TipoError tipoerror, int idError, string mensajeUsuario, Exception inner) : base(mensajeUsuario, inner)
        {
            tipoError = tipoerror;
            IdError = idError;
            MensajeUsuario = mensajeUsuario;

            switch (tipoerror)
            {
                case TipoError.ErrorAplicacion:
                    MensajeError = mensajeUsuario;
                    MensajeLog = mensajeUsuario;
                    break;
                case TipoError.ErrorSQL:
                    MensajeError = inner.Message;
                    MensajeLog = inner.ToString();
                    break;
                default:
                    MensajeError = inner.Message;
                    MensajeLog = inner.ToString();
                    break;
            }
        }

        public enum TipoError { ErrorGenerico = 0, ErrorSQL = 1, ErrorAplicacion = 3 }

        public TipoError tipoError { get; set; }
        public int IdError { get; set; }
        public string MensajeError { get; set; }
        public string MensajeLog { get; set; }
        public string MensajeUsuario { get; set; }
    }
}
