using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = WA_Entidades;
using exM = UtileriasComunes.ManejoErrores;
using utilEncript = UtileriasComunes.UtileriasEncripta.EncriptDecript;
using utilEnum = UtileriasComunes.ManejoEnumTextos;
using conBD = UtileriasBaseDatos;
using conBDEnt = UtileriasBaseDatos.Entidades;
using conBDEnum = UtileriasBaseDatos.EnumTextos.EnumComponentes;

namespace WA_RN.Seguridad
{
    public class ManejoSeguridad : IManejoSeguridad
    {
        public ent.EntEmpleado ValidaUsuario(string sUsuario, string sPasword)
        {
            ent.EntEmpleado itemEmpleado = null;
            DataSet ds = null;
            try
            {
                itemEmpleado = new ent.EntEmpleado();
                ds = validaUsusarioBD(sUsuario, sPasword);

                foreach (DataTable dt in ds.Tables) 
                {
                    foreach(DataRow dr in dt.Rows)
                    {

                    }
                }
            }
            catch (exM.ErroresAplicacion ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new exM.ErroresAplicacion(exM.ErroresAplicacion.TipoError.ErrorGenerico, 0, "Error al validar el usuario", ex);
            }
            return itemEmpleado;
        }

        public DataSet validaUsusarioBD(string sUsuario, string sPasword)
        {
            DataSet dsResult = null;
            List<conBDEnt.entParametro> ParametrosBD = null;
            conBD.Conect_BD_SQL ManejoBD = null;
            try
            {
                ParametrosBD = new List<conBD.Entidades.entParametro>()
                {
                    new conBDEnt.entParametro { p_Nombre = "@nTipoConsulta", p_Tipo = conBDEnum.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = 1 },
                    new conBDEnt.entParametro { p_Nombre = "@nIdUsuario", p_Tipo = conBDEnum.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = null },
                    new conBDEnt.entParametro { p_Nombre = "@sUsuario", p_Tipo = conBDEnum.BD_TipoParametro.TextoNor, p_Tamaño = 50, p_Valor = sUsuario },
                    new conBDEnt.entParametro { p_Nombre = "@sContrasenia", p_Tipo = conBDEnum.BD_TipoParametro.TextoNor, p_Tamaño = 150, p_Valor = utilEncript.Encriptar(sPasword, utilEnum.Encriptador.TipoLlave.Usuario) }
                };

                ManejoBD = new conBD.Conect_BD_SQL("conBDManejoRMA");

                dsResult = ManejoBD.ConsultaDataSet("catSeguridadUsuario", ParametrosBD, conBDEnum.BD_TipoComando.StoreProcedure);
            }
            catch (exM.ErroresAplicacion ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new exM.ErroresAplicacion(exM.ErroresAplicacion.TipoError.ErrorGenerico, 0, "Error al validar el usuario", ex);
            }
            return dsResult;
        }
    }
}
