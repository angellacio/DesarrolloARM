using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.Enumeradores;
using SAT.CreditosFiscales.Motor.Negocio.DatosCache;




namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
    public class HerramientasReglasEquivalencia
    {
        /// <summary>
        /// Método que obtiene las reglas equivalencia padre.
        /// </summary>
        /// <param name="idAplicacion">Aplicación para las que son válidas las reglas a consultar.</param>
        /// <param name="tipoObjeto">Tipo de objeto de la regla.</param>
        /// <returns>Lista de reglas por aplicación.</returns>
        public static List<CatReglasEquivalencia> ObtenerReglasEquivalencia(int idAplicacion, EnumTipoObjeto tipoObjeto)
        {
            var lstTemp = CacheRepository.ReglasEquivalencia.Where(
                                                r => r.IdAplicacion == idAplicacion && r.IdTipoObjecto == (short)tipoObjeto && r.IdPadre == null).OrderBy(r=>r.Secuencia).ToList();
            foreach (var reglaPadre in lstTemp)
            {
                reglaPadre.ReglasHijas.AddRange(CacheRepository.ReglasEquivalencia.Where(x => x.IdAplicacion == idAplicacion && x.IdTipoObjecto == (short)tipoObjeto && x.IdPadre == reglaPadre.IdreglaEquivalencia));
            }

            return lstTemp;
        }


        public static List<CatReglasEquivalencia> ObtenerReglasEquivalencia(int idAplicacion, EnumTipoObjeto tipoObjeto, string claveOriginal, int idTipoDocumento)
        {
            var lstTemp = CacheRepository.ReglasEquivalencia.Where(
                    r => r.IdAplicacion == idAplicacion && r.IdTipoObjecto == (short)tipoObjeto && r.ValorObjeto == claveOriginal
                    && r.IdTipoDocumento == idTipoDocumento && r.IdPadre == null).OrderBy(r => r.Secuencia).ToList();
            foreach (var reglaPadre in lstTemp)
            {
                reglaPadre.ReglasHijas.AddRange(
                    CacheRepository.ReglasEquivalencia.Where(
                        r =>
                        r.IdAplicacion == idAplicacion && r.IdTipoObjecto == (short) tipoObjeto &&
                        r.ValorObjeto == claveOriginal
                        && r.IdTipoDocumento == idTipoDocumento && r.IdPadre == reglaPadre.IdreglaEquivalencia).OrderBy(r => r.Secuencia));
            }

            return lstTemp;
        }

        /// <summary>
        /// Método que aplica las reglas que corresponden a la funcionalidad deseada.
        /// </summary>
        /// <param name="objetoFiltro">XML del nodo que se desea evaluar.</param>
        /// <param name="lstReglasEequivalencia">Lista de reglas que aplican al valor objeto.</param>
        /// <returns>Valor de retorno después de la evaluación.</returns>
        public static string AplicarReglasEquivalencia(XDocument objetoFiltro, List<CatReglasEquivalencia> lstReglasEequivalencia)
        {
            string idValorRetorno = "-1";
            foreach (CatReglasEquivalencia regla in lstReglasEequivalencia)
            {
                if (regla.ReglasHijas.Count == 0)
                {
                    if(regla.XPathEvaluacion != string.Empty)
                    {
                        var nodoObjeto = objetoFiltro.XPathSelectElement(regla.XPathEvaluacion);
                        if (nodoObjeto != null)
                        {
                            if (nodoObjeto.Value == regla.ValorEvaluacion || string.IsNullOrEmpty(regla.ValorEvaluacion))
                            {
                                idValorRetorno = regla.ValorRetorno;
                                break;
                            }
                        }
                    }
                    else
                    {
                        idValorRetorno = regla.ValorRetorno;
                        break;
                    }
                }
                else
                {
                    idValorRetorno = AplicarReglasEquivalenciaHijas(objetoFiltro, regla);
                    if (idValorRetorno == "-1")
                        continue;
                    break;
                }
            }

            return idValorRetorno;
        }

        private static string AplicarReglasEquivalenciaHijas(XDocument objetoFiltro, CatReglasEquivalencia regla)
        {
            string idValorRetorno = "-1";
            var esAplicarPadre = true;
            var esAplicarSiguiente = true;
            foreach (var reglaHija in regla.ReglasHijas)
            {
                if(!esAplicarSiguiente)
                    return idValorRetorno;
                var nodoObjeto = objetoFiltro.XPathSelectElement(reglaHija.XPathEvaluacion);
                if(nodoObjeto != null)
                {
                    if(nodoObjeto.Value == reglaHija.ValorEvaluacion || string.IsNullOrEmpty(reglaHija.ValorEvaluacion))
                    {
                        continue;
                    }
                    esAplicarSiguiente = false;
                    esAplicarPadre = false;
                }
                else
                {
                    esAplicarSiguiente = false;
                    esAplicarPadre = false;
                }
            }
            if(esAplicarPadre)
            {
                var nodoObjeto = objetoFiltro.XPathSelectElement(regla.XPathEvaluacion);
                if(nodoObjeto != null)
                {
                    if(nodoObjeto.Value == regla.ValorEvaluacion || string.IsNullOrEmpty(regla.ValorEvaluacion))
                    {
                        idValorRetorno = regla.ValorRetorno;
                    }
                }
            }

            return idValorRetorno;
        }
    }
}
