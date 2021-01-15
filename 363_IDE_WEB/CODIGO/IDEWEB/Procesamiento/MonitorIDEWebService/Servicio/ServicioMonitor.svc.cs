using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using IdeMonitorService.Negocio.Datos;
using IdeMonitorService.Negocio.Contratos;
using System.Web.UI.WebControls;


namespace IdeMonitorService
{
   
    public class ServicioMonitor : IServicioMonitor
    {
        public List<DeclaracionSalida> TraerDeclaraciones(DeclaracionEntrada declaracion)
        {
            List<DeclaracionSalida> ren = new List<DeclaracionSalida>();
            IdeMonitorService.Negocio.Datos.AccesoDatos AD = new IdeMonitorService.Negocio.Datos.AccesoDatos();
            ren = AD.DatosDeclaraciones(declaracion);
            return ren;
        }

        public List<DeclaracionSalida> TraerBitacora(DeclaracionEntrada declaracion)
        {
            List<DeclaracionSalida> ren = new List<DeclaracionSalida>();
            IdeMonitorService.Negocio.Datos.AccesoDatos AD = new IdeMonitorService.Negocio.Datos.AccesoDatos();
    
            ren = AD.DatosBitacora(declaracion);
            return ren;
        }

        public List<CatalogoSalida> TraerCatalogos(int id)
        { 
            List<CatalogoSalida> LCS = new List<CatalogoSalida>();
            IdeMonitorService.Negocio.Datos.AccesoDatos AD = new IdeMonitorService.Negocio.Datos.AccesoDatos();
            String opc = "";
            switch (id)
            { 
                case 1:
                    opc = "CatTipoArchivo";
                    break;
                case 2:
                    opc = "CatMediosRecepcion";
                    break;
                case 3:
                    opc = "CatEstados";
                    break;
                case 4:
                    opc = "CatSector";
                    break;
                default:
                    opc = "";
                    break;
            }
            LCS = AD.DatosCatalogo(opc);
            return LCS;
        }

        public List<EstadisticaSalida> TraerEstadistica(EstadisticaEntrada estadistica)
        {
            List<EstadisticaSalida> LES = new List<EstadisticaSalida>();
            IdeMonitorService.Negocio.Datos.AccesoDatos AD = new IdeMonitorService.Negocio.Datos.AccesoDatos();
            LES = AD.DatosEstadistica(estadistica);
            return LES;
        }

        public String TraerAcuse(int Folio)
        {
            String acuse = "";
            IdeMonitorService.Negocio.Datos.AccesoDatos AD = new IdeMonitorService.Negocio.Datos.AccesoDatos();
            acuse = System.Web.HttpUtility.HtmlEncode( AD.generarAcuse(Folio));
            return acuse;
        }
    }
}
