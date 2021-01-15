
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:ControlAcceso:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase que representa los datos del control de acceso
    /// de la tabla TControlAcceso
    /// </summary>
    public class ControlAcceso
    {
        private string _rfc;
        private string _usuario;
        private string _clave;
        private string _llave;
        private string _perfil;

        public string RFC
        {
            get { return _rfc; }
            set { _rfc = value; }
        }

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }

        public string Llave
        {
            get { return _llave; }
            set { _llave = value; }
        }

        public string Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }

        public static List<int> ParsePermisos(string listadoPermisos, char separador)
        {
            List<int> listado = new List<int>();

            string[] perfiles = listadoPermisos.Split(separador);
            foreach (string item in perfiles)
            {
                if (!string.IsNullOrEmpty(item))
                    listado.Add(int.Parse(item));
            }

            return listado;
        }

        /// <summary>
        /// Realiza la comparación de los permisos registrados para un usuario
        /// contra un listado de permisos solicitado
        /// </summary>
        /// <param name="PermisosActuales">Lista de permisos solicitados</param>
        /// <param name="PermisosRegistrados">Lista de permisos registrados</param>
        /// <returns></returns>
        public static bool CompararPermisos(List<int> PermisosActuales, List<int> PermisosRegistrados)
        {
            bool resultado = false;

            foreach (int permiso in PermisosActuales)
            {
                if (PermisosRegistrados.Contains(permiso))
                {
                    resultado = true;
                    break;
                }
                else
                {
                    resultado = false;
                }
            }

            return resultado;
        }
    }
}
