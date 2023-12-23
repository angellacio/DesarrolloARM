
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.CatReglasEquivalencia:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Entidades.Catalogos
{
    /// <summary>
    /// Clase que representa el catálogo de reglas de equivalencia.
    /// </summary>
    public class CatReglasEquivalencia
    {        

        public CatReglasEquivalencia()
        {
            ReglasHijas = new List<CatReglasEquivalencia>();
        }

        /// <summary>
        /// Índice del catálogo de Reglas.
        /// </summary>
        public int IdreglaEquivalencia { get; set; }

        /// <summary>
        /// Tipo de aplicación.
        /// </summary>
        public short IdAplicacion { get; set; }

        /// <summary>
        /// Descripción del contenido de la regla.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Tipo de objeto sobre el que aplicará la regla.
        /// </summary>
        public int IdTipoObjecto { get; set; }

        /// <summary>
        /// Valor del objecto que se quiere obtener.
        /// </summary>
        public string ValorObjeto { get; set; }

        /// <summary>
        /// Valor de Salida final después de la evaluación.
        /// </summary>
        public string ValorRetorno { get; set; }

        /// <summary>
        /// Xpath para evaluar el contenido de la regla respecto al xml de entrada.
        /// </summary>
        public string XPathEvaluacion { get; set; }

        /// <summary>
        /// Valor de evaluación.
        /// </summary>
        public string ValorEvaluacion { get; set; }

        /// <summary>
        /// Activo.
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IdTipoDocumento { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int Secuencia { get; set; }

        /// <summary>
        /// Identificador de agrupador de reglas
        /// </summary>
        public int? IdPadre { get; set; }

        /// <summary>
        /// Lista que contendrá las reglas hijas asociadas a la regla principal
        /// </summary>
        public List<CatReglasEquivalencia> ReglasHijas { get; set; }
    }
}