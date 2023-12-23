using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ConsolaDePruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            //DateTime e1 = new DateTime(2013, 09, 1);
            //DateTime e2 = new DateTime(2013, 09, 30);
            //DateTime e3 = new DateTime(2013, 10, 1);
            //DateTime e4 = new DateTime(2013, 10, 31);
            //DateTime e5 = new DateTime(2013, 02, 27);
            //DateTime e6 = new DateTime(2013, 02, 28);
            //DateTime e7 = new DateTime(2013, 03, 1);
            //DateTime s1 = Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura.LineasCaptura.RecuperarVigenciaXContribucionSinPeriodo(e1);
            //DateTime s2 = Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura.LineasCaptura.RecuperarVigenciaXContribucionSinPeriodo(e2);
            //DateTime s3 = Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura.LineasCaptura.RecuperarVigenciaXContribucionSinPeriodo(e3);
            //DateTime s4 = Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura.LineasCaptura.RecuperarVigenciaXContribucionSinPeriodo(e4);
            //DateTime s5 = Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura.LineasCaptura.RecuperarVigenciaXContribucionSinPeriodo(e5);
            //DateTime s6 = Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura.LineasCaptura.RecuperarVigenciaXContribucionSinPeriodo(e6);
            //DateTime s7 = Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura.LineasCaptura.RecuperarVigenciaXContribucionSinPeriodo(e7);
            
            //List<string> ls= new List<string>();
            //ls.Add("ACM9602228S5");

            //Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios.ProxyManagerArca.ObtenerInformacionContribuyente("ACM9602228S5", ls);

            string xmlPath = @"c:\XMLErrorDeserializa2.xml";
            Console.WriteLine(String.Format("Cargando archivo:'{0}'",xmlPath));
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            var a = Sat.CreditosFiscales.Comunes.Herramientas.MetodosComunes.Deserializa<Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ServicioArcaCF.DocumentosDeterminantes[]>(doc.InnerXml);
            Console.WriteLine("Archivo deserializado correctamente");
            Console.ReadLine();


        }
    }
}
