using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SAT.CreditosFiscales.Motor.Entidades.Respuestas
{
    [SerializableAttribute]
    public class LineasCaptura
    {
        #region Campos
        
        private List<DatosLinea> datosLinea;
        
        #endregion

        [XmlElement()]
        public List<DatosLinea> DatosLinea
        {
            get
            {
                return this.datosLinea;
            }
            set
            {
                this.datosLinea = value;
            }
        }
    }
}
