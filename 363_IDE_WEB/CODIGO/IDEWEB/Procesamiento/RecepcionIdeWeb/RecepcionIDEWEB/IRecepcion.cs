using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RecepcionIDEWEB.Negocio.Contratos;

namespace RecepcionIDEWEB
{
    [ServiceContract]
    public interface IRecepcion
    {   
        [OperationContract]
        RecSalida RegistrarArchivo(RecEntrada ObjRecep);

        //[OperationContract]
        //RecSalida RegistrarArchivoModulo(RecEntrada ObjRecep);
        
        [OperationContract]
        RecVerifica ExisteArchivo(string archivo);
       
        [OperationContract]
        List<Mensaje> GetMensajes(int idTipoMensaje);

        [OperationContract]
        long SiDesEncripta(string rutaArch, string archOrig);
        
    }
}
