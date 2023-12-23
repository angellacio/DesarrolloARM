
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.GeneracionEquivalenciaFormato:1:12/07/2012[Assembly:1.0:12/07/2013])


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
    /// <summary>
    /// Clase para manejar equivalencias de formato de impresiones
    /// </summary>
    public class GeneracionEquivalenciaFormato
    {
        /// <summary>
        /// Obtiene el formato de impresión para el id de aplicacion y tipo de documento asignado
        /// </summary>
        /// <param name="IdAplicacion">Id de la aplicacion</param>
        /// <param name="IdTipoDocumento">Id del tipo documento</param>
        /// <returns>Id del formato de impresión</returns>
        public static int GeneraEquivalenciaFormato(int IdAplicacion, int IdTipoDocumento, string documento)
        {
            int FormatoImpresion = 0;

            var ReglasFormatoImpresion = DalFormatoEquivalencia.ObtieneEquivalenciaFormatoImpresion(IdTipoDocumento, IdAplicacion);


            var objetoFiltro = XDocument.Parse(documento);
            if (ReglasFormatoImpresion.Count > 1)
            {
                foreach (var regla in ReglasFormatoImpresion)
                {
                    if (regla.XPathEvaluacion != string.Empty)
                    {
                        var nodoObjeto = objetoFiltro.XPathSelectElement(regla.XPathEvaluacion);
                        if (nodoObjeto != null && (nodoObjeto.Value == regla.ValorEvaluacion || string.IsNullOrEmpty(regla.ValorEvaluacion)))
                        {
                            FormatoImpresion = regla.IdFormatoImpresion;
                            break;
                        }
                    }
                    else
                        FormatoImpresion = regla.IdFormatoImpresion;
                }
            }
            else
            {
                var regla = ReglasFormatoImpresion.FirstOrDefault();
                if (regla != null)
                {
                    if (regla.XPathEvaluacion != string.Empty)
                    {
                        var nodoObjeto = objetoFiltro.XPathSelectElement(regla.XPathEvaluacion);
                        if (nodoObjeto != null && (nodoObjeto.Value == regla.ValorEvaluacion || string.IsNullOrEmpty(regla.ValorEvaluacion)))
                        {
                            FormatoImpresion = regla.IdFormatoImpresion;
                        }
                    }
                    else
                    {
                        FormatoImpresion = regla.IdFormatoImpresion;
                    }
                }
            }

            return FormatoImpresion;
        }
    }
}
