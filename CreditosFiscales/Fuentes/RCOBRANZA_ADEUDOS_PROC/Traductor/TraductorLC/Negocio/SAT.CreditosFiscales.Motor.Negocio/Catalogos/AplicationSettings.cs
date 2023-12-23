
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.AplicationSettings:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;

namespace SAT.CreditosFiscales.Motor.Negocio.Catalogos
{
    public static class AplicationSettings
    {
        public static object ConsultaConfiguracion(string settingName)
        {
            //DalApplicationSettings dalAppSettings = new DalApplicationSettings();
            return DalApplicationSettings.RecuperaConfiguracion(settingName);
        }

        public static T ConsultaConfiguracion<T>(string settingName)
        {
            return (T)Convert.ChangeType(ConsultaConfiguracion(settingName), typeof(T));
        }
    }
}
