using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;

namespace MonitorPaquetesWEB
{
    public class Desarrollador
    {
        public string Nombre { set; get; }
        public string Correo { set; get; }
        public string Teloficina { set; get; }
        public string Telmovil { set; get; }

        public Desarrollador(string paquete)
        {
            Detalle_desa(paquete);
        }
        private void Detalle_desa(string paquete)
        {
            Datos det_desarrollador = new Datos();
            DataTable resultado = new DataTable();
            resultado = det_desarrollador.Det_Desarrollador(paquete);

            foreach (DataRow dr in resultado.Rows)
            {
                this.Nombre = dr[0].ToString();
                this.Correo = dr[1].ToString();
                this.Teloficina = dr[2].ToString();
                this.Telmovil = dr[3].ToString();
            }
        }
    }
}