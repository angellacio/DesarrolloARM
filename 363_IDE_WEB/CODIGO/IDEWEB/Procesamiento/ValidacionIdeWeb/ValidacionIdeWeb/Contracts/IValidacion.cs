using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ValidacionIdeWeb
{
    [ServiceContract]
    public interface IValidacion
    {
        [OperationContract]
        void iniciarValidacion(int folio);
    }
}
