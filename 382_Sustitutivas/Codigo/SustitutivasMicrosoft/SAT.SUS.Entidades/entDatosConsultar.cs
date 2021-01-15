using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SAT.SUS.Entidades
{
    [Serializable]
    public class entDatosConsultar
    {
        public entDatosConsultar(DataRow itemDR)
        {
            nId = Convert.ToInt32(itemDR["nId"]); ;
            RFC = itemDR["rfc"].ToString().Trim();
            FechaRecepcion = Convert.ToDateTime(itemDR["fecharecepcion"]);
            Ejercicio = Convert.ToInt32(itemDR["ejercicio"]);
            Periodo = itemDR["periodo"].ToString().Trim();
            Banco = itemDR["banco"].ToString().Trim();
            Concepto = itemDR["concepto"].ToString().Trim();
            fModificacion = Convert.ToDateTime(itemDR["fmodificacion"]);
            nEstatus = Convert.ToInt32(itemDR["nestatus"]);
            sEquipo = itemDR["sequipo"].ToString().Trim();
            nIntentos = Convert.ToInt32(itemDR["nintentos"]);
            entRespuesta = new entDatosRespuesta();
        }
        public override string ToString()
        {
            return string.Format("nId: {0} ;; RFC: {1} ;; Ejercicio: {2} ;; Periodo: {3} ;; FechaRecepcion: {4} ;; Banco: {5} ;; Concepto: {6} ;; fModificacion: {7} ;; nEstatus: {8} ;; sEquipo: {9} ;; nIntentos: {10} ;; ",
                nId, RFC, Ejercicio, Periodo, FechaRecepcion, Banco, Concepto, fModificacion, nEstatus, sEquipo, nIntentos);
        }

        public int nId { get; set; }
        public String RFC { get; set; } // RFC que generó la operación. Fijo de 13 caracteres, para PM se rellena con espacio en blanco al principio
        public int Ejercicio { get; set; } // Ejercicio de la operación. Numérico de cuatro posiciones
        public String Periodo { get; set; } // Clave del periodo de la operación. Tres caracteres de longitud
        public DateTime FechaRecepcion { get; set; } // Fecha de la recepción de la operación en formato AAAAMMDD
        public String Banco { get; set; } // Código del banco que recibió el pago. Cinco carateres de longitud
        public String Concepto { get; set; } // Código del concepto de pago. Máximo de seis carateres de longitud
        public DateTime fModificacion { get; set; }
        public int nEstatus { get; set; }
        public String sEquipo { get; set; }
        public int nIntentos { get; set; }

        public entDatosRespuesta entRespuesta { get; set; }
    }
}
