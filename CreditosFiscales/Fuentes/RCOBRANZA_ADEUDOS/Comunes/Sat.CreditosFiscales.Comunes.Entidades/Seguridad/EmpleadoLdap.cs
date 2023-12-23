
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Seguridad:Sat.CreditosFiscales.Comunes.Entidades.Seguridad.EmpleadoLdap:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Seguridad
{
    [Serializable]
    [DataContract]
    public class EmpleadoLdap
    {
        #region Campos
        private string rfcCorto;

        private string rfc;

        private string nombreCompleto;

        private List<string> alr;

        private List<string> alrCveCorta;

        private List<string> rol;

        //private PerfilEmpleado perfil;

        private bool accesoValido;

        private string descripcionRol;




        #endregion

        #region Propiedades

        /// <summary>
        /// RFC corto.
        /// </summary>
        [DataMember]
        public string RfcCorto
        {
            get { return rfcCorto; }
            set { rfcCorto = value; }
        }

        /// <summary>
        /// RFC.
        /// </summary>
        [DataMember]
        public string Rfc
        {
            get { return rfc; }
            set { rfc = value; }
        }

        /// <summary>
        /// Nombre completo.
        /// </summary>
        [DataMember]
        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set { nombreCompleto = value; }
        }

        /// <summary>
        /// Lista de ALR's
        /// </summary>
        [DataMember]
        public List<string> Alr
        {
            get { return alr; }
            set { alr = value; }
        }

        /// <summary>
        /// ALR clave corta.
        /// </summary>
        [DataMember]
        public List<string> AlrCveCorta
        {
            get { return alrCveCorta; }
            set { alrCveCorta = value; }
        }

        /// <summary>
        /// Lista de roles.
        /// </summary>
        [DataMember]
        public List<string> Rol
        {
            get { return rol; }
            set { rol = value; }
        }
        

        /// <summary>
        /// Bandera que indica si el acceso es valido.
        /// </summary>
        [DataMember]
        public bool AccesoValido
        {
            get { return accesoValido; }
            set { accesoValido = value; }
        }

        /// <summary>
        /// Descripción del rol.
        /// </summary>
        [DataMember]
        public string DescripcionRol
        {
            get { return descripcionRol; }
            set { descripcionRol = value; }
        }
        #endregion
    }
}
