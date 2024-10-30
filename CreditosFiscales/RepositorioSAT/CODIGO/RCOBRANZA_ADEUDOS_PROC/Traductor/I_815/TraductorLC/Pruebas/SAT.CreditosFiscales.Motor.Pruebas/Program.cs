using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.RecepcionLC;
using SAT.CreditosFiscales.Motor.Pruebas.RecepcionLC;

namespace SAT.CreditosFiscales.Motor.Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            //LogicalLte("10/07/2013", "08/08/2013");
            //SolicitudGeneracionLC respuesta = deserializa();
            EjecutaMotor();
            //InsertaReglas();
            //TraduccionParaDyP();          
            //EjecutaWCFDyP();
            //InsertaEsquemas();
        }

        private static void TraduccionParaDyP()
        {
            GeneracionEquivalenciaConceptos.GeneraMensajeLineaCaptura("xml");
        }

        public static void InsertaEsquemas()
        {
            string xsd = File.ReadAllText(@"D:\Projects\SAT-LC-CreditosFiscales\TraductorLC\Comunes\SAT.CreditosFiscales.Motor.Esquemas\MotorCreditosFiscales_In\CF_PagosParcialidadesInicial.xsd");

            SAT.CreditosFiscales.Motor.Negocio.Esquemas.Validacion comun = new SAT.CreditosFiscales.Motor.Negocio.Esquemas.Validacion();

            comun.InsertaEsquema("Esquema entrada para pagos parcialidad inicial", xsd, string.Empty);
        }

        public static void InsertaReglas()
        {
            SAT.CreditosFiscales.Motor.Negocio.AdmonReglas.Ejecucion motor = new SAT.CreditosFiscales.Motor.Negocio.AdmonReglas.Ejecucion();

            string xslt = File.ReadAllText(@"D:\Projects\SAT-LC-CreditosFiscales\TraductorLC\Comunes\SAT.CreditosFiscales.Motor.Esquemas\Transformaciones\AgrupaConceptos.xslt");

            motor.InsertaRegla(new SAT.CreditosFiscales.Motor.Entidades.AdmonRegla.Regla { Descripcion = "Agrupación de conceptos DyP y Transacciones - Pago una sola exhibición", Xslt = xslt, EsValidacion = false });
        }


        public static SolicitudGeneracionLC deserializa(string FilePath)
        { 
        
            SolicitudGeneracionLC oSolicitud = new SolicitudGeneracionLC();

            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                XmlSerializer serializer = new XmlSerializer(typeof(SolicitudGeneracionLC));
                MemoryStream stream =
                      new MemoryStream(encoding.GetBytes(File.ReadAllText(FilePath)));
                oSolicitud = (SolicitudGeneracionLC)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oSolicitud;
       
        }

        public static RespuestaLC deserializaResp(string text)
        {

            RespuestaLC oSolicitud = new RespuestaLC();

            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                XmlSerializer serializer = new XmlSerializer(typeof(RespuestaLC));
                MemoryStream stream =
                      new MemoryStream(encoding.GetBytes(text));
                oSolicitud = (RespuestaLC)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oSolicitud;

        }

        public static void EjecutaMotor()
        {            
            Traductor motor = new Traductor();
            MotorWCF.CreditosFiscalesClient oCliente = new MotorWCF.CreditosFiscalesClient();

            string xml = File.ReadAllText(@"D:\Projects\SAT-LC-CreditosFiscales\Pruebas\Files\XML\CF_Pago en una sola exhibicion.xml");
            //string xml = File.ReadAllText(@"C:\Pruebas\SolicitudLC_MovimientosContables_Agrupado.xml");
            

            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            //Respuesta<ResultadoReglas> resultadoValidacion = motor.EjecutaMotor(1, 30, xml);
            //string resultadoValidacion = oCliente.ObtieneLineaCaptura(3, 10, xml);
            string resultadoValidacion = oCliente.ObtieneLineaCapturaConPDF(2, 10, xml);
            RespuestaLC oDatosLinea = deserializaResp(resultadoValidacion);
            
            //stopWatch.Stop();

            //// Get the elapsed time as a TimeSpan value.
            //TimeSpan ts = stopWatch.Elapsed;

            //// Format and display the TimeSpan value.
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            Console.WriteLine(string.Format("Resultado: {0}", oDatosLinea.TipoRespuesta == 0 ? "No Exitoso" : "Exitoso"));

           //Linea de captura
            if (oDatosLinea.LineasCaptura != null)
            {
                if (oDatosLinea.PDF != null)
                {
                    if (oDatosLinea.PDF.Length > 0)
                    {
                        string nameText = string.Format(@"D:\Projects\SAT-LC-CreditosFiscales\TraductorLC\Documentacion\PDF\{0}.txt", oDatosLinea.LineasCaptura[0].Folio);
                        string namePDF = string.Format(@"D:\Projects\SAT-LC-CreditosFiscales\TraductorLC\Documentacion\PDF\{0}.pdf", oDatosLinea.LineasCaptura[0].Folio);
                        SaveText(Convert.ToBase64String(oDatosLinea.PDF), nameText);
                        Decode(nameText, namePDF);
                    }
                }

                foreach (RespuestaLCDatosLinea linea in oDatosLinea.LineasCaptura)
                {
                    Console.WriteLine(string.Format("Folio: {0}", linea.Folio));
                    Console.WriteLine(string.Format("LineaCaptura: {0}", linea.LineaCaptura));
                    Console.WriteLine("*****************");
                }
            }
           //Errores
            if (oDatosLinea.ListaErrores != null)
            {
                foreach (string error in oDatosLinea.ListaErrores)
                {
                    Console.WriteLine(string.Format("Error: {0}", error));                    
                    Console.WriteLine("*****************");
                }
            }

            Console.ReadLine();
        }

        public static void Decode(string inFileName, string outFileName)
        {
            System.Security.Cryptography.ICryptoTransform transform = new System.Security.Cryptography.FromBase64Transform();
            using (System.IO.FileStream inFile = System.IO.File.OpenRead(inFileName),
                                      outFile = System.IO.File.Create(outFileName))
            using (System.Security.Cryptography.CryptoStream cryptStream = new System.Security.Cryptography.CryptoStream(inFile, transform, System.Security.Cryptography.CryptoStreamMode.Read))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = cryptStream.Read(buffer, 0, buffer.Length)) > 0)
                    outFile.Write(buffer, 0, bytesRead);

                outFile.Flush();
            }
        }

        public static bool LogicalLte(string val1, string val2)
        {
            bool ret = false;     

            ret = DateTime.Parse(val1) <= DateTime.Parse(val2);            

            return ret;
        }

        public static void SaveText(string TextPDF, string name)
        {
            System.IO.File.WriteAllText(name, TextPDF);
        }

        public static void EjecutaWCFDyP()
        {
            try
            {
                RecepcionLC.RecepcionClient oCliente = new RecepcionLC.RecepcionClient();

                SolicitudGeneracionLC solicitud = new SolicitudGeneracionLC();
                solicitud = deserializa(@"D:\Projects\SAT-LC-CreditosFiscales\TraductorLC\Documentacion\SolicitudPagosUnaExhibicion_reducido.xml");

                var respuesta = oCliente.GenerarLineaCaptura(solicitud);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
