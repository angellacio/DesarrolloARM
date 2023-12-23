using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA_Entidades.ManejoProblemas
{
    public class EntProblematica
    {
        public EntProblematica() { }

        public int IdProblematica {  get; set; }
        public int IdTipoProblematica { get; set;}
        public string TipoProblematica { get; set; }
        public int IdEstado {  get; set; }
        public string Estado { get; set; }
        public int IdResponsable { get; set; }
        public string Responsable { get; set; }
        public int IdAplicativo { get; set; }
        public string Aplicativo { get; set; }
        public string NumProblematica { get; set; }
        public string NumRQS { get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public string RFC { get; set; }
        public string IdentificadorReq { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Observaciones { get; set; }
    }
}
