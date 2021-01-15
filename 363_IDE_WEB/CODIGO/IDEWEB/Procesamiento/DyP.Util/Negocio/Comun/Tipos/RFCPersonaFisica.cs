
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:RFCPersonaFisica:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Configuration;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase Tipo de Persona Fisica
    /// </summary>
    [Serializable]
    public class RFCPersonaFisica: RFC
    {
        private const int LONGITUD_RFC_FISICAS = 13;
        private string REGEX_RFC_FISICAS ="";

        /// <summary>
        /// Constructor de Persona Física
        /// </summary>
        /// <param name="rfc">Cadena RFC</param>
        public RFCPersonaFisica(string rfc)
        {
            REGEX_RFC_FISICAS = ConfigurationManager.ApplicationSettings.ReadSetting("SAT.DyP.Negocio.Comun::RFCPersonaFisicaRegEx");

            this._rfcOriginal = rfc;
            this.Parse();
        }
        /// <summary>
        /// Regresa la longitud del Registro Federal
        /// </summary>
        protected override int LongitudValida
        {
            get { return LONGITUD_RFC_FISICAS; }
        }
        /// <summary>
        /// Obtiene del RFC la fecha en formato AAMMDD
        /// </summary>
        /// <returns>Regresa la cadena Fecha en formato AAMMDD</returns>
        public override string GetSubStringFecha()
        {
            return this._rfcOriginal.Substring(4,6);
        }
        /// <summary>
        /// Regresa la expresión regular del RFC dependiendo del tipo de RFC (Fisica|Moral)
        /// </summary>
        protected override string RegExRFC
        {
            get { return REGEX_RFC_FISICAS; }
            
        }
        /// <summary>
        /// Obtiene del RFC las iniciales 
        /// </summary>
        public override string GetSubStringIniciales()
        {
            return this._rfcOriginal.Substring(0, 4);
        }

        /// <summary>
        /// Obtiene del RFC la homoclave
        /// </summary>
        public override string GetSubStringHomoClave()
        {
            return this._rfcOriginal.Substring(10, 3);
        }
    }
}
