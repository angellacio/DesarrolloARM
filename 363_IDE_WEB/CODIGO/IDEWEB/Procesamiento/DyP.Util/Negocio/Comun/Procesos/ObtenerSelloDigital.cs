
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ObtenerSelloDigital:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Negocio.Comun.Tipos;
namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Obtención del sello digital
    /// </summary>
    [Serializable]
    public class ObtenerSelloDigital
    {
        /// <summary>
        /// Regresa el sello digital en cadena
        /// </summary>
        /// <param name="oDocument">Tipo de dato Documento</param>
        /// <returns>Sello Digital en cadena</returns>
        public string Execute(Documento oDocument)
        { 
            return Guid.NewGuid().ToString();
        }
    }
}
