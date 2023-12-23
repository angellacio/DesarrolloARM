
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.CatMarca:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    [DataContract]
    public class CatMarca
    {
        [DataMember]
        public int CveMarca { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Observacion { get; set; }
        [DataMember]
        public bool MostrarImportes { get; set; }
        [DataMember]
        public bool GenerarLC { get; set; }
        [DataMember]
        public string Portal { get; set; }
        [DataMember]
        public string Telefonos { get; set; }


        public enum Campos
        {
            [E.NombreCampo("CveMarca")]
            [E.NombreParametroSql("@CveMarca")]
            CveMarca = 1,
            [E.NombreCampo("Descripcion")]
            [E.NombreParametroSql("@Descripcion")]
            Descripcion,
            [E.NombreCampo("Observacion")]
            [E.NombreParametroSql("@Observacion")]
            Observacion,
            [E.NombreCampo("MostrarImportes")]
            [E.NombreParametroSql("@MostrarImportes")]
            MostrarImportes,
            [E.NombreCampo("GenerarLC")]
            [E.NombreParametroSql("@GenerarLC")]
            GenerarLC
        }
    }

    
}

