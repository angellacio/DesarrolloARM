using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using exM = UtileriasComunes.ManejoErrores;
using ent = WA_Entidades;

namespace PruebasUnitarias.Seguridad
{
    [TestClass]
    public class AccesoUsuario
    {
        [TestMethod]
        public void AccesoUsuarioConraseña()
        {
            ent.Seguridad.EntDatosAutentificacion entDatUser = null;
            try
            {
                WA_RN.Seguridad.IManejoSeguridad mSeguridad = new WA_RN.Seguridad.ManejoSeguridad();

                entDatUser = mSeguridad.ValidaUsuario("aramirez", "12345678a", true);

                Assert.IsNotNull(entDatUser);
            }
            catch (exM.ErroresAplicacion ex) 
            {
                if (ex.IdError < 0 ) { Assert.IsNotNull(entDatUser); }
                else Assert.Fail(ex.ToString());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally { }
        }
        [TestMethod]
        public void AccesoConfig()
        {
            ent.Seguridad.EntDatosAutentificacion entDatUser;
            try
            {
                WA_RN.Seguridad.IManejoSeguridad mSeguridad = new WA_RN.Seguridad.ManejoSeguridad();

                entDatUser = mSeguridad.ValidaUsuario();

                Assert.IsNotNull(entDatUser);
            }
            catch (exM.ErroresAplicacion ex)
            {
                Assert.AreEqual(ex.IdError, -999, ex.ToString());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally { }
        }
    }
}
