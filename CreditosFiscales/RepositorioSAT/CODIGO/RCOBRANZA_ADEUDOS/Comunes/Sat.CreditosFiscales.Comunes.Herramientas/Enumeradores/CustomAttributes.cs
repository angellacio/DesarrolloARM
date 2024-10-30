
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores:Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores
{
    public class CustomAttributes
    {
        public sealed class NombreCampo : Attribute, IAtributoPersonalizado<string>
        {
            private readonly string valor;

            public NombreCampo(string valorAtributo)
            {
                this.valor = valorAtributo;
            }

            public string Value
            {
                get { return this.valor; }

            }
        }

        public sealed class NombreParametroSql : Attribute, IAtributoPersonalizado<string>
        {
            private readonly string valor;

            public NombreParametroSql(string valorAtributo)
            {
                this.valor = valorAtributo;
            }

            public string Value
            {
                get { return this.valor; }

            }
        }
    }
}
