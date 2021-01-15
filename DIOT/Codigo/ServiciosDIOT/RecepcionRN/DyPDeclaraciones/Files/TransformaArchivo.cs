using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using cManLog = ControlMensajes;
using exC = ControlMensajes.Errores;


namespace RecepcionRN.DyPDeclaraciones.Files
{
    public static class TransformaArchivo
    {
        private static cManLog.ManejoLog log()
        {
            return new cManLog.ManejoLog(ConfigurationManager.AppSettings["RegLogSours"].ToString(), 1);
        }

        private static string sRutaFileShare
        {
            get
            {
                string sResult = "";

                sResult = ConfigurationManager.AppSettings["FileShare"].ToString();

                return sResult;
            }
        }

        public static byte[] ToArrayArchivo(string sArchivo)
        {
            byte[] arrayResult = null;
            Stream myStream = null;
            MemoryStream ms = null;
            try
            {
                if (!File.Exists(string.Format(@"{0}\{1}", sRutaFileShare, sArchivo))) throw new ApplicationException(string.Format("El Archivo '{0}' no se encuentra.", string.Format(@"{0}\{1}", sRutaFileShare, sArchivo)));

                myStream = File.Open(string.Format(@"{0}\{1}", sRutaFileShare, sArchivo), FileMode.Open);

                ms = new MemoryStream();
                myStream.CopyTo(ms);
                arrayResult = ms.ToArray();
            }
            catch (ApplicationException ex)
            {
                log().MensajeWarning(ex.Message);
            }
            catch (Exception ex)
            {
                log().MensajeError(ex.Message);
                throw new exC.exSalidaNext("", ex);
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                    ms = null;
                }
                if (myStream != null)
                {
                    myStream.Close();
                    myStream.Dispose();
                    myStream = null;
                }
            }
            return arrayResult;
        }
    }
}
