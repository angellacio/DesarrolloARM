
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:VerificarUsuario:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	

using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.Security.Interop.CryptDecrypt;
using SAT.DyP.Negocio.Comun.Procesos.Datos;
using SAT.DyP.Negocio.Comun.Tipos;


namespace SAT.DyP.Negocio.Comun.Procesos.Autenticacion
{
    /// <summary>
    /// Componente responsable de validar la información del
    /// acceso de un usuario en la base de datos SPED
    /// </summary>
    public class VerificarUsuario
    {
        /// <summary>
        /// Devuelve la información del acceso del usuario
        /// </summary>
        /// <param name="usuario">Clave del usuario</param>
        /// <returns></returns>
        public ControlAcceso GetInfo(string usuario)
        {
            ControlAcceso info = null;
            DALDatosAcceso daoAcceso = new DALDatosAcceso();

            try
            {
                 info= daoAcceso.ObtenerDatosUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex.Message, ex);
            }

            return info;
        }

        /// <summary>
        /// Valida la información de la cuenta de acceso
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <param name="clave">contraseña</param>
        /// <returns></returns>
        public bool ValidarAcceso(string usuario, string clave,string perfil)
        {
            bool resultado = false;

            DALDatosAcceso daoAcceso = new DALDatosAcceso();

            try
            {
                //Se obtiene la información del control de acceso del usuario
                ControlAcceso datosAcceso = daoAcceso.ObtenerDatosUsuario(usuario);

                if (datosAcceso != null)
                {
                    CCryptDecryptClass seguridad=new CCryptDecryptClass();
                    string llave = datosAcceso.Llave;

                    //se encripta la contraseña enviada en el login
                    string encriptarClave = seguridad.Encripta(ref clave,ref llave);

                    //se compara la contraseña enviada con la existente
                    resultado= encriptarClave.Equals(datosAcceso.Clave);

                    if (resultado)
                    {
                        List<int> permisosRegistrados = ControlAcceso.ParsePermisos(datosAcceso.Perfil,',');
                        List<int> permisosActuales = ControlAcceso.ParsePermisos(perfil,',');

                        //se comparan los permisos para comprobar el acceso del usuario
                        resultado = ControlAcceso.CompararPermisos(permisosActuales, permisosRegistrados);

                        if (!resultado)
                        {
                            throw new PlatformException("El usuario no cuenta con los permisos suficientes para la aplicación.");
                        }
                    }
                }
                else
                {
                    //se lanza una excepción ya que no existe información en las tablas
                    //de TControlAcceso y TCatContrib
                    throw new PlatformException("No existe información de acceso para la cuenta de usuario.");
                }
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex.Message);
            }
            
            return resultado;
        }
    }
}
