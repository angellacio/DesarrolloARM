using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RecepcionIDEWEB.Negocio.Contratos
{

        [DataContract]

        public class RecEntrada
        {
            [DataMember]
            public int IdMedioRecepcion
            { get; set; }
            [DataMember]
            public int IdEntidadReceptora
            { get; set; }
            [DataMember]
            public String RfcAutenticacion
            { get; set; }
            [DataMember]
            public String RfcContribuyente
            { get; set; }
            [DataMember]
            public String DireccionIP
            { get; set; }
            [DataMember]
            public String NombreArchivo
            { get; set; }
            [DataMember]
            public int TamañoArchivo
            { get; set; }
            [DataMember]
            public String Formato
            { get; set; }
            [DataMember]
            public int Materia
            { get; set; }
            [DataMember]
            public bool EsNormal
            { get; set; }
            [DataMember]
            public bool EsAnual
            { get; set; }
            [DataMember]
            public String ArchivoFisico
            { get; set; }
 

    }
}