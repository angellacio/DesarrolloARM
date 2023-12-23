
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios.Peticiones:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Datos.AccesoDatos.Servicios;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios
{
    /// <summary>
    /// Clase para manejar la inserción asíncrona de Peticiones y Respuestas que se obtienes de la comunicación con otros sistemas.
    /// </summary>
    public static class Peticiones
    {
        delegate void DelegadoGuardaPeticionAsinc(Peticion peticion);
        /// <summary>
        /// Método que invoca al guradado asincrono de una petición en base de datos.
        /// </summary>
        /// <param name="peticion">Información del request y response realizado a los servicios con los que convive la aplicaición.</param>
        public static void GuardaPeticion(Peticion peticion)
        {
            DelegadoGuardaPeticionAsinc opAsinc = new DelegadoGuardaPeticionAsinc(GuardaPeticionAsinc);
            opAsinc.BeginInvoke(peticion, null, null);
        }
        private static void GuardaPeticionAsinc(Peticion peticion)
        {
            //System.Threading.Thread.Sleep(60000);
            try
            {
                DalPeticion dalPeticion = new DalPeticion();
                dalPeticion.GuardaPeticion(peticion);
            }
            catch (Exception ex)
            {
                AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresInfraestructura.ErrorAlGuardarPeticiones, "Error al guardar mensaje en la tabla TblPeticiones.", ex);
            }
        }

    }
}
