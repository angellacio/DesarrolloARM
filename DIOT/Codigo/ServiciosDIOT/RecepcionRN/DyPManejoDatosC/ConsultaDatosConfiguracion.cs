using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using exC = ControlMensajes.Errores;
using entDyP = RecepcionEnt.DyPManejoDatosC;
using BD = RecepcionBD.DyPManejoDatosC;
using TextEnum = RecepcionEnt.TextosEnumer;

namespace RecepcionRN.DyPManejoDatosC
{
    public class ConsultaDatosConfiguracion
    {

        private string sSours { get { return "SerRecepcion"; } }
        private int nSours { get { return 1; } }

        private List<entDyP.entCatalogoServicios> ConsultaServiciosAll(int nIdServicio)
        {
            List<entDyP.entCatalogoServicios> result = null;
            BD.DatosConfiguracion ConsultaServicios = null;
            try
            {
                ConsultaServicios = new BD.DatosConfiguracion(TextEnum.Enumeradores.Conexion.Servicio);

                result = ConsultaServicios.ConsultaServicios(nIdServicio);
            }
            catch (exC.exSalidaNext ex)
            {
                throw ex;
            }
            catch (exC.exSalidaAll ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw new exC.exSalidaNext("", ex);
            }
            catch (Exception ex)
            {
                throw new exC.exSalidaAll("", ex);
            }
            finally
            {
                if (ConsultaServicios != null) ConsultaServicios.FinalizaComponentes();
            }
            return result;
        }

        public entDyP.entCatalogoServicios ConsultaServicio(int nIdServicio)
        {
            entDyP.entCatalogoServicios result = null;
            try
            {

                result = ConsultaServiciosAll(nIdServicio)[0];
            }
            catch (exC.exSalidaNext ex)
            {
                throw ex;
            }
            catch (exC.exSalidaAll ex)
            {
                throw ex;
            }
            finally { }
            return result;
        }
    }
}
