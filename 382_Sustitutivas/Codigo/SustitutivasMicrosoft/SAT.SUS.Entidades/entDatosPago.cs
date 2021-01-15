using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.SUS.Entidades
{
    [Serializable]
    public class entDatosPago
    {
        public String sPagado { get; set; } // Indica si la operación está o no pagada, si la operación es un cero, se considera como pagada y se devolverá verdadero (1), si no está pagada o si se genera algun error, el resultado será falso (0)
        public String sLineaCaptura { get; set; } // Cuando la operación inmediata anterior es un pago, se devuelve la línea de captura, vacío si hay un error, es un cero o no ha sido pagado. 20 posiciones de longitud
        public String sTipoOperacion { get; set; } // 1: Cero, 2:Pago, 3:Error
        public String sMensaje { get; set; } // Solo se llena este mensaje si se genera algún error. Maximo 256 caracteres de longitud
        public String sNumOperBanco { get; set; } // Número de operación del Banco, vacío si hay un error, es un cero o no ha sido pagado. Máximo 14 caracteres alfanuméricos
        public String sFoperacion { get; set; } // Fecha de pago en el banco, vacío si hay un error, es un cero o no ha sido pagado. Diez caracteres de longitud en formato AAAA-MM-DD
        public String sHoperacion { get; set; } // Hora de pago recibido por el banco, vacío si hay un error, es un cero o no ha sido pagado. Cinco caracteres de lóngitud en formato HH:MM
        public String sNumOpeDecla { get; set; } // Número de operación de la declaración para DyP o el idsolicituddocumento para SIT, vacío si hay un error, es un cero o no ha sido pagado. Entero de máximo 14 posiciones
        public String sFPresentacion { get; set; } // Fecha de presentación de la declaración, vacío si hay un error, es un cero o no ha sido pagado. Diez caracteres de longitud en formato AAAA-MM-DD
        public int nEstatus
        {
            get
            {
                int nResult = 0;

                if (sTipoOperacion != "") int.TryParse(sTipoOperacion, out nResult);

                return nResult;
            }
        }
        public Nullable<DateTime> dOperacion
        {
            get
            {
                Nullable<DateTime> dResult = null;

                if (sFoperacion != "" && sHoperacion != "")
                {
                    int nAnio = 0, nMes = 0, nDia = 0, nHora = 0, nMinuto = 0, nSegundo = 0;

                    int.TryParse(sFoperacion.Substring(0, 4).Trim(), out nAnio);
                    int.TryParse(sFoperacion.Substring(4, 2).Trim(), out nMes);
                    int.TryParse(sFoperacion.Substring(6, 2).Trim(), out nDia);

                    int.TryParse(sHoperacion.Substring(0, 2).Trim(), out nHora);
                    int.TryParse(sHoperacion.Substring(3, 2).Trim(), out nMinuto);
                    nSegundo = 0;

                    dResult = new DateTime(nAnio, nMes, nDia, nHora, nMinuto, nSegundo);
                }

                return dResult;
            }
        }
        public Nullable<DateTime> dPresentacion
        {
            get
            {
                Nullable<DateTime> dResult = null;

                if (sFPresentacion != "")
                {
                    int nAnio = 0, nMes = 0, nDia = 0, nHora = 0, nMinuto = 0, nSegundo = 0;

                    int.TryParse(sFPresentacion.Substring(0, 4).Trim(), out nAnio);
                    int.TryParse(sFPresentacion.Substring(4, 2).Trim(), out nMes);
                    int.TryParse(sFPresentacion.Substring(6, 2).Trim(), out nDia);

                    dResult = new DateTime(nAnio, nMes, nDia, nHora, nMinuto, nSegundo);
                }

                return dResult;
            }
        }
    }
}
