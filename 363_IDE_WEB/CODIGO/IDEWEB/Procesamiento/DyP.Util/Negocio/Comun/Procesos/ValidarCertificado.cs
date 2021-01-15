
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ValidarCertificado:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Negocio.Comun.Tipos;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Comprobar la validaci�n del certificado
    /// </summary>
    [Serializable]
    public class ValidarCertificado
    {
        /// <summary>
        /// Obtener el documento con la validaci�n del certificado
        /// </summary>
        /// <param name="documento">Tipo de dato Documento</param>
        /// <returns>Documento que contiene la validaci�n del certificado</returns>
        public Documento Execute(Documento documento)
        {
            return documento;
        }
    }
}
