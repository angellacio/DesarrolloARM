
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.ApplicationSetting:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    [DataContract]
    public class ApplicationSetting
    {
         [DataMember]
         public string SettingName { get; set; }

         [DataMember]
         public object SettingValue { get; set; }

         public enum Campos
         {
             [E.NombreCampo("SettingName")]
             [E.NombreParametroSql("@SettingName")]
             SettingName = 1,

             [E.NombreCampo("SettingValue")]
             [E.NombreParametroSql("@SettingValue")]
             SettingValue
         }
    }
}
