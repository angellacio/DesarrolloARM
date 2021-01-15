using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace MonitorIncidentesServices
{
    class Negocio
    {
        public void Execute()
        {
            Datos incidentes = new Datos();
            DataTable dtl_inc = new DataTable();
            Correo C_inc = new Correo();
            string lista_inc = string.Empty;
            try
            {
                dtl_inc = incidentes.consulta();

                foreach (DataRow dr in dtl_inc.Rows)
                {
                    lista_inc = lista_inc + dr[0].ToString() + " - " + dr[1].ToString() + " - " + dr[2].ToString() + "<BR>";
                }

                if (lista_inc != string.Empty && lista_inc != "" && lista_inc != null)
                {
                    string plantilla = incidentes.Parametros("INCIDENTES_0");
                    string asunto = incidentes.Parametros("ASUNTO_INC");

                    plantilla = plantilla.Replace("@Incidentes", lista_inc);
                    C_inc.Enviar(plantilla, asunto);
                }
            }
            catch (Exception ex)
            {
                Errores(ex.ToString());
            }
            finally
            {
                dtl_inc.Dispose();
                lista_inc = string.Empty;
                incidentes.Dispose();
            }

        }
        public void Errores(string error)
        {
            Datos config = new Datos();
            string path = config.Parametros("RUTA_LOG");
            path = path.Replace("@Log", "Log_Error_ServicioIncidentes");
            File.WriteAllText(path, error, Encoding.UTF8);
        }
    }
}
