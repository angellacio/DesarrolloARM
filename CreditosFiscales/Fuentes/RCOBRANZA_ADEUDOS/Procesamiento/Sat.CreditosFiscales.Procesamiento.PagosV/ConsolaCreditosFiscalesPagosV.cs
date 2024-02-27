using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Procesamiento.PagosV.Servicios;

namespace Sat.CreditosFiscales.Procesamiento.PagosV
{
    /// <summary>
    /// Consola de decargo de pagos que contiene la inicilización de la aplicación.
    /// </summary>
    public class ConsolaCreditosFiscalesPagosV : ApplicationManager
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

        protected override ProcesoPagosV InitializeProgram()
        {
            var procesoPagosV = new ProcesoPagosV();
            return procesoPagosV;
        }        
    }
}
