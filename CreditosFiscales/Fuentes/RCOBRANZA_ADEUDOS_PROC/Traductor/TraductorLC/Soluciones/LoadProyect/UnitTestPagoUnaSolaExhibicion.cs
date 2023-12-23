using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using LoadProyect.CreditosFiscales;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoadProyect
{
    [TestClass]
    public class UnitTestPagoUnaSolaExhibicion
    {
        [TestMethod]
        public void TestMethodPagoUnaSolaExhibicion()
        {
            using (CreditosFiscalesClient clienteMotor = new CreditosFiscalesClient())
            {
                string xml = File.ReadAllText(@"D:\Projects\SAT-LC-CreditosFiscales\Pruebas\Files\XML\PagoAutorización\PagoAutorización.xml");
                Stopwatch sw = new Stopwatch();
                using (OperationContextScope scope = new OperationContextScope(clienteMotor.InnerChannel))
                {
                    HttpRequestMessageProperty property = new HttpRequestMessageProperty();
                    property.Headers.Add("Authorization", "CRFIApp0001|CRFIApp0001");
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = property;                       
                    var respuesta = clienteMotor.ObtieneLineaCapturaConPDF(2, 22, xml);                    
                    Assert.IsNotNull(respuesta);
                }
            }
        }
    }
}
