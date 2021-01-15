//@(#)SCADE2(W:SKDN08515FF4:Sat.Scade.Net.IDE.Presentacion.Web:Recursos:0:23/Diciembre/2008[Sat.Scade.Net.IDE.Presentacion.Web:0:23/Diciembre/2008]) 
using System;
using System.Configuration;
using SAT.DyP.Util.Web.WebServiceConfiguracion;
using System.Globalization;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public enum MedioPresentacion
    {
        Internet = 3,
        Modulo = 4
    }

    /// <summary>
    /// Enumeracion para Tipo de Autenticacion por FIEL o CIEC
    /// </summary>
    public enum TipoAutenticacion
    {
        Ciec = 0,
        Fiel = 1
    }   

    /// <summary>
    /// Enumeracion para Tipo Respuesta
    /// </summary>
    public enum TipoRespuesta
    {
        Aceptada = 1,
        Rechazada = 0
    }

    public static class Configuraciones
    {
        // R2. Constante para el Servicio para Actualizar el Correo.
        public const string UrlServicioActualizacionCorreo = "SAT.DyP.Negocio.Comun::LigaServicioActualizacionCorreo";
    }
}