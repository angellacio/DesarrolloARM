using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Presentacion.Herramientas
{
    public class ClienteServicioReglasEquivalencia : System.ServiceModel.ChannelFactory<IServicioReglasEquivalencia>, IDisposable
    {
        public ClienteServicioReglasEquivalencia() : base("EndPoint_IServicioReglasEquivalencia") { }

        #region Implementación de Idisposable
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    
}
