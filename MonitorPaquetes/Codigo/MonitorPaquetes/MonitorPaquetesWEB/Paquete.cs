using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;

namespace MonitorPaquetesWEB
{
    public class Paquete
    {
        public string Rdl { set; get; }
        public string Nombre { set; get; }
        public string Desarrollador { set; get; }
        public string Estado { set; get; }
        public string IncidentesRela { set; get; }
        public string PaquetesRela { set; get; }
        public string FechaRegistro { set; get; }
        public string Rechazo { set; get; }

        public Paquete(string _nombre)
        {
            Datos_Paquete(_nombre);
        }

        private void Datos_Paquete(string _nombre)
        {
            Datos Detalle = new Datos();
            DataTable resultado = new DataTable();
            string temp_inc = string.Empty;
            string temp_paq = string.Empty;
            string temp_rech = string.Empty;
            
            /////////////////
            resultado = Detalle.Det_Paquete(_nombre);
            foreach(DataRow dr in resultado.Rows)
            {
                
                this.Rdl = dr[0].ToString();
                this.Nombre = dr[1].ToString();
                this.Estado = dr[2].ToString();
                this.Desarrollador = dr[3].ToString();
                this.FechaRegistro = dr[4].ToString();
            }

            /////////////////
            resultado = Detalle.Det_Paquete_INC(_nombre);
            foreach (DataRow dr in resultado.Rows)
            {
                temp_inc = temp_inc + dr[0].ToString() + Environment.NewLine;
            }
            this.IncidentesRela = temp_inc;

            /////////////////
            resultado = Detalle.Det_Paquete_RPaq(_nombre);
            foreach (DataRow dr in resultado.Rows)
            {
                temp_paq = temp_paq + dr[0].ToString() + Environment.NewLine;
            }

            this.PaquetesRela = temp_paq;

            /////////////////
            resultado = Detalle.detalle_monitor(_nombre);
            foreach (DataRow dr in resultado.Rows)
            {
                temp_rech = temp_rech + dr[0].ToString() + Environment.NewLine;
            }

            this.Rechazo = temp_rech;
        }
    }
}