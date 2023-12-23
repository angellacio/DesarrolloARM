using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Ent = WA_Entidades;
using conBD = UtileriasBaseDatos;
using conBDEnt = UtileriasBaseDatos.Entidades;
using conBDEnum = UtileriasBaseDatos.EnumTextos.EnumComponentes;
using exM = UtileriasComunes.ManejoErrores;

namespace WA_RN.Catalogos
{
    public class ManejoCatalogo : IManejoCatalogo
    {

        public List<Ent.EntCatalogo> Consulta_PR_Cat_TipoRequerimiento()
        {
            List<Ent.EntCatalogo> lstResult;
            try
            {
                lstResult = ConsultaCatalogo(1, 1, null, true, null, null, null, null, null);
            }
            catch (exM.ErroresAplicacion ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new exM.ErroresAplicacion(exM.ErroresAplicacion.TipoError.ErrorGenerico, 0, "Error al validar el usuario", ex);
            }
            return lstResult; 
        }

        public List<Ent.EntCatalogo> Consulta_PR_Cat_EstadosRMA()
        {
            List<Ent.EntCatalogo> lstResult;
            try
            {
                lstResult = ConsultaCatalogo(1, 2, null, true, null, null, null, null, null);
            }
            catch (exM.ErroresAplicacion ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new exM.ErroresAplicacion(exM.ErroresAplicacion.TipoError.ErrorGenerico, 0, "Error al validar el usuario", ex);
            }
            return lstResult;
        }

        public List<Ent.EntCatalogo> Consulta_PR_Cat_Aplicativos()
        {
            List<Ent.EntCatalogo> lstResult;
            try
            {
                lstResult = ConsultaCatalogo(1, 6, null, true, null, null, null, null, null);
            }
            catch (exM.ErroresAplicacion ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new exM.ErroresAplicacion(exM.ErroresAplicacion.TipoError.ErrorGenerico, 0, "Error al validar el usuario", ex);
            }
            return lstResult;
        }


        private List<Ent.EntCatalogo> ConsultaCatalogo(int TipoConsulta, int? IdTabla, string Tabla, Boolean? EstadoTabla, int? IdDetalle, string Acronimo, string Observacion, string Detalle, Boolean? EstadoDetalle)
        {
            List<Ent.EntCatalogo> lstResult;
            try
            {
                lstResult = new List<Ent.EntCatalogo>();
                DataSet ds = Catalogo(TipoConsulta, IdTabla, Tabla, EstadoTabla, IdDetalle, Acronimo, Observacion, Detalle, EstadoDetalle);

                foreach (DataTable Dt in ds.Tables)
                {
                    foreach (DataRow Dr in Dt.Rows)
                    {
                        lstResult.Add(new Ent.EntCatalogo(Dr));
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
            return lstResult;
        }

        private DataSet Catalogo(int TipoConsulta, int? IdTabla, string Tabla, Boolean? EstadoTabla, int? IdDetalle, string Acronimo, string Observacion, string Detalle, Boolean? EstadoDetalle)
        {
            DataSet dsResult = null;
            conBD.Conect_BD_SQL ManejoBD = null;
            try
            {
                List<conBDEnt.entParametro> ParametrosBD = new List<conBD.Entidades.entParametro>()
                {
                    new conBDEnt.entParametro { p_Nombre = "@nTipoConsulta", p_Tipo = conBDEnum.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = TipoConsulta },
                    new conBDEnt.entParametro { p_Nombre = "@nIdTbla", p_Tipo = conBDEnum.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = IdTabla },
                    new conBDEnt.entParametro { p_Nombre = "@sTabla", p_Tipo = conBDEnum.BD_TipoParametro.TextoNor, p_Tamaño = 50, p_Valor = Tabla },
                    new conBDEnt.entParametro { p_Nombre = "@bEstadoT", p_Tipo = conBDEnum.BD_TipoParametro.SiNo, p_Tamaño = 150, p_Valor =  EstadoTabla},
                    new conBDEnt.entParametro { p_Nombre = "@nIdDetalle", p_Tipo = conBDEnum.BD_TipoParametro.EnteroNor, p_Tamaño = 150, p_Valor =  IdDetalle},
                    new conBDEnt.entParametro { p_Nombre = "@sAcronimo", p_Tipo = conBDEnum.BD_TipoParametro.TextoNor, p_Tamaño = 150, p_Valor =  Acronimo},
                    new conBDEnt.entParametro { p_Nombre = "@sObservaciones", p_Tipo = conBDEnum.BD_TipoParametro.TextoNor, p_Tamaño = 150, p_Valor =  Observacion},
                    new conBDEnt.entParametro { p_Nombre = "@sTablaDetalle", p_Tipo = conBDEnum.BD_TipoParametro.TextoNor, p_Tamaño = 150, p_Valor =  Detalle},
                    new conBDEnt.entParametro { p_Nombre = "@bEstadoD", p_Tipo = conBDEnum.BD_TipoParametro.SiNo, p_Tamaño = 150, p_Valor =  EstadoDetalle}
                };

                ManejoBD = new conBD.Conect_BD_SQL("conBDManejoRMA");

                dsResult = ManejoBD.ConsultaDataSet("catSensilloConsulta", ParametrosBD, conBDEnum.BD_TipoComando.StoreProcedure);
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
