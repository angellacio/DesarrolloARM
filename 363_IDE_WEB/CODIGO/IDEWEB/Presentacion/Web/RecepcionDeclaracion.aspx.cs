using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sat.Scade.Net.IDE.Presentacion.Web;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using SAT.DyP.Util.Service.WCF;
using Sat.Scade.Net.IDE.Presentacion.Proxy;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization;
using System.Configuration;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.Web.WebServiceConfiguracion;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public partial class RecepcionDeclaracion : PaginaBase
    {
        private IDCMembershipUser usuario;
        private string strArchivoOriginal { get; set; }
        ServiceRecepcion.RecSalida rs { get; set; }
        private string _GeneraUID;
        private string GeneraUID
        {
            get
            {
                if (_GeneraUID == "" || _GeneraUID == null)
                {
                    Guid g;
                    g = Guid.NewGuid();
                    _GeneraUID = g.ToString();
                }
                return _GeneraUID;
            }
        }
        private string ArhivoUID { get; set; }
        private string RutaRemota
        {
            get
            {
                string sRuta = "", nombreArchivo = "";

                nombreArchivo = string.Format("{0}.dec", strArchivoOriginal.Substring(0, strArchivoOriginal.IndexOf(".")));

                this.ArhivoUID = this.GeneraUID + nombreArchivo.Substring(nombreArchivo.Length - 4, 4);

                sRuta = Path.Combine(Parametros.RutaLocal, ArhivoUID);

                return sRuta;
            }
        }

        private int Cook_MedioPresentacion
        {
            get
            {
                int result = -1;

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["MedioPresentacion"] != null)
                        result = int.Parse(Request.Cookies[Parametros.Sesion]["MedioPresentacion"].Trim());
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).MedioPresentacion;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["MedioPresentacion"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.MedioPresentacion = value;
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private int Cook_Estatus
        {
            get
            {
                int result = -1;

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["Estatus"] != null)
                        result = int.Parse(Request.Cookies[Parametros.Sesion]["Estatus"].Trim());
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).Estatus;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["Estatus"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.Estatus = value;
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_RfcContribuyente
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["RfcContribuyente"] != null)
                        result = Request.Cookies[Parametros.Sesion]["RfcContribuyente"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).RfcContribuyente;
                }
                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["RfcContribuyente"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.RfcContribuyente = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_RfcAutenticacion
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["RfcAutenticacion"] != null)
                        result = Request.Cookies[Parametros.Sesion]["RfcAutenticacion"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).RfcAutenticacion;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["RfcAutenticacion"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.RfcAutenticacion = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_NombreArchivo
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["NombreArchivo"] != null)
                        result = Request.Cookies[Parametros.Sesion]["NombreArchivo"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).NombreArchivo;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["NombreArchivo"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.NombreArchivo = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private int Cook_TamanoArchivo
        {
            get
            {
                int result = -1;

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["TamanoArchivo"] != null)
                        result = int.Parse(Request.Cookies[Parametros.Sesion]["TamanoArchivo"].Trim());
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).TamañoArchivo;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["TamanoArchivo"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.TamañoArchivo = value;
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_FechaRecepcion
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["FechaRecepcion"] != null)
                        result = Request.Cookies[Parametros.Sesion]["FechaRecepcion"];
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).FechaRecepcion;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["FechaRecepcion"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.FechaRecepcion = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_HoraRecepcion
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["HoraRecepcion"] != null)
                        result = Request.Cookies[Parametros.Sesion]["HoraRecepcion"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).HoraRecepcion;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["HoraRecepcion"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.HoraRecepcion = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_Folio
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["Folio"] != null)
                        result = Request.Cookies[Parametros.Sesion]["Folio"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).Folio;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["Folio"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.Folio = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_Mensaje
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["Mensaje"] != null)
                        result = Request.Cookies[Parametros.Sesion]["Mensaje"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).Mensaje;
                }

                return Server.UrlDecode(result);
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["Mensaje"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.Mensaje = Server.UrlEncode(value.ToString().Trim());
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.usuario = base.GetCurrentMembershipUser();

            Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n RecepcionDeclaracion.Page_Load {0}: {1}. ", Parametros.Sesion, Cook_Mensaje));

            if (!Page.IsPostBack)
            {
                rfcLabel.Text = usuario.RFC;
                razonSocialLabel.Text = usuario.RazonSocial;
                Session[Parametros.Sesion] = null;
                Response.Cookies[Parametros.Sesion]["Folio"] = null;
            }
            enviarButton.Attributes.Add("onclick", " this.disabled = false; " + Page.GetPostBackEventReference(enviarButton) + ";");
        }

        protected void EnviarDeclaracion_Click(object sender, EventArgs e)
        {
             string strValores = "";
            try
            {
                strValores = HValidacion.Value;
                strArchivoOriginal = CargaFileUpload.FileName;

                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n RecepcionDeclaracion.EnviarDeclaracion_Click strArchivoOriginal: {0}. ", strArchivoOriginal));
                
                DatosValidos(strValores);

                TransferirArchivos();

                ConstruirMensajeAceptacion(rs);

                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n RecepcionDeclaracion.EnviarDeclaracion_Click {0}: {1}. ", Parametros.Sesion, Cook_FechaRecepcion));
            }
            catch (ApplicationException ex)
            {
                ConstruirAcuseRechazo(ex.Message);
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogEventos(ex);
                ConstruirAcuseRechazo(Mensajes.ServicioNoDisponible);
            }
            finally
            {
                if (CargaFileUpload.PostedFile != null && CargaFileUpload.PostedFile.InputStream != null)
                {
                    CargaFileUpload.PostedFile.InputStream.Close();
                    CargaFileUpload.PostedFile.InputStream.Dispose();
                }
            }

            Response.Redirect(Urls.Notificacion, false);
        }

        private DocumentRequest ConstruirSolicitud()
        {
            DocumentRequest solicitud = new DocumentRequest();
            solicitud.MedioPresentacion = (int)Parametros.MedioPresentacion;
            solicitud.MensajeID = Guid.NewGuid().ToString();
            solicitud.RfcAutenticacion = this.usuario.RFC;
            solicitud.RfcContribuyente = String.Empty;
            solicitud.NombreArchivo = CargaFileUpload.FileName;
            solicitud.TamañoArchivo = CargaFileUpload.PostedFile.ContentLength;
            solicitud.FechaPresentacion = DateTime.Now;
            solicitud.DireccionIP = HttpContext.Current.Request.UserHostAddress;
            solicitud.NumeroSerie = this.usuario.TieneFIEL.ToString();
            solicitud.RutaArchivo = this.RutaRemota;
            solicitud.Contenido = String.Empty;

            return solicitud;
        }

        private void ConstruirAcuseRechazo(string mensaje)
        {
            Cook_Estatus = 0;
            Cook_MedioPresentacion = (int)Parametros.MedioPresentacion;
            Cook_RfcAutenticacion = Server.UrlEncode(this.usuario.RFC);
            Cook_Mensaje = mensaje == null ? "" : mensaje;
            Cook_FechaRecepcion = FormatearFecha(DateTime.Now);
            Cook_NombreArchivo = Server.UrlEncode(CargaFileUpload.FileName);
        }

        private void ConstruirAcuseAceptacion(DocumentRequest solicitud, DocumentResponse respuesta)
        {
            Cook_MedioPresentacion = solicitud.MedioPresentacion;
            Cook_Estatus = respuesta.Estatus;
            Cook_RfcContribuyente = Server.UrlEncode(respuesta.RfcContribuyente);
            Cook_RfcAutenticacion = Server.UrlEncode(respuesta.RfcAutenticacion);
            Cook_NombreArchivo = Server.UrlEncode(solicitud.NombreArchivo);
            Cook_TamanoArchivo = solicitud.TamañoArchivo;
            Cook_FechaRecepcion = respuesta.FechaRecepcion.ToString(Parametros.FormatoFecha, Parametros.Cultura).ToUpper();
            Cook_HoraRecepcion = respuesta.FechaRecepcion.ToString(Parametros.FormatoHora, Parametros.Cultura);
            Cook_Folio = respuesta.Folio;
            Cook_Mensaje = respuesta.Mensaje == null ? "" : respuesta.Mensaje;
        }

        //private bool ArchivoValido(string strValores)
        //{
        //    if (!strValores.Equals(""))
        //    {
        //        string[] arrValores = strValores.Split('|');
        //        int indice = 0;
        //        string strMensaje = "";
        //        ServiceRecepcion.Mensaje[] listMensaje = GetMensajes();

        //        if (listMensaje != null)
        //        {
        //            strMensaje += "<ul>";
        //            foreach (string valor in arrValores)
        //            {
        //                if (valor != null && !valor.Equals(""))
        //                {
        //                    indice = int.Parse(valor);
        //                    for (int i = 0; i < listMensaje.Length; i++)
        //                    {
        //                        if (indice == listMensaje[i].IdMensaje)
        //                        {
        //                            strMensaje += "<li> " + listMensaje[i].Descripcion + "</li> ";
        //                        }
        //                    }
        //                }
        //            }
        //            strMensaje += "</ul> ";
        //            ConstruirAcuseRechazo(strMensaje);
        //        }
        //        return false;
        //    }

        //    return true;
        //}

        private void DatosValidos(string strValores)
        {
            if (String.IsNullOrEmpty(CargaFileUpload.PostedFile.FileName)) throw new ApplicationException(Mensajes.RutaRequerida);
            else if (!CargaFileUpload.HasFile) throw new ApplicationException(String.Format(Mensajes.RutaInvalida, CargaFileUpload.FileName));

            if (strValores != null && strValores.Trim() != "")
            {
                string[] arrValores = strValores.Split('|');
                int indice = 0;
                string strMensaje = "";
                ServiceRecepcion.Mensaje[] listMensaje = GetMensajes();

                if (listMensaje != null)
                {
                    foreach (string valor in arrValores)
                    {
                        if (valor != null && valor.Trim() != "")
                        {
                            indice = int.Parse(valor.Trim());
                            for (int i = 0; i < listMensaje.Length; i++)
                            {
                                if (indice == listMensaje[i].IdMensaje)
                                {
                                    strMensaje = string.Format("{0}<li>{1}</li>", strMensaje, listMensaje[i].Descripcion.Trim());
                                }
                            }
                        }
                    }
                    strMensaje = string.Format("<ul>{0}</ul>", strMensaje);

                    throw new ApplicationException(strMensaje);
                }
            }

            ValidaArchivoBD();
        }

        //private void TransferirArchivo()
        //{
        //    BigFilesServiceProxy proxy = null;
        //    TransferResponse respuesta = null;

        //    try
        //    {
        //        proxy = new BigFilesServiceProxy();
        //        respuesta = proxy.Send(CargaFileUpload.PostedFile.InputStream, this.RutaRemota, Parametros.DataBaseKeyConfig, Parametros.StoreProcedureName, Parametros.StoreProcedureParameterName, Mensajes.ArchivoExistenteCF);
        //    }
        //    catch (Exception excepcion)
        //    {
        //        Utilerias.RegistrarLogEventos(Mensajes.ServicioTransferenciaNoDisponible, excepcion);
        //        ConstruirAcuseRechazo(Mensajes.ServicioNoDisponible);
        //    }
        //    finally
        //    {
        //        if (proxy != null)
        //        {
        //            proxy.Dispose();
        //            proxy = null;
        //        }
        //    }

        //    if (respuesta != null)
        //    {
        //        switch (respuesta.Status)
        //        {
        //            case TransferStatus.Downloaded:

        //                //Archivo transferido correctamente.
        //                EnviarSolicitud();
        //                TransferirArchivoBD();
        //                break;

        //            case TransferStatus.InProgress:

        //                //Ya existe un archivo en progreso de transferencia.
        //                ConstruirAcuseRechazo(respuesta.Message);
        //                break;

        //            case TransferStatus.Uploaded:

        //                //Ya existe un registro en base de datos del archivo.
        //                ConstruirAcuseRechazo(respuesta.Message);
        //                break;

        //            case TransferStatus.Unkwond:

        //                //Ocurrio un error en la transferencia del archivo.
        //                Utilerias.RegistrarLogEventos("El servicio de transferencia envio el siguiente mensaje de error: " + respuesta.Message);
        //                ConstruirAcuseRechazo(Mensajes.ServicioNoDisponible);
        //                break;
        //        }
        //    }
        //}

        //private void EnviarSolicitud()
        //{
        //    IDE_RecepcionDeclaracion_RecepcionSolicitudClient proxy = null;
        //    DocumentResponse respuesta = null;
        //    DocumentRequest solicitud = this.ConstruirSolicitud();

        //    try
        //    {
        //        proxy = new IDE_RecepcionDeclaracion_RecepcionSolicitudClient();
        //        respuesta = proxy.ProcesarIDE(solicitud);
        //    }
        //    catch (Exception excepcion)
        //    {
        //        Utilerias.RegistrarLogEventos(Mensajes.ServicioRecepcionNoDisponible, excepcion);
        //        DeshacerTransferirArchivo();
        //        ConstruirAcuseRechazo(Mensajes.ServicioNoDisponible);
        //    }
        //    finally
        //    {
        //        if (proxy != null)
        //        {
        //            proxy.Close();
        //            proxy = null;
        //        }
        //    }

        //    if (respuesta != null)
        //    {
        //        switch (respuesta.Estatus)
        //        {
        //            case 1: //Recepción satisfactoria
        //                ConstruirAcuseAceptacion(solicitud, respuesta);
        //                break;

        //            case 0: //Error de negocio.
        //                //DeshacerTransferirArchivo();
        //                ConstruirAcuseRechazo(respuesta.Mensaje);
        //                break;

        //            default: //Error desconocido.
        //                Utilerias.RegistrarLogEventos(respuesta.Mensaje);
        //                DeshacerTransferirArchivo();
        //                ConstruirAcuseRechazo(Mensajes.ServicioNoDisponible);
        //                break;
        //        }
        //    }
        //}

        private void DeshacerTransferirArchivo()
        {
            BigFilesServiceProxy proxy = null;

            try
            {
                proxy = new BigFilesServiceProxy();
                proxy.UndoSend(this.RutaRemota);
            }
            catch (Exception excepcion)
            {
                Utilerias.RegistrarLogEventos(Mensajes.ServicioTransferenciaNoDisponible, excepcion);
            }
            finally
            {
                if (proxy != null)
                {
                    proxy.Dispose();
                    proxy = null;
                }
            }
        }

        private string FormatearFecha(DateTime fecha)
        {
            return fecha.ToString(Parametros.FormatoFecha, Parametros.Cultura).ToUpper();
        }

        //private string RutaRemota
        //{
        //    get
        //    {
        //        return Path.Combine(Parametros.RutaLocal, generaNombre(CargaFileUpload.FileName));
        //    }
        //}
        //private string ArhivoUID
        //{
        //    get;
        //    set;
        //}
        //private string generaNombre(string nombreArchivo)
        //{
        //    string nombreTemporal = "";
        //    string uid = this.GeneraUID();
        //    int tamanio = nombreArchivo.Length;
        //    string ext = nombreArchivo.Substring(tamanio - 4, 4);
        //    nombreTemporal = uid + ext;
        //    this.ArhivoUID = nombreTemporal;
        //    return nombreTemporal;
        //}

        private void ValidaArchivoBD()
        {
            ServiceRecepcion.RecepcionClient sr = null;
            ServiceRecepcion.RecVerifica rv = null;

            try
            {
                sr = new ServiceRecepcion.RecepcionClient("WSHttpBinding_IRecepcion");
                rv = new ServiceRecepcion.RecVerifica();
                rv = sr.ExisteArchivo(strArchivoOriginal);
                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n ValidaArchivoBD.Send Archivo {0}. ExtensionData: {1}. Folio: {2}. Fecha: {3:dd/MM/yyyy HH:mm}. Existencia: {4}. ", strArchivoOriginal, rv.ExtensionData, rv.Folio, rv.Fecha, rv.Existencia));
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogEventos(Mensajes.ServicioRecepcionNoDisponible, ex);
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }

            if (rv == null) throw new ApplicationException(Mensajes.ArchivoExistenteSF);
            if (rv.Existencia == 1) throw new ApplicationException(string.Format(Mensajes.ArchivoExistenteCF, rv.Folio));
        }

        private void TransferirArchivoBD()
        {
            string extension = "";
            string rfc = "";
            bool esNormal = false;
            bool esAnual = false;
            ServiceRecepcion.RecepcionClient sr = null;
            ServiceRecepcion.RecEntrada re = null;

            string strArchivo = "";
            try
            {
                re = new ServiceRecepcion.RecEntrada();
                strArchivo = CargaFileUpload.FileName;
                extension = strArchivo.Substring(strArchivo.Length - 3, 3);
                esNormal = (strArchivo.Substring(21, 1)).Equals("N") ? true : false;
                esAnual = (strArchivo.Substring(0, 3)).Equals("DIA") ? true : false;
                rfc = strArchivo.Substring(3, 12);

                re.DireccionIP = HttpContext.Current.Request.UserHostAddress;
                re.EsAnual = esAnual;
                re.EsNormal = esNormal;
                re.Formato = extension;
                re.IdEntidadReceptora = int.Parse(Parametros.EntidadReceptora);
                re.IdMedioRecepcion = (int)Parametros.MedioPresentacion;
                re.Materia = int.Parse(Parametros.IdMateria);
                re.NombreArchivo = strArchivo;//nombreArchivo;
                re.RfcAutenticacion = this.usuario.RFC;
                re.RfcContribuyente = re.IdMedioRecepcion == int.Parse(Parametros.IdInternet) ? re.RfcAutenticacion : rfc;
                re.TamañoArchivo = CargaFileUpload.PostedFile.ContentLength;
                re.ArchivoFisico = this.ArhivoUID;

                sr = new ServiceRecepcion.RecepcionClient();
                rs = new ServiceRecepcion.RecSalida();

                Utilerias.RegistrarLogEventosAuditoria(string.Format("Modulo ServiceRecepcion.RegistrarArchivo DireccionIP {0}. EsAnual: {1}. EsNormal: {2}. Formato: {3}. IdEntidadReceptora: {4}. IdMedioRecepcion: {5}. Materia: {6}. NombreArchivo: {7}. RfcAutenticacion: {8}. RfcContribuyente: {9}. TamañoArchivo: {10}. ArchivoFisico: {11}. ",
    re.DireccionIP, re.EsAnual, re.EsNormal, re.Formato, re.IdEntidadReceptora, re.IdMedioRecepcion, re.Materia, re.NombreArchivo, re.RfcAutenticacion, re.RfcContribuyente, re.TamañoArchivo, re.ArchivoFisico));

                rs = sr.RegistrarArchivo(re);

                Utilerias.RegistrarLogEventosAuditoria(string.Format("Modulo ServiceRecepcion.RegistrarArchivo DireccionIP {0}. EsAnual: {1}. EsNormal: {2}. Formato: {3}. IdEntidadReceptora: {4}. IdMedioRecepcion: {5}. Materia: {6}. NombreArchivo: {7}. RfcAutenticacion: {8}. RfcContribuyente: {9}. TamañoArchivo: {10}. ArchivoFisico: {11}. " +
                    "ResTamano: {12}. ResRfc: {13}. ResNombre: {14}. ResFolio: {15}. ResFecha: {16}. ResExtensionData: {17}. ",
                    re.DireccionIP, re.EsAnual, re.EsNormal, re.Formato, re.IdEntidadReceptora, re.IdMedioRecepcion, re.Materia, re.NombreArchivo, re.RfcAutenticacion, re.RfcContribuyente, re.TamañoArchivo, re.ArchivoFisico,
                    rs.Tamano, rs.Rfc, rs.Nombre, rs.Folio, rs.Fecha, rs.ExtensionData));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }

            if (rs == null && rs.Folio == 0) throw new Exception(Mensajes.ServicioNoDisponible);
        }

        private void ConstruirMensajeAceptacion(ServiceRecepcion.RecSalida rs)
        {
            DocumentRequest solicitud = this.ConstruirSolicitud();
            DocumentResponse respuesta = new DocumentResponse();

            respuesta.RfcContribuyente = rs.Rfc;
            respuesta.RfcAutenticacion = rs.Rfc;
            respuesta.Folio = rs.Folio.ToString();
            respuesta.FechaRecepcion = rs.Fecha;
            respuesta.Estatus = 1;
            ConstruirAcuseAceptacion(solicitud, respuesta);
        }

        private void TransferirArchivos()
        {

            BigFilesServiceProxy proxy = null;
            TransferResponse respuesta = null;
            string path = "";

            try
            {
                path = this.RutaRemota;
                proxy = new BigFilesServiceProxy();
                respuesta = proxy.Send(CargaFileUpload.PostedFile.InputStream, this.RutaRemota, Parametros.DataBaseKeyConfig, Parametros.StoreProcedureName, Parametros.StoreProcedureParameterName, Mensajes.ArchivoExistenteCF);

                if (respuesta != null)
                {
                    if (respuesta.Status == TransferStatus.Downloaded) TransferirArchivoBD();
                    else if (respuesta.Status == TransferStatus.InProgress) throw new ApplicationException(respuesta.Message);
                    else if (respuesta.Status == TransferStatus.Uploaded) throw new ApplicationException(respuesta.Message);
                    else if (respuesta.Status == TransferStatus.Unkwond) throw new Exception(string.Format("El servicio de transferencia envio el siguiente mensaje de error: {0}", respuesta.Message));
                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (proxy != null)
                {
                    proxy.Dispose();
                    proxy = null;
                }
            }
        }

        protected void satSesionLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            string direccion = "";
            try
            {
                Configuracion servicioConfiguracion = new Configuracion();
                direccion = servicioConfiguracion.ReadSetting("Sat.Scade.Net.UrlNovellLogout");
            }
            catch (Exception ex)
            {
                Sat.Scade.Net.IDE.Presentacion.Web.Utilerias.RegistrarLogEventos(ex);
                throw ex;
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["AutenticacionNovell"]))
            {
                Session.Abandon();
                HttpContext.Current.Request.Cookies.Clear();
                Response.Redirect(direccion);
            }
            else
            {
                Session.Abandon();
                HttpContext.Current.Request.Cookies.Clear();
                Response.Redirect(@"~\Login.aspx");
            }
        }

        private ServiceRecepcion.Mensaje[] GetMensajes()
        {

            ServiceRecepcion.RecepcionClient sr = null;
            ServiceRecepcion.Mensaje[] listMensaje = null;
            try
            {
                sr = new ServiceRecepcion.RecepcionClient();
                listMensaje = sr.GetMensajes(int.Parse(Parametros.IdTipoMensaje));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }

            return listMensaje;
        }
        //private String GeneraUID()
        //{
        //    Guid g;
        //    g = Guid.NewGuid();
        //    return g.ToString();
        //}
    }
}