
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas:Sat.CreditosFiscales.Presentacion.Herramientas.ClienteServicioCatalogos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Presentacion.Herramientas
{
   public  class ClienteServicioCatalogos : ChannelFactory<IServicioCatalogos>, IDisposable
    {
        public ClienteServicioCatalogos() : base("EndPoint_IServicioCatalogos") { }

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
