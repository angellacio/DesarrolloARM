using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RecepcionIDEWEB.Negocio.Contratos;

namespace RecepcionIDEWEB
{
    public class ServicioRecepcion : IRecepcion
    {

        public RecSalida RegistrarArchivo(RecEntrada ObjRecep)
        {
            Negocio.AccesoDatos.Datos DATOS = new Negocio.AccesoDatos.Datos();
            RecSalida SS = new RecSalida();
            SS = DATOS.generarFolio(ObjRecep);
            return SS;
        }
        //public RecSalida RegistrarArchivoModulo(RecEntrada ObjRecep)
        //{
        //    Negocio.AccesoDatos.Datos DATOS = new Negocio.AccesoDatos.Datos();
        //    RecSalida SS = new RecSalida();
        //    SS = DATOS.generarFolioModulo(ObjRecep);
        //    return SS;
        //}

        public RecVerifica ExisteArchivo(string archivo)
        {
            RecepcionIDEWEB.Negocio.AccesoDatos.Datos ARCH = new RecepcionIDEWEB.Negocio.AccesoDatos.Datos();
            RecVerifica exi = new RecVerifica();
            exi = ARCH.VerificarExistArch(archivo);
            return exi;
        }

        public List<Mensaje> GetMensajes(int idTipoMensaje)
        {

            Negocio.AccesoDatos.Datos ARCH = new Negocio.AccesoDatos.Datos();
            List<Mensaje> msg = new List<Mensaje>();
            msg = ARCH.GetMensajes(idTipoMensaje);
            return msg;
        }

        public long SiDesEncripta(string rutaArch, string archOrig)
        {

            Negocio.Comun.Desencripta ARCH = new Negocio.Comun.Desencripta();

            return ARCH.ProcessWithTempFiles(rutaArch, archOrig);

        }

    }
}
