using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using EntApp = RecepcionEnt;
using BD = RecepcionBD;
using exC = ControlMensajes.Errores;
using TextEnum = RecepcionEnt.TextosEnumer;

namespace RecepcionRN.DyPDeclaraciones
{
    public static class ConsultaDeclaraciones
    {
        private static int NIntentos
        {
            get
            {
                int nResult = 0;

                if (!int.TryParse(ConfigurationManager.AppSettings["numIntentosPro"].ToString().Trim(), out nResult))
                    nResult = 0;

                return nResult;
            }
        }
        private static string SNombreEquipo
        {
            get
            {
                string sResult = ConfigurationManager.AppSettings["NombreEquipo"].ToString().Trim();
                if (sResult == "") sResult = Environment.MachineName;
                return sResult;
            }
        }
        private static string SRutaFileShare
        {
            get
            {
                string sResult = ConfigurationManager.AppSettings["FileShare"].ToString().Trim();
                return sResult;
            }
        }

        public static List<EntApp.DyPDeclaraciones.entDeclaracion> ConsultaProcesar()
        {
            List<EntApp.DyPDeclaraciones.entDeclaracion> result = null;
            BD.ManejoBD ConsDeclaraciones = null;
            try
            {
                ConsDeclaraciones = new BD.ManejoBD();
                result = ConsDeclaraciones.ConsultaDeclaraciones(SNombreEquipo, NIntentos);
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
                if (ConsDeclaraciones != null) ConsDeclaraciones.FinalizaComponentes();
            }
            return result;
        }

        public static void GuardaDeclaracion(int nOperacion, string sRutaArchivo, Byte[] btArchivo)
        {
            BD.ManejoBD ConsDeclaraciones = null;
            BD.DyPManejoDatosC.DatosConfiguracion datosConfiguracion = null;
            int nFileMax = 0;
            try
            {
                datosConfiguracion = new BD.DyPManejoDatosC.DatosConfiguracion(TextEnum.Enumeradores.Conexion.scadeNet);
                ConsDeclaraciones = new BD.ManejoBD();

                nFileMax = int.Parse(datosConfiguracion.ConsultaDatosConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.InformativaDeTerceros::MaxFileSizeInternet").sDescripcion.Trim());

                ConsDeclaraciones.ActualizaDeclaracionFile(nOperacion, btArchivo.Length, nFileMax, btArchivo);
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
                if (ConsDeclaraciones != null) ConsDeclaraciones.FinalizaComponentes();
            }
        }
    }
}
