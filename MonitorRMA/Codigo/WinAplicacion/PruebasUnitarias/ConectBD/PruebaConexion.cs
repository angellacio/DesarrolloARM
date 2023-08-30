using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using BDEnumComp = UtileriasBaseDatos.EnumTextos.EnumComponentes;
using BDEnti = UtileriasBaseDatos.Entidades;
using UtiError = UtileriasComunes.ManejoErrores;

namespace PruebasUnitarias.ConectBD
{
    [TestClass]
    public class PruebaConexion
    {
        [TestMethod]
        public void FuncionDataTable()
        {
            UtileriasBaseDatos.Conect_BD_SQL conectBD = null;
            List<BDEnti.entParametro> ParametrosBD = null;
            DataTable dt = null;
            try
            {
                ParametrosBD = new List<BDEnti.entParametro>()
                {
                    new BDEnti.entParametro { p_Nombre = "@nTipoConsulta", p_Tipo = BDEnumComp.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = 1 },
                    new BDEnti.entParametro { p_Nombre = "@nIdUsuario", p_Tipo = BDEnumComp.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = null },
                    new BDEnti.entParametro { p_Nombre = "@sUsuario", p_Tipo = BDEnumComp.BD_TipoParametro.TextoNor, p_Tamaño = 50, p_Valor = "aramirez" },
                    new BDEnti.entParametro { p_Nombre = "@sContrasenia", p_Tipo = BDEnumComp.BD_TipoParametro.TextoNor, p_Tamaño = 150, p_Valor = "oEFPCa1Z1k+qV2t65a98ypA==" }
                };

                conectBD = new UtileriasBaseDatos.Conect_BD_SQL("conBDManejoRMA");

                dt = conectBD.ConsultaDataTable("catSeguridadUsuario", ParametrosBD, BDEnumComp.BD_TipoComando.StoreProcedure);
                Assert.IsNotNull(dt);
            }
            catch (UtiError.ErroresAplicacion ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                conectBD.Finally();
            }
        }
        [TestMethod]
        public void FuncionDataSet()
        {
            UtileriasBaseDatos.Conect_BD_SQL conectBD = null;
            List<BDEnti.entParametro> ParametrosBD = null;
            DataSet ds = null;
            try
            {
                ParametrosBD = new List<BDEnti.entParametro>()
                {
                    new BDEnti.entParametro { p_Nombre = "@nTipoConsulta", p_Tipo = BDEnumComp.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = 1 },
                    new BDEnti.entParametro { p_Nombre = "@nIdUsuario", p_Tipo = BDEnumComp.BD_TipoParametro.EnteroNor, p_Tamaño = null, p_Valor = null },
                    new BDEnti.entParametro { p_Nombre = "@sUsuario", p_Tipo = BDEnumComp.BD_TipoParametro.TextoNor, p_Tamaño = 50, p_Valor = "idias" },
                    new BDEnti.entParametro { p_Nombre = "@sContrasenia", p_Tipo = BDEnumComp.BD_TipoParametro.TextoNor, p_Tamaño = 150, p_Valor = "oEFPCa1Z1k+qV2t65a98yA==" }
                };

                conectBD = new UtileriasBaseDatos.Conect_BD_SQL("conBDManejoRMA");

                ds = conectBD.ConsultaDataSet("catSeguridadUsuario", ParametrosBD, BDEnumComp.BD_TipoComando.StoreProcedure);
                Assert.IsNotNull(ds);
            }
            catch (UtiError.ErroresAplicacion ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                conectBD.Finally();
            }
        }
    }
}



