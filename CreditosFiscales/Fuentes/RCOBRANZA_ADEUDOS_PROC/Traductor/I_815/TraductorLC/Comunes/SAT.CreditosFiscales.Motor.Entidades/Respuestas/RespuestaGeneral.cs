using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SAT.CreditosFiscales.Motor.Entidades.Respuestas
{
    [XmlSerializerFormat]
    [XmlRoot("RespuestaLC")]
    public class RespuestaGeneral
    {

        #region Campos

        private LineasCaptura lineasCaptura;
        private ListaErrores listaErrores;

        #endregion


        #region Propiedades

        [XmlElement(Order = 1)]
        public LineasCaptura LineasCaptura
        {
            get { return lineasCaptura; }
            set { lineasCaptura = value; }
        }

        [XmlElement(Order = 2)]
        public ListaErrores ListaErrores
        {
            get { return listaErrores; }
            set { listaErrores = value; }
        }

        #endregion
    }
}
