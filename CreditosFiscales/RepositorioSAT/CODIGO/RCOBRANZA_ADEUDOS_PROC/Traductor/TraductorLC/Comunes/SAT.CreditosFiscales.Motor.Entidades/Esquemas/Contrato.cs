
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.Contratos:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Entidades.Esquemas
{    
    /// <summary>
    /// Clase que hereda del genérico List y representa a los contratos.
    /// </summary>
    public class Contratos : List<Contrato>
    { }

    /// <summary>
    /// Objeto que tiene propiedades y métodos de un Contrato/Esquema.
    /// </summary>
    [Serializable]
    public class Contrato
    {
        #region Propiedades

        /// <summary>
        /// Identificador del esquema.
        /// </summary>
        public short IdEsquema { get; set; }
        
        /// <summary>
        /// Descripción del esquema.
        /// </summary>
        public string Descripcion { get; set; }
        
        /// <summary>
        /// Esquema.
        /// </summary>
        public string Xsd { get; set; }

        /// <summary>
        /// Valor del targetnamespace.
        /// </summary>
        public string TargetNamespace { get; set; }

        /// <summary>
        /// Dirección en la que se está validando el documento.
        /// </summary>
        public Enumeraciones.DireccionTraductor DireccionContrato { get; set; }

        #endregion
    }
}
