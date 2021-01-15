
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:RFC:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])

using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Types;
using System.Text.RegularExpressions;
using System.Diagnostics;
using SAT.DyP.Util.Configuration;


namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase abstracta del RFC
    /// </summary>
    [Serializable]
    public abstract class RFC
    {
        protected string _rfcOriginal = "";

        protected void Parse()
        {
            if (this._rfcOriginal.Length != this.LongitudValida)
            {
                string errorMessage = String.Format("La longitud del RFC debe ser de {0} caracteres.", this.LongitudValida);
                throw new BusinessException(errorMessage);
            }
            if (!SeEncuentraSintacticamenteBienFormado())
            {
                string errorMessage = String.Format("El RFC '{0}' no se encuentra bien formado.", this._rfcOriginal);
                throw new BusinessException(errorMessage);
            }
            if (!this.ValidaFecha(this.GetSubStringFecha()))
            {
                string errorMessage = String.Format("La fecha '{0}' del RFC no es una fecha válida.", this.GetSubStringFecha());
                throw new BusinessException(errorMessage);
            }
        }
        /// <summary>
        /// Crea instancia de objecto a los cuales hereda.
        /// </summary>
        /// <param name="rfc">Cadena RFC</param>
        /// <param name="tipoDePersona">Enumerador Tipo de Persona (Fisica|Moral)</param>
        /// <returns>Regresa un objeto RFC</returns>
        public static RFC Parse(string rfc, TipoDePersona tipoDePersona)
        {
            if (tipoDePersona == TipoDePersona.Fisica)
                return new RFCPersonaFisica(rfc);
            else if (tipoDePersona == TipoDePersona.Moral)
                return new RFCPersonaMoral(rfc);
            else throw new BusinessException("Tipo de Persona Inválida");
        }
        /// <summary>
        /// Valida que el RFC se encuentre correctamente estructurado
        /// </summary>
        /// <returns>Regresa verdader si fue existosa, falso en caso contrario</returns>
        public bool SeEncuentraSintacticamenteBienFormado()
        {
            Regex regexValidaRFC = new Regex(this.RegExRFC);
            return regexValidaRFC.IsMatch(this._rfcOriginal);
        }
        public bool ValidaFecha(string fecha)
        {
            if (String.IsNullOrEmpty(fecha))
            {
                return false;
            }

            //Valida que el mes no sea mayor a 13
            if (Convert.ToInt16(fecha.Substring(2, 2)) < 13)
            {
                DateTime fechaResultado = new DateTime();
                if (fecha.Trim().Length == 6)
                    return DateTime.TryParseExact(fecha, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaResultado);
                else return false;
            }
            else return false;
        }
        /// <summary>
        /// Obtiene del RFC la fecha en formato AAMMDD
        /// </summary>
        /// <returns>Regresa la cadena Fecha en formato AAMMDD</returns>
        public abstract string GetSubStringFecha();

        /// <summary>
        /// Regresa la longitud del Registro Federal
        /// </summary>
        protected abstract int LongitudValida
        {
            get;
        }

        /// <summary>
        /// Regresa la expresión regular del RFC dependiendo del tipo de RFC (Fisica|Moral)
        /// </summary>
        protected abstract string RegExRFC
        {
            get;
        }

        /// <summary>
        /// Regresa las iniciales de la cadena RFC
        /// </summary>
        public abstract string GetSubStringIniciales();

        /// <summary>
        /// Regresa la homoclave de la cadena RFc
        /// </summary>
        public abstract string GetSubStringHomoClave();

        public static bool Validate(string rfc)
        {
            string rfcGeneralRegex = ConfigurationManager.ApplicationSettings.ReadSetting(@"SAT.DyP.Negocio.Comun::RFCRegEx");
            Regex re = new Regex(rfcGeneralRegex);
            return re.IsMatch(rfc);
        }

        public static TipoDePersona InferirTipoPersona(string rfc)
        {
            if (rfc.Length == 13) return TipoDePersona.Fisica;
            else if (rfc.Length == 12) return TipoDePersona.Moral;
            else throw new BusinessException(String.Format("El formato del RFC '{0}' no es reconocido.", rfc));
        }

        public static string NormalizeLenght(string rfc)
        {
            if (rfc.Length == 12) rfc = " " + rfc;
            return rfc;
        }
    }
}
