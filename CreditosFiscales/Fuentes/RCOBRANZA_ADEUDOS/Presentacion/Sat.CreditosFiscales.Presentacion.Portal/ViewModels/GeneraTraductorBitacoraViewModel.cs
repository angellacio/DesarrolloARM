
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.GeneraTraductorBitacoraViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class GeneraTraductorBitacoraViewModel
    {
        public List<TraductorBitacora> listaBitacora { set; get; }

        public string IdProcesamiento { set; get; }
        public int IdAplicacion { set; get; }
        public int IdTipoDocumento { set; get; }
        public int IdPaso { set; get; }
        public int conError { set; get; }
        public string FechaInicio { set; get; }
        //public string FechaFin { set; get; }
        public int Horario { set; get; }
        public int Minutos { set; get; }
        public string Rfc { set; get; }

        public Dictionary<int, string> listaAplicacion { set; get; }
        public Dictionary<int, string> listaTipoDocumento { set; get; }
        public Dictionary<int, string> listaPasos { set; get; }
        public Dictionary<int, string> listaFiltrarPor { get { return new Dictionary<int, string>() { { -1, "Ver Todos" }, { 0, "Sin Error" }, { 1, "Con Error" }}; } }
        public Dictionary<int, string> listaHorario 
        { 
            get { return new Dictionary<int, string>() {
                { 0, "0 hrs." }, 
                { 1, "1 hrs." },
                { 2, "2 hrs." },
                { 3, "3 hrs." },
                { 4, "4 hrs." },
                { 5, "5 hrs." },
                { 6, "6 hrs." },
                { 7, "7 hrs." },
                { 8, "8 hrs." },
                { 9, "9 hrs." },
                { 10, "10 hrs." },
                { 11, "11 hrs." },
                { 12, "12 hrs." },
                { 13, "13 hrs." },
                { 14, "14 hrs." },
                { 15, "15 hrs." },
                { 16, "16 hrs." },
                { 17, "17 hrs." },
                { 18, "18 hrs." },
                { 19, "19 hrs." },
                { 20, "20 hrs." },
                { 21, "21 hrs." },
                { 22, "22 hrs." },
                { 23, "23 hrs." },
                { 24, "24 hrs." }
            }; 
            } 
        }
        public Dictionary<int, string> listaMinutos
        {
            get
            {
                return new Dictionary<int, string>() {
                { -1, "Toda la hora" }, 
                { 0, "0 a 15 minm" }, 
                { 15, "15 a 30 min." },
                { 30, "30 a 45 min." },
                { 45, "45 a 60 min." }
            };
            }
        }

    }
}

