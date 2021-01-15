using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.IO;

namespace MonitorPaquetesWEB
{
    public class Negocio
    {
        public DataTable Monitoreo_Paquetes()
        {
            Datos grid_consulta = new Datos();
            return grid_consulta.consulta();
        }

        public DataTable Avisos()
        {
            Datos grid_notificados = new Datos();
            return grid_notificados.Consulta_Notificados();
        }

        public DataTable IncidentesSinPaquete()
        {
            Datos grid_incidentessin = new Datos();
            return grid_incidentessin.consulta_incidentes();
        }

        public DataTable IncidentesConPaquete()
        {
            Datos grid_incidentescon = new Datos();
            return grid_incidentescon.consulta_incidentesPaquete();
        }


        public Paquete Detalle(string paquete)
        {
            Paquete p_detalle = new Paquete(paquete);
            return p_detalle;
        }
    }
}