using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using Sat.Scade.Net.IDE.Presentacion.Web;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using SAT.DyP.Util.Service.WCF;
using Sat.Scade.Net.IDE.Presentacion.Proxy;
using System.Globalization;
using System.Runtime.Serialization;
using Sat.Scade.Net.IDE.Presentacion.Web.Comun;
//using CriptoNet = mx.gob.sat.sgi.SgiCripto;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public partial class Default : PaginaBase
    {
        private IDCMembershipUser usuario;

        private string Applet_strRFC { get; set; }
        private string Applet_archivoDec { get; set; }
        private string Applet_extOrigen { get; set; }
        private string Applet_md5Recibido { get; set; }
        private string Applet_nSerial { get; set; }
        private double Applet_nTamArchivo { get; set; }

        private double Temp_nTamArchivo { get; set; }
        private string rutaArchivoEncrip { get; set; }
        private double FS_nTamArchivo { get; set; }

        private Boolean bolValTamArchivos
        {
            get
            {
                Boolean result = false;

                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.bolValTamArchivos Applet_nTamArchivo: {0}. Temp_nTamArchivo: {1}. FS_nTamArchivo: {2}", Applet_nTamArchivo, Temp_nTamArchivo, FS_nTamArchivo));

                if (Applet_nTamArchivo == Temp_nTamArchivo)
                    if (Temp_nTamArchivo == FS_nTamArchivo)
                        result = true;

                return result;
            }
        }

        private string mensaje { get; set; }
        private string md5Base64 { get; set; }

        private string rutaArchivo { get; set; }
        

        private string strArchivoOriginal;

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

        private ServiceRecepcion.RecSalida rs { get; set; }

        private Boolean CrearArchivoApplet()
        {
            byte[] buff, hash;
            Boolean bCreaArchivo = false;
            FileStream fs = null;
            BinaryWriter bw = null;
            bool md5FinalBlock = false;
            int leidos = 1;
            //int PIEZA = 1024 * 10;
            //int PIEZA = 65536;
            int PIEZA = 64000;
            MD5 md5;
            try
            {
                Applet_md5Recibido = Applet_md5Recibido.Replace(' ', '+');
                rutaArchivoEncrip = Environment.GetEnvironmentVariable("temp") + "\\" + Applet_archivoDec;

                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.CrearArchivoApplet archivo: {0}, inicio.", rutaArchivoEncrip));

                md5 = MD5.Create();
                buff = new byte[PIEZA];
                using (fs = new FileStream(@rutaArchivoEncrip, FileMode.Create))
                {
                    using (bw = new BinaryWriter(fs))
                    {
                        md5FinalBlock = false;
                        leidos = 1;
                        while (leidos > 0)
                        {
                            buff = new byte[PIEZA];
                            leidos = Request.InputStream.Read(buff, 0, PIEZA);
                            if (leidos > 0)
                            {
                                bw.Write(buff, 0, leidos);
                                if (leidos >= PIEZA)
                                    md5.TransformBlock(buff, 0, buff.Length, buff, 0);
                                else
                                {
                                    md5.TransformFinalBlock(buff, 0, leidos);      //para el último bloque del segmento...
                                    md5FinalBlock = true;
                                }
                                System.Threading.Thread.Sleep(20);
                            }
                            else
                            {
                                if (!md5FinalBlock)
                                {
                                    md5.TransformFinalBlock(buff, 0, leidos);      //para el último bloque del segmento...
                                    md5FinalBlock = true;
                                }
                            }
                        }

                        hash = md5.Hash;
                        md5Base64 = Convert.ToBase64String(hash);
                        Temp_nTamArchivo = double.Parse(fs.Length.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogEventos("Error al crear el archivo obtenido del applet.", ex);
                throw ex;
            }
            finally
            {
                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.CrearArchivoApplet archivo: {0}, termino. {1}", rutaArchivoEncrip, Temp_nTamArchivo));
                if (bw != null) bw.Close();

                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            return bCreaArchivo;
        }
        private Boolean bolValidaSession
        {
            get
            {
                Boolean bResult = false;

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion]["FechaRecepcion"] == null || Request.Cookies[Parametros.Sesion]["FechaRecepcion"].Trim() == "") { bResult = true; }
                }
                else
                {
                    if (Session[Parametros.Sesion] == null) { bResult = true; }
                }
                return bResult;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Utilerias.RegistrarLogEventosAuditoria("\r\n Default.Page_Load ** Inicio proceso *** ");
            if (!IsPostBack)
            {
                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.Page_Load bolValidaSession: {3}. {0}: {1}. archivo: {2}", Parametros.Sesion, Session[Parametros.Sesion], Request.QueryString.Get("archivo").Trim(), bolValidaSession));
                if (bolValidaSession)
                {
                    List<string> lParamEntrada = null;
                    try
                    {
                        lParamEntrada = Request.QueryString.Get("archivo").Trim().Split('?').ToList();

                        Applet_archivoDec = lParamEntrada[0].Trim();  // Archivo Applet
                        Applet_strRFC = lParamEntrada[1].Trim(); // RFC Applet
                        Applet_extOrigen = lParamEntrada[2].Trim(); // Extencion Applet
                        Applet_md5Recibido = lParamEntrada[3].Trim(); // hash Applet
                        Applet_nSerial = lParamEntrada[4].Trim(); // Numero de Serie Applet
                        Applet_nTamArchivo = double.Parse(lParamEntrada[5].Trim()); // Numero de Serie Applet

                        strArchivoOriginal = Applet_archivoDec.Replace(".dec", "." + Applet_extOrigen).Replace(".DEC", "." + Applet_extOrigen);

                        Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.Page_Load PARAMETROS strArchivoOriginal: {0}", strArchivoOriginal));

                        this.usuario = base.GetCurrentMembershipUser(Applet_strRFC);

                        Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.Page_Load MEMBERSHIP strArchivoOriginal: {0}", strArchivoOriginal));

                        CrearArchivoApplet();

                        Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.Page_Load ARCHIVO APPLET strArchivoOriginal: {0}", strArchivoOriginal));

                        ValidaArchivoBD();

                        Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.Page_Load MEMBERSHIP strArchivoOriginal: {0}", strArchivoOriginal));
                        lblPOST.Text = string.Format("Request.Length {0} Escritos: {1} Hash: {2} --- {3} {4} ", this.Request.ContentLength.ToString(), Temp_nTamArchivo, md5Base64, md5Base64.Length, mensaje);

                        TransferirArchivos();

                        Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.Page_Load TRANSFIERE ARCHIVO strArchivoOriginal: {0}", strArchivoOriginal));

                        ConstruirMensajeAceptacion(rs);

                        Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.Page_Load RESPUESTA strArchivoOriginal: {0}", strArchivoOriginal));

                    }
                    catch (ApplicationException ex)
                    {
                        ConstruirAcuseRechazo(ex.Message);
                        if (rutaArchivo != "") if (File.Exists(rutaArchivo)) File.Delete(rutaArchivo);
                        Utilerias.RegistrarLogEventos("ApplicationException", ex);
                    }
                    catch (Exception ex)
                    {
                        Utilerias.RegistrarLogEventos("Exception", ex);
                        ConstruirAcuseRechazo(Mensajes.ServicioNoDisponible);
                        if (rutaArchivo != "") if (File.Exists(rutaArchivo)) File.Delete(rutaArchivo);
                    }
                    finally
                    {
                        string[] txtList = Directory.GetFiles(Environment.GetEnvironmentVariable("temp") + "\\", "*.dec");
                        foreach (string f in txtList)
                        {
                            File.Delete(f);
                        }
                    }
                }
                //CargaApplet enviarEncriptaButton = new CargaApplet();
                //enviarEncriptaButton.Visible = true;
            }
            Utilerias.RegistrarLogEventosAuditoria("\r\n Default.Page_Load ** Termino proceso *** ");
            Response.Redirect(Urls.NotificacionEncrip, true);
        }

        private DocumentRequest ConstruirSolicitud()
        {
            DocumentRequest solicitud = new DocumentRequest();
            solicitud.MedioPresentacion = (int)Parametros.MedioPresentacion;
            solicitud.MensajeID = Guid.NewGuid().ToString();
            solicitud.RfcAutenticacion = this.usuario.RFC;
            solicitud.RfcContribuyente = String.Empty;
            solicitud.NombreArchivo = strArchivoOriginal;
            solicitud.TamañoArchivo = (int)FS_nTamArchivo;
            solicitud.FechaPresentacion = DateTime.Now;
            solicitud.DireccionIP = HttpContext.Current.Request.UserHostAddress;
            solicitud.NumeroSerie = this.usuario.TieneFIEL.ToString();
            solicitud.RutaArchivo = rutaArchivo;
            solicitud.Contenido = String.Empty;

            return solicitud;
        }

        private void ConstruirAcuseRechazo(string mensaje)
        {
            if (Parametros.UsarCookies)
            {
                Response.Cookies[Parametros.Sesion]["Estatus"] = "0";
                Response.Cookies[Parametros.Sesion]["MedioPresentacion"] = ((int)Parametros.MedioPresentacion).ToString();
                Response.Cookies[Parametros.Sesion]["RfcAutenticacion"] = Server.UrlEncode(this.usuario.RFC);
                Response.Cookies[Parametros.Sesion]["Mensaje"] = Server.UrlEncode(mensaje);
                Response.Cookies[Parametros.Sesion]["FechaRecepcion"] = FormatearFecha(DateTime.Now);
                Response.Cookies[Parametros.Sesion]["NombreArchivo"] = Server.UrlEncode(strArchivoOriginal);
            }
            else
            {
                MensajeAcuse acuse = new MensajeAcuse();
                acuse.Estatus = 0;
                acuse.MedioPresentacion = (int)Parametros.MedioPresentacion;
                acuse.RfcAutenticacion = this.usuario.RFC;
                acuse.Mensaje = mensaje;
                acuse.FechaRecepcion = FormatearFecha(DateTime.Now);
                acuse.NombreArchivo = strArchivoOriginal;

                Session[Parametros.Sesion] = acuse;

                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.ConstruirAcuseRechazo {0}: {1}. ", Parametros.Sesion, Session[Parametros.Sesion]));
            }
        }

        private void ConstruirAcuseAceptacion(DocumentRequest solicitud, DocumentResponse respuesta)
        {
            if (Parametros.UsarCookies)
            {
                Response.Cookies[Parametros.Sesion]["MedioPresentacion"] = solicitud.MedioPresentacion.ToString();
                Response.Cookies[Parametros.Sesion]["Estatus"] = respuesta.Estatus.ToString();
                Response.Cookies[Parametros.Sesion]["RfcContribuyente"] = Server.UrlEncode(respuesta.RfcContribuyente);
                Response.Cookies[Parametros.Sesion]["RfcAutenticacion"] = Server.UrlEncode(respuesta.RfcAutenticacion);
                Response.Cookies[Parametros.Sesion]["NombreArchivo"] = Server.UrlEncode(solicitud.NombreArchivo);
                Response.Cookies[Parametros.Sesion]["TamanoArchivo"] = solicitud.TamañoArchivo.ToString();
                Response.Cookies[Parametros.Sesion]["FechaRecepcion"] = respuesta.FechaRecepcion.ToString(Parametros.FormatoFecha, Parametros.Cultura).ToUpper();
                Response.Cookies[Parametros.Sesion]["HoraRecepcion"] = respuesta.FechaRecepcion.ToString(Parametros.FormatoHora, Parametros.Cultura);
                Response.Cookies[Parametros.Sesion]["Folio"] = respuesta.Folio;
                Response.Cookies[Parametros.Sesion]["Mensaje"] = Server.UrlEncode(respuesta.Mensaje);
            }
            else
            {
                MensajeAcuse acuse = new MensajeAcuse();
                acuse.MedioPresentacion = solicitud.MedioPresentacion;
                acuse.Estatus = respuesta.Estatus;
                acuse.RfcContribuyente = respuesta.RfcContribuyente;
                acuse.RfcAutenticacion = respuesta.RfcAutenticacion;
                acuse.NombreArchivo = solicitud.NombreArchivo;
                acuse.TamañoArchivo = solicitud.TamañoArchivo;
                acuse.FechaRecepcion = respuesta.FechaRecepcion.ToString(Parametros.FormatoFecha, Parametros.Cultura).ToUpper();
                acuse.HoraRecepcion = respuesta.FechaRecepcion.ToString(Parametros.FormatoHora, Parametros.Cultura);
                acuse.Folio = respuesta.Folio;
                acuse.Mensaje = respuesta.Mensaje;
                Session[Parametros.Sesion] = acuse;

                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Default.ConstruirAcuseAceptacion {0}: {1}. ", Parametros.Sesion, Session[Parametros.Sesion]));
            }
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

        //private void TransferirArchivo()
        //{
        //    BigFilesServiceProxy proxy = null;
        //    TransferResponse respuesta = null;

        //    try
        //    {
        //        proxy = new BigFilesServiceProxy();
        //        //respuesta = proxy.Send(CargaFileUpload.PostedFile.InputStream, this.RutaRemota, Parametros.DataBaseKeyConfig, Parametros.StoreProcedureName, Parametros.StoreProcedureParameterName, Mensajes.ErrorMessage);
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
        //                DeshacerTransferirArchivo();
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

        //private void DeshacerTransferirArchivo()
        //{
        //    BigFilesServiceProxy proxy = null;

        //    try
        //    {
        //        proxy = new BigFilesServiceProxy();
        //        //proxy.UndoSend(this.RutaRemota);
        //    }
        //    catch (Exception excepcion)
        //    {
        //        Utilerias.RegistrarLogEventos(Mensajes.ServicioTransferenciaNoDisponible, excepcion);
        //    }
        //    finally
        //    {
        //        if (proxy != null)
        //        {
        //            proxy.Dispose();
        //            proxy = null;
        //        }
        //    }
        //}

        private string FormatearFecha(DateTime fecha)
        {
            return fecha.ToString(Parametros.FormatoFecha, Parametros.Cultura).ToUpper();
        }

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

            if (rv == null)
            {
                throw new ApplicationException(Mensajes.ArchivoExistenteSF);
            }

            if (rv.Existencia == 1)
            {
                throw new ApplicationException(string.Format(Mensajes.ArchivoExistenteCF, rv.Folio));
            }
        }

        private void TransferirArchivoBD()
        {
            string extension = "";
            string rfc = "";
            bool esNormal = false;
            bool esAnual = false;
            ServiceRecepcion.RecepcionClient sr = null;
            ServiceRecepcion.RecEntrada re = null;


            re = new ServiceRecepcion.RecEntrada();

            extension = strArchivoOriginal.Substring(strArchivoOriginal.Length - 3, 3);
            esNormal = (strArchivoOriginal.Substring(21, 1)).Equals("N") ? true : false;
            esAnual = (strArchivoOriginal.Substring(0, 3)).Equals("DIA") ? true : false;
            rfc = strArchivoOriginal.Substring(3, 12);

            re.DireccionIP = HttpContext.Current.Request.UserHostAddress;
            re.EsAnual = esAnual;
            re.EsNormal = esNormal;
            re.Formato = extension;
            re.IdEntidadReceptora = int.Parse(Parametros.EntidadReceptora);
            re.IdMedioRecepcion = (int)Parametros.MedioPresentacion;
            re.Materia = int.Parse(Parametros.IdMateria);
            re.NombreArchivo = strArchivoOriginal;
            re.RfcAutenticacion = this.usuario.RFC;
            re.RfcContribuyente = re.IdMedioRecepcion == int.Parse(Parametros.IdInternet) ? re.RfcAutenticacion : rfc;
            re.TamañoArchivo = (int)FS_nTamArchivo;
            re.ArchivoFisico = this.ArhivoUID.Replace(".dec", "." + extension).Replace(".DEC", "." + extension);

            sr = new ServiceRecepcion.RecepcionClient();
            rs = new ServiceRecepcion.RecSalida();

            Utilerias.RegistrarLogEventosAuditoria(string.Format("ServiceRecepcion.RegistrarArchivo DireccionIP {0}. EsAnual: {1}. EsNormal: {2}. Formato: {3}. IdEntidadReceptora: {4}. IdMedioRecepcion: {5}. Materia: {6}. NombreArchivo: {7}. RfcAutenticacion: {8}. RfcContribuyente: {9}. TamañoArchivo: {10}. ArchivoFisico: {11}. ",
                re.DireccionIP, re.EsAnual, re.EsNormal, re.Formato, re.IdEntidadReceptora, re.IdMedioRecepcion, re.Materia, re.NombreArchivo, re.RfcAutenticacion, re.RfcContribuyente, re.TamañoArchivo, re.ArchivoFisico));

            rs = sr.RegistrarArchivo(re);

            Utilerias.RegistrarLogEventosAuditoria(string.Format("ServiceRecepcion.RegistrarArchivo DireccionIP {0}. EsAnual: {1}. EsNormal: {2}. Formato: {3}. IdEntidadReceptora: {4}. IdMedioRecepcion: {5}. Materia: {6}. NombreArchivo: {7}. RfcAutenticacion: {8}. RfcContribuyente: {9}. TamañoArchivo: {10}. ArchivoFisico: {11}. " +
                "ResTamano: {12}. ResRfc: {13}. ResNombre: {14}. ResFolio: {15}. ResFecha: {16}. ResExtensionData: {17}. ",
                re.DireccionIP, re.EsAnual, re.EsNormal, re.Formato, re.IdEntidadReceptora, re.IdMedioRecepcion, re.Materia, re.NombreArchivo, re.RfcAutenticacion, re.RfcContribuyente, re.TamañoArchivo, re.ArchivoFisico,
                rs.Tamano, rs.Rfc, rs.Nombre, rs.Folio, rs.Fecha, rs.ExtensionData));

            if (rs != null)
            {
                if (rs.Folio == 0) { throw new ApplicationException(Mensajes.ServicioNoDisponible); }
            }
            else { throw new ApplicationException(Mensajes.ServicioNoDisponible); }
        }

        //private bool DesencriptaArchivo()
        //{
        //    ServiceRecepcion.RecepcionClient sr = null;
        //    sr = new ServiceRecepcion.RecepcionClient();

        //    //ProcessWithTempFiles();

        //    Utilerias.RegistrarLogEventosAuditoria(string.Format("ServiceRecepcion.SiDesEncripta RutaArchivo {0}. archOrigen: {1}. ", rutaArchivo, strArchivoOriginal));

        //    FinalChecker = sr.SiDesEncripta(rutaArchivo, strArchivoOriginal);

        //    Utilerias.RegistrarLogEventosAuditoria(string.Format("ServiceRecepcion.SiDesEncripta RutaArchivo {0}. archOrigen: {1}. result: {2}.", rutaArchivo, strArchivoOriginal, FinalChecker));

        //    if (FinalChecker == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //    //throw new NotImplementedException();
        //}

        private void TransferirArchivos()
        {

            BigFilesServiceProxy proxy = null;
            TransferResponse trArchivoEncriptado = null;
            FileStream s = null;

            rutaArchivo = RutaRemota;

            try
            {
                proxy = new BigFilesServiceProxy();

                s = new FileStream(rutaArchivoEncrip, FileMode.Open, FileAccess.Read);
                trArchivoEncriptado = proxy.Send(s, rutaArchivo, Parametros.DataBaseKeyConfig, Parametros.StoreProcedureName, Parametros.StoreProcedureParameterName, Mensajes.ArchivoExistenteCF);
                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n BigFilesServiceProxy.Send Archivo {0}. Status 1: {1}. Message 1: {2}. ", rutaArchivo, trArchivoEncriptado.Status, trArchivoEncriptado.Message));
                FS_nTamArchivo = s.Length;


                if (trArchivoEncriptado != null)
                {
                    if (bolValTamArchivos)
                    {
                        if (trArchivoEncriptado.Status == TransferStatus.Downloaded) { TransferirArchivoBD(); }
                        else if (trArchivoEncriptado.Status == TransferStatus.InProgress) { throw new ApplicationException(trArchivoEncriptado.Message); }
                        else if (trArchivoEncriptado.Status == TransferStatus.Uploaded) { throw new ApplicationException(trArchivoEncriptado.Message); }
                        else
                        {
                            Utilerias.RegistrarLogEventos("El servicio de transferencia envio el siguiente mensaje de error: " + trArchivoEncriptado.Message);
                            throw new ApplicationException(Mensajes.ServicioNoDisponible);
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Error al transferir los archivos, favor de reintentar.");
                    }
                }
                else
                {
                    throw new ApplicationException("Error al transferir los archivos, favor de reintentar.");
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
                if (s != null)
                {
                    s.Close();
                    s.Dispose();
                    s = null;
                }
            }
        }
    }


}
