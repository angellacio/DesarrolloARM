using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PruebasUnitarias.UtileriasComunesP
{
    [TestClass]
    public class Logs
    {
        [TestMethod]
        public void Mensajes()
        {
            string sMensaje = "Prueba Metodo {0}";
            UtileriasComunes.ManejoLog.ActualizaLog.mensajeNormal(string.Format(sMensaje, "mensajeNormal"));
            UtileriasComunes.ManejoLog.ActualizaLog.mensajeAuditoria(string.Format(sMensaje, "mensajeAuditoria"));
            UtileriasComunes.ManejoLog.ActualizaLog.mensajeAlerta(string.Format(sMensaje, "mensajeAlerta"));
            UtileriasComunes.ManejoLog.ActualizaLog.mensajeError(string.Format(sMensaje, "mensajeError"));

            for (int nI = 0; nI < 50000; nI++)
            {
                UtileriasComunes.ManejoLog.ActualizaLog.mensajeNormal(string.Format(sMensaje, $"mensajeNormal_{nI}"));
            }
        }
    }
}
