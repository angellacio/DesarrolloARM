using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using DatosProcesamiento = SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;





namespace SAT.CreditosFiscales.Motor.ReglasNet
{
    public class QuitarDescuentos : Negocio.ReglasNet.ReglaBaseNet
    {
        public override RespuestaGenerica Ejecuta(XmlDocument documento)
        {
            var respuesta = new RespuestaGenerica();
            
                var oCreditosFiscales = new DatosProcesamiento.CreditosFiscales();
                var creditosFiscales =
                    MetodosComunes.DeserializaObjectoPorXml(documento.InnerXml);

                //Conceptos
                foreach (DatosProcesamiento.CreditosFiscalesAgrupador agrupador in creditosFiscales.Agrupadores)
                {
                    foreach (
                        DatosProcesamiento.CreditosFiscalesAgrupadorConceptos concepto in agrupador.ConceptosOriginal)
                    {
                        if (concepto.Descuentos.Count() > 0)
                        {
                            foreach (
                                DatosProcesamiento.CreditosFiscalesAgrupadorConceptosDescuento descuento in
                                    concepto.Descuentos)
                            {
                                concepto.ImporteHistorico -= descuento.ImporteDescuento;
                            }

                            concepto.Descuentos = null;
                        }
                    }
                }

                return respuesta;            
        }
    }
}
