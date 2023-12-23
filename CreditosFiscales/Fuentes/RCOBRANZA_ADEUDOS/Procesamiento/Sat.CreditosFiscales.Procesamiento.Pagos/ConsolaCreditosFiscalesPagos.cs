using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Procesamiento.Pagos.Servicios;

namespace Sat.CreditosFiscales.Procesamiento.Pagos
{
    /// <summary>
    /// Consola de decargo de pagos que contiene la inicilización de la aplicación.
    /// </summary>
    public class ConsolaCreditosFiscalesPagos : ApplicationManager
    {
        /// <summary>
        /// Propiedad que contiene el nombre de la aplicación.
        /// </summary>
        public override string ApplicationName
        {
            get { return "Pagos créditos fiscales"; }
        } 

        public override string ApplicationID
        {
            get { throw new NotImplementedException(); }
        }

        protected override ProcesoPagos InitializeProgram()
        {
            var procesoPagos = new ProcesoPagos();
            return procesoPagos;
        }        
    }
}
