using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SAT.CreditosFiscales.Motor.Entidades.Respuestas
{
    [SerializableAttribute]
    public class ListaErrores
    {
        #region Declaraciones        

        private List<string> error;

        #endregion
               
        [XmlElement()]
        public List<string> Error
        {
            get
            {
                return this.error;
            }
            set
            {
                this.error = value;
            }
        }
    }
}
