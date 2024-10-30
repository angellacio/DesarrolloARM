
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato.Solicitud:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato
{
    [DataContract]
    public class Solicitud
    {

        #region Campos
        [DataMember]
        public Int64 IdSolicitud { get; set; }
        [DataMember]
        public DateTime FechaSolicitud { get; set; }
        [DataMember]
        public Usuario Usuario { get; set; }
        [DataMember]
        public List<DocumentoDeterminante> Documentos { get; set; }
        #endregion

        #region Constructor
        public Solicitud()
        {
            Documentos = new List<DocumentoDeterminante>();
        }
        #endregion

        public enum Campos
        {
            [E.NombreCampo("IdSolicitud")]
            [E.NombreParametroSql("@IdSolicitud")]
            IdSolicitud = 1,



            [E.NombreCampo("FechaSolicitud")]
            [E.NombreParametroSql("@FechaSolicitud")]
            FechaSolicitud,
        }
    }
}
