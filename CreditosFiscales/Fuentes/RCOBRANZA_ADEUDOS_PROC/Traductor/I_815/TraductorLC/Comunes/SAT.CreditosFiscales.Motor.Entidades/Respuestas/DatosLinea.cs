using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SAT.CreditosFiscales.Motor.Entidades.Respuestas
{
    [SerializableAttribute]
    public class DatosLinea
    {
        #region Declaraciones

        private string folio;

        private string lineaCaptura;

        #endregion

        [XmlElement()]
        public string Folio
        {
            get
            {
                return this.folio;
            }
            set
            {
                this.folio = value;
            }
        }

        [XmlElement()]
        public string LineaCaptura
        {
            get
            {
                return this.lineaCaptura;
            }
            set
            {
                this.lineaCaptura = value;
            }
        }
    }
}
