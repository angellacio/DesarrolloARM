
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas:Sat.CreditosFiscales.Presentacion.Herramientas.ClienteServicioLineaCaptura:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Presentacion.Herramientas
{
    public class ClienteServicioLineaCaptura : System.ServiceModel.ChannelFactory<IServicioLineasCaptura>, IDisposable
    {
        public ClienteServicioLineaCaptura() : base("EndPoint_IServicioLineasCaptura") { }

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
