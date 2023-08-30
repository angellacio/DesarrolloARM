using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PruebasUnitarias.UtileriasComunesP
{
    [TestClass]
    public class UtileriasEncripta
    {
        [TestMethod]
        public void EncriptaUsuario()
        {
            try
            {
                string sUsuarioEncript = UtileriasComunes.UtileriasEncripta.EncriptDecript.Encriptar("12345678a", UtileriasComunes.ManejoEnumTextos.Encriptador.TipoLlave.Usuario);
                Assert.AreEqual("oEFPCa1Z1k+qV2t65a98yA==", sUsuarioEncript, sUsuarioEncript);
            }
            catch (Exception ex) 
            {  
                Assert.Fail(ex.ToString());
            }
        }
        [TestMethod]
        public void DesencriptaUsuario()
        {
            try
            {
                string Desencript = UtileriasComunes.UtileriasEncripta.EncriptDecript.Desencriptar("oEFPCa1Z1k+qV2t65a98yA==", UtileriasComunes.ManejoEnumTextos.Encriptador.TipoLlave.Usuario);
                Assert.AreEqual("12345678a", Desencript, Desencript);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void EncriptaBDRMA()
        {
            try
            {
                string sBDEncript = UtileriasComunes.UtileriasEncripta.EncriptDecript.Encriptar("Data Source=192.168.37.130;User ID=sa;Password=Sat.2023;Initial Catalog=ManejoRMA;Persist Security Info=True", UtileriasComunes.ManejoEnumTextos.Encriptador.TipoLlave.CadenaConexion);

                Assert.AreEqual("F4n+Lpm1+o0EEPwn4SvDdhK7LA6bUzKKcGe4NjaYB4cYX8CHFeBBxKJ7ig241oqRBBbzkeIljBYaI+8isSqklnKsQk3MnrMt2Er7NtGvn30FS5tEO49JhRh60Fp/A2Fker7LUdwy/RClcWZhhbOl3w==", sBDEncript, sBDEncript);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [TestMethod]
        public void DesencriptaBDRMA()
        {
            try
            {
                string sBDDecript = UtileriasComunes.UtileriasEncripta.EncriptDecript.Desencriptar("F4n+Lpm1+o0EEPwn4SvDdhK7LA6bUzKKcGe4NjaYB4cYX8CHFeBBxKJ7ig241oqRBBbzkeIljBYaI+8isSqklnKsQk3MnrMt2Er7NtGvn30FS5tEO49JhRh60Fp/A2Fker7LUdwy/RClcWZhhbOl3w==", UtileriasComunes.ManejoEnumTextos.Encriptador.TipoLlave.CadenaConexion);

                Assert.AreEqual("Data Source=192.168.37.130;User ID=sa;Password=Sat.2023;Initial Catalog=ManejoRMA;Persist Security Info=True", sBDDecript, sBDDecript);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}
