
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Utilidades:SAT.CreditosFiscales.Motor.Utilidades.UtilidadEnumeraciones:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Utilidades
{
    public class UtilidadEnumeraciones
    {        
        /// <summary>
        /// Obtiene la descripción de un enumerado.
        /// </summary>
        /// <param name="enumerador">Enumerado.</param>
        /// <returns>Descripción del enumerado.</returns>
        public static string ObtieneDescripcionEnum(Enum enumerador)
        {
            Type type = enumerador.GetType();
            MemberInfo[] memInfo = type.GetMember(enumerador.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var atributos = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (atributos != null && atributos.Length > 0)
                {
                    return ((DescriptionAttribute)atributos[0]).Description;
                }
            }

            //// Si no cuenta con el atributo se regresa el nombre.
            return enumerador.ToString();
        }
    }
}
