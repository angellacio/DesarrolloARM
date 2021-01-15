using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace ProDec_ReglaNegocio.Proceso
{
    public static class ProcesaDeclaracion
    {
        static string _sRutaFSH;

        private static int nMaxRows
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["MaxNumRenglones"].ToString().Trim());
            }
        }
        private static string sRutaFSH
        {
            get
            {
                if (_sRutaFSH == null || _sRutaFSH == "") _sRutaFSH = ConectBD.Parametros.RutaLocal;

                return _sRutaFSH;
            }
        }

        //METODO RESERVA DECLARACIONES
        public static void ReserveDeclas(int MaxNumIntentos, String nombreEquipo)
        {
            ConectBD.connectBD accessoBD = null;
            List<Entidades.catConsultaDeclas> CatDeclasReserv = null;
            List<Entidades.catParametros> ParamSPReservDeclas = null;

            try
            {
                ParamSPReservDeclas = new List<Entidades.catParametros>()
                    {
                        new Entidades.catParametros() { sNombre = "Apartados", sqlTipo = System.Data.SqlDbType.Int, oDato = nMaxRows },
                        new Entidades.catParametros() { sNombre = "NumInt", sqlTipo = System.Data.SqlDbType.Int, oDato = MaxNumIntentos },
                        new Entidades.catParametros() { sNombre = "Equipo", sqlTipo = System.Data.SqlDbType.VarChar, oDato = nombreEquipo }
                    };

                accessoBD = new ConectBD.connectBD(ConectBD.connectBD.sTipConect.IDE_WEB);
                CatDeclasReserv = accessoBD.ConsultaReservaDeclas("spreservadeposito", ParamSPReservDeclas);

                ManejoErrores.MensajeAuditoria(string.Format("ProcesaDeclaracion.ReserveDeclas Declaraciones a procesar: {0}.", CatDeclasReserv.Count));
                CatDeclasReserv.ForEach(item =>
                {
                    ManejoErrores.MensajeAuditoria(string.Format("ProcesaDeclaracion.ReserveDeclas Folio: {0}. MedioRecepcion: {1}. NombreArchivo: {2}. NombreFisico: {3}. ", item.nFolio, item.nMedioRecepcion, item.sNombreArchivo, item.sNombreFisico));
                    ValidaDeclaracion(item.nFolio, item.nMedioRecepcion, item.sNombreArchivo, item.sNombreFisico);
                });
            }
            catch (Exception ex)
            {
                ManejoErrores.MensajeError("Error . . . ", ex);
            }
        } // FIN METODO ReserveDeclas

        // METODO Valida Declaracion
        public static void ValidaDeclaracion(int nFolio, int nMedioRecepcion, string sArchivo, string sArchivoEncript)
        {
            IdeValidacion.ValidacionClient validaDecalaracion = null;
            try
            { 
                string sArcExtencion = Path.GetExtension(sArchivoEncript);
                string sArchivoFileShare = string.Format(@"{0}\{1}", sRutaFSH, sArchivoEncript.Replace(sArcExtencion, ".dec"));

                Boolean bolExisteArchivo = false;
                try
                {
                    bolExisteArchivo = File.Exists(sArchivoFileShare);
                    if (bolExisteArchivo)
                        if (nMedioRecepcion == 3) EncriptDecriptArchivo.DesencriptaDeclaracion(sArchivo, sArchivoEncript, sRutaFSH);

                    if (!bolExisteArchivo)
                        throw new ApplicationException(string.Format("El archivo {0} no se encontro en el FileShare {1}.", sArchivoEncript.Replace(sArcExtencion, ".dec"), sRutaFSH)); 
                }
                catch (Exception ex)
                {
                    ActualizaSegimeiento(nFolio, 1, ex.Message, 1);
                }
                finally
                { }

                validaDecalaracion = new IdeValidacion.ValidacionClient();
                validaDecalaracion.iniciarValidacion(nFolio);

                ActualizaSegimeiento(nFolio, 0, "", 0);
            }
            catch (ApplicationException ex)
            {
                ManejoErrores.MensajeWarning(string.Format("ProcesaDeclaracion.ProcesaDeclaracion nFolio:{0}, nMedioRecepcion:{1} . . . ", nFolio, nMedioRecepcion), ex);
                ActualizaSegimeiento(nFolio, 1, ex.Message, 0);
            }
            catch (Exception ex)
            {
                ManejoErrores.MensajeError(string.Format("ProcesaDeclaracion.ProcesaDeclaracion nFolio:{0}, nMedioRecepcion:{1} . . . ", nFolio, nMedioRecepcion), ex);
                ActualizaSegimeiento(nFolio, 1, ex.Message, 0);
            } 
        } // FIN METODO

        private static void ActualizaSegimeiento(int nFolio, int nReultado, string sMensaje, int nProceso)
        {
            ConectBD.connectBD actualizaFlijo = null;
            int nMaxProceso = 0;
            try
            {
                nMaxProceso = int.Parse(ConfigurationManager.AppSettings["MaxNumIntentos"].ToString().Trim());

                actualizaFlijo = new ConectBD.connectBD(ConectBD.connectBD.sTipConect.IDE_WEB);
                actualizaFlijo.ProcesoValidacion("spactualizadeposito", new List<Entidades.catParametros>()
                { 
                    new Entidades.catParametros() { sNombre = "@Folio", oDato = nFolio, sqlTipo =  System.Data.SqlDbType.Int },
                    new Entidades.catParametros() { sNombre = "@Resultado", oDato = nReultado, sqlTipo =  System.Data.SqlDbType.Int }, 
                    new Entidades.catParametros() { sNombre = "@Numint", oDato = nMaxProceso, sqlTipo =  System.Data.SqlDbType.Int }, 
                    new Entidades.catParametros() { sNombre = "@Menopera", oDato = sMensaje, sqlTipo =  System.Data.SqlDbType.NVarChar }, 
                    new Entidades.catParametros() { sNombre = "@Proceso", oDato = nProceso, sqlTipo =  System.Data.SqlDbType.Int } 
                });
            }
            catch (Exception ex)
            {
                ManejoErrores.MensajeError(string.Format("ProcesaDeclaracion.ProcesaDeclaracion nFolio:{0} . . . ", nFolio), ex);
            }
            finally
            { }
        }
    }
}
