// Referencias de sistema.
using System.IO;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Referencias personalizadas.
using LoadProyect.CreditosFiscales;

namespace LoadProyect
{
	/// <summary>
	/// 
	/// </summary>
	[TestClass]
	public class UnitTestPagoUnaSolaExhibicion
	{
		/// <summary>
		/// 
		/// </summary>
		[TestMethod]
		public void TestMethodPagoUnaSolaExhibicion()
		{
			using (CreditosFiscalesClient clienteMotor = new CreditosFiscalesClient())
			{
				string xml = File.ReadAllText(@"D:\Projects\SAT-LC-CreditosFiscales\Pruebas\Files\XML\PagoAutorización\PagoAutorización.xml");
				var sw = new Stopwatch();
				using (OperationContextScope scope = new OperationContextScope(clienteMotor.InnerChannel))
				{
					var property = new HttpRequestMessageProperty();
					property.Headers.Add("Authorization", "CRFIApp0001|CRFIApp0001");
					OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = property;
					string respuesta = clienteMotor.ObtieneLineaCapturaConPDF(2, 22, xml);
					Assert.IsNotNull(respuesta);
				}
			}
		}
	}
}