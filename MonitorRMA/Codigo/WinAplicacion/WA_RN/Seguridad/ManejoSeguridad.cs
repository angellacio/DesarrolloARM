using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
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
        private string Config_Usuario
        {
            get
            {
                string sResult;
                try
                {
                    sResult = ConfigurationManager.AppSettings.Get("usrDefault");
                    if (sResult == null) sResult = "";
                    sResult = sResult.Trim();
                }
                catch { sResult = ""; }
                return sResult;
            }
        }
        private string Config_Pswr
        {
            get
            {
                string sResult;
                try
                {
                    sResult = ConfigurationManager.AppSettings.Get("pswDefault");
                    if (sResult == null) sResult = "";
                    sResult = sResult.Trim();
                }
                catch { sResult = ""; }
                return sResult;
            }
        }

        public ent.Seguridad.EntDatosAutentificacion ValidaUsuario()
        {
            ent.Seguridad.EntDatosAutentificacion itemDatosUsuario;
            try
            {
                if (string.IsNullOrEmpty(Config_Usuario) && string.IsNullOrEmpty(Config_Pswr)) throw new exM.ErroresAplicacion(exM.ErroresAplicacion.TipoError.ErrorAplicacion, -999, "Acceso pantalla.");

                itemDatosUsuario = ValidaUsuario(Config_Usuario, Config_Pswr, false);

            }
            catch (exM.ErroresAplicacion ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new exM.ErroresAplicacion(exM.ErroresAplicacion.TipoError.ErrorGenerico, 0, "Error al validar el usuario", ex);
            }
            return itemDatosUsuario;
        }

        public ent.Seguridad.EntDatosAutentificacion ValidaUsuario(string sUsuario, string sPasword, Boolean bolEcripta)
        {
            ent.Seguridad.EntDatosAutentificacion itemDatosUsuario = null;
            try
            {
                DataSet ds = ValidaUsusarioBD(sUsuario, sPasword, bolEcripta);

                foreach (DataTable DTable in ds.Tables)
                {
                    if (DTable.TableName.Equals("Table"))  // Datos Empleados
                    {
                        foreach (DataRow DRow in DTable.Rows)
                        {
                            itemDatosUsuario = new ent.Seguridad.EntDatosAutentificacion(DRow);
                        }
                    }
                    else if (DTable.TableName.Equals("Table1")) // Datos Area
                    {
                        foreach (DataRow DRow in DTable.Rows)
                        {
                            itemDatosUsuario.Areas.Add(new ent.Empleados.EntUbicacion(utilEnum.Empelados.TC_Ubicacion.Area, DRow, itemDatosUsuario.IdArea));
                        }
                    }
                    else if (DTable.TableName.Equals("Table2")) // Datos Aplicativo
                    {
                        foreach (DataRow DRow in DTable.Rows)
                        {
                            itemDatosUsuario.Aplicativos.Add(new ent.Empleados.EntUbicacion(utilEnum.Empelados.TC_Ubicacion.Aplicativo, DRow, itemDatosUsuario.IdAplicativo));
                        }
                    }
                }
                //if (itemDatosUsuario.Areas.Count > 0) itemDatosUsuario.Areas.Add(new ent.Empleados.EntUbicacion(utilEnum.Empelados.TC_Ubicacion.Area) { IdTabla = -1, Tabla = " ** Todos ** ", EstadoTabla = true, IdCatalogo = -1, Acronimo = "", Orden = -1, Observaciones = "", Catalogo = " ** Todos ** ", EstadaCatalogo = true });
                //if (itemDatosUsuario.Aplicativos.Count > 0) itemDatosUsuario.Aplicativos.Add(new ent.Empleados.EntUbicacion(utilEnum.Empelados.TC_Ubicacion.Aplicativo) { IdTabla = -1, Tabla = " ** Todos ** ", EstadoTabla = true, IdCatalogo = -1, Acronimo = "", Orden = -1, Observaciones = "", Catalogo = " ** Todos ** ", EstadaCatalogo = true });
            }
            catch (exM.ErroresAplicacion ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new exM.ErroresAplicacion(exM.ErroresAplicacion.TipoError.ErrorGenerico, 0, "Error al validar el usuario", ex);
            }
            return itemDatosUsuario;
        }

        private DataSet ValidaUsusarioBD(string sUsuario, string sPasword, Boolean bEncript)
        {
            DataSet dsResult = null;
            conBD.Conect_BD_SQL ManejoBD = null;
            try
            {
                List<conBDEnt.entParametro> ParametrosBD = new List<conBD.Entidades.entParametro>()
                {
                    new conBDEnt.entParametro { p_Nombre = "@nTipoConsulta", p_Tipo = conBDEnum.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = 1 },
                    new conBDEnt.entParametro { p_Nombre = "@nIdUsuario", p_Tipo = conBDEnum.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = null },
                    new conBDEnt.entParametro { p_Nombre = "@sUsuario", p_Tipo = conBDEnum.BD_TipoParametro.TextoNor, p_Tamaño = 50, p_Valor = sUsuario },
                    new conBDEnt.entParametro { p_Nombre = "@sContrasenia", p_Tipo = conBDEnum.BD_TipoParametro.TextoNor, p_Tamaño = 150, p_Valor = bEncript == true ? utilEncript.Encriptar(sPasword, utilEnum.Encriptador.TipoLlave.Usuario) : sPasword }
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
            finally
            {
                ManejoBD.Finally();
            }
            return dsResult;
        }
    }
}
