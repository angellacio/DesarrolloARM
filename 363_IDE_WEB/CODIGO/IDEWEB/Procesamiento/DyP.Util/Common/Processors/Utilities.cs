using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Util.Processors
{
    public sealed class Utilities
    {
        public static string CleanInvalidXmlChars(string text)
        {
            // Caracteres validos de la especificacion de XML:
            // #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]    
            // Cualquier caracter unicode, excluyendo los valores: FFFE y FFFF.
            string re= ConfigurationManager.ApplicationSettings.ReadSetting("SAT.DyP.Util::InvalidXmlChars");
            try
            {
                return Regex.Replace(text, re, "");
            }
            catch (Exception ex)
            {
                throw new PlatformException(string.Format("Error al Validar la Expresion Regular {0}", re), ex);
            }
        }
    }
}
