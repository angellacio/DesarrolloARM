//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:Utility:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public static class Utility
    {
        public class ToolBoxData
        {
            ///ToolBoxData
            public const string LoginCiec = "<{0}:LoginCiec runat=server FailureAction=\"RedirectToLoginPage\"></{0}:LoginCiec>";
            public const string LoginFiel = "<{0}:LoginFiel runat=server></{0}:LoginFiel>";
            public const string LoginScade = "<{0}:LoginScade runat=server></{0}:LoginScade>";
            public const string Header = "<{0}:Header runat=\"server\"></{0}:Header>";
            public const string Email = "<{0}:Email runat=server></{0}:Email>";
            public const string ButtonBlocked = "<{0}:ButtonBlocked runat=\"server\"></{0}:ButtonBlocked>";
            public const string ButtonEncripta = "<{0}:ButtonEncripta runat=\"server\"></{0}:ButtonEncripta>";
            public const string Encripta = "<{0}:Encripta runat=server></{0}:Encripta>";
            public const string EjecutaEncripta = "<{0}:EjecutaEncripta runat=server FailureAction=\"RedirectToLoginPage\"></{0}:EjecutaEncripta>";
            public const string MuestraEncripta = "<{0}:MuestraEncripta runat=server></{0}:MuestraEncripta>";
            
        }

        public class defaultProperty
        {
            public const string Text = "CiecText";
        }

        public class Resource
        {
            ///Resources Nombre del ensamblado Presentacion.Comun
            public const string ImagenNewLogin =        "Sat.Scade.Net.IDE.Presentacion.Web.Controles.LoginCiec.ImageNewLogin.jpg";
            public const string ImagenFiel =            "Sat.Scade.Net.IDE.Presentacion.Web.Controles.LoginFiel.ImageFiel.jpg";
            public const string ImagenNewHeader =       "Sat.Scade.Net.IDE.Presentacion.Web.Controles.Header.ImageNewHeader.jpg";
            public const string ImagenSat =             "Sat.Scade.Net.IDE.Presentacion.Web.Controles.Header.ImageSat.jpg";
            public const string ImagenSatInfo =         "Sat.Scade.Net.IDE.Presentacion.Web.Controles.Header.ImageSatInfo.jpg";
            public const string ImagenScade =           "Sat.Scade.Net.IDE.Presentacion.Web.Controles.Header.ImageScade.jpg";
            public const string ImagenMailIcono =       "Sat.Scade.Net.IDE.Presentacion.Web.Controles.Email.Mail.bmp";
            public const string ImagenHeaderIcono =     "Sat.Scade.Net.IDE.Presentacion.Web.Controles.Header.Header.bmp";
            public const string ImagenloginCiecIcono =  "Sat.Scade.Net.IDE.Presentacion.Web.Controles.LoginCiec.LoginCiec.bmp";
            public const string ImagenloginFielIcono =  "Sat.Scade.Net.IDE.Presentacion.Web.Controles.LoginFiel.LoginFiel.bmp";
            public const string ImagenloginScadeIcono = "Sat.Scade.Net.IDE.Presentacion.Web.Controles.LoginScade.LoginScade.bmp";
            public const string ButtonSending =         "Sat.Scade.Net.IDE.Presentacion.Web.Controles.ButtonBlocked.DOTUploading.gif";
            public const string RecursosFiel =          "Sat.Scade.Net.IDE.Presentacion.Web.Controles.LoginFiel.RecursosFiel.js";
            public const string RecursosBloqueo =       "Sat.Scade.Net.IDE.Presentacion.Web.Controles.ButtonBlocked.RecursosBloqueo.js";
            public const string RecursosValidacion =    "Sat.Scade.Net.IDE.Presentacion.Web.Controles.ButtonBlocked.RecursosValidacion.js";

            public const string fullType =              "Sat.Scade.Net.IDE.Presentacion.Web";
            public const string assemblyName =          "Sat.Scade.Net.IDE.Presentacion.Web";
            public const string Sat =                   "Sat";
            public const string WebControls =           "Presentacion.Script";
            public const string ScriptBloqueo =         "Presentacion.Script.Bloqueo";
        }

        public class Description
        {
            //Descripciones
            public const string PasswordRequerido = "Mensaje que se mostrara si el password no es vacio.";
            public const string Tema = "Elige el tema que utilizara.";
            public const string MedioPresentacion = "Medio de presentacion que se esta utilizando.";
            public const string UserNameRequiredErrorText = "Error que se mostrara si el nombre de usuario es vacio.";
            public const string UserNameTextLabel = "Texto que sera mostrado como nombre de usuario.";
            public const string PasswordTextLabel = "Texto que sera mostrado como password";
            public const string DisplayMail = "Indica si se mostrara el correo electrónico.";
            public const string Titulo = "Titulo que sera mostrado solo en la pagina del login.";
            public const string TituloRecepcion = "Titulo que sera mostrado en las demas paginas que tengan activado ForzarMedioInternet.";
            public const string ButtonText = "Texto que sera mostrado en el boton de Login";
            public const string MailText = "Texto que sera mostrado en el checkbox de Actualizar Mail.";
            public const string FielTitle = "Texto descriptivo que indica que debe autenticarse por FIEL";
            public const string FielNavigateUrl = "Url donde se encuentra la autenticacion FIEL";
            public const string FielTextUrl = "Texto que se mostrata en el Url de Autenticacion FIEL";
            public const string CiecFNavigateUrl = "Url donde se encuentra la Ciec Fortalecida";
            public const string ChangeLoginType = "Cambia el tipo de login a mostrar";


            public const string CiecFTextUrl = "Texto que mostra el link de Ciec";
            public const string ErrorAutenticacion = "Mensaje que se mostrara si ocurre un error en la autenticacion.";
            public const string VerificaCorreo = "Verifica si se tiene que actualizar el correo electrónico.";

            public const string ForzarInternet = "Invalida el medio de presentacion configurado en el Web.Config y forza a mostrar el control en medio de presentacion Internet (por compatibilidad de Scade I)";
            public const string ModoFielHeader = "Configura el header para que solo muestre la imagen del SAT en el centro.";

            public const string MailPageUrl = "Url donde se encuetra la pagina de actualización de correo electrónico.";
            public const string PasswordTecleado = "Password que tecleo el usuario.";

            //EMail Control
            public const string TitleTextStyleEmail = "Estilo de texto del titulo.";
            public const string LabelTextStyleEmail = "Estilo de texto de las etiquetas.";
            public const string ErrorTextStyleEmail = "Estilo de texto del error.";
            public const string ButtonTextStyleEmail = "Estilo de texto del boton.";    
            public const string FooterTextStyleEmail = "Estilo de texto del pie de pagina.";
            public const string TexBoxTextStyleEmail = "Estilo de texto de los TexBox.";
            public const string TitleEmail = "Texto que se mostrara como titulo.";
            public const string RfcEmail = "Rfc que identifica al usuario.";
            public const string RazonSocialEmail = "Razon Social que identifica al usuario.";
            public const string UserLabelEmail = "Texto que sera mostrado para la etiqueta de usuario.";
            public const string MailLabelEmail = "Texto que sera mostrado para la etiqueta de correo electrónico.";
            public const string ActualEmail = "Correo electrónico Actual.";
            public const string NewEmail = "Correo electronico Nuevo.";
            public const string ContinuosButtonEmail = "Texto que sera mostrado en el boton de Continuar.";
            public const string UpdateButtonEmail = "Texto que sera mostrado en el boton Actualizar.";
            public const string CancelButtonEmail = "Texto que sera mostrado en el boton Cancelar.";
            public const string FooterTextEmail = "Texto que sera mostrado como pie de pagina.";
            public const string FailureEmail = "Mensaje que sera mostrado en caso que el correo electrónico no tenga un formato válido.";
            public const string FailureRequireEmail = "Mensaje que sera mostrado en caso que el correo electrónico sea vacio.";
            public const string FailureReqCaracter = "Caracter que sera colocado en los campos que son requeridos.";
            public const string SuccessfullTextEmail = "Mensaje que sera mostrado en caso que la actualizacion de correo electrónico sea satisfactoria.";
            public const string ExceptionTextEmail = "Mensaje que sera mostrado en caso que la actualizacion de correo electrónico falle.";
            public const string RegExEmail = "Expresion Regular con la que sera validado el correo electrónico.";
            public const string DestinationPageUrlEmail = "Url al que sera enviado el usuario, despues de la actualización y cancelacion del correo electrónico.";
            public const string MembershipProviderEmail =  "Proveedor que se usara para realizar la actualización de correo eletrónico.";
            public const string HasChangedEmail = "Indica si el correo electrónico fue actualizado satisfactoriamente.";
            public const string errorOcurrEmail = "Indica si ha ocurrido un error en la actualizacion de correo electronico.";

        }

        public class Category
        {
            
            public const string EDSApplet = "EDS Applet";
            public const string EDSInformacion = "EDS Informacion";
            public const string EDSFiel = "EDS Fiel";
            public const string EDSCiec = "EDS Ciec";
            public const string EDSHeader = "EDS Header";
            public const string EDSScade = "EDS Scade";
            public const string EDSConfiguracion = "EDS Configuracion";
            public const string EDSEmail = "EDS Email";            
        }

        public class ViewSate
        {
            public const string passwordRequiredErrorTextLogin = "PasswordRequiredErrorTextLogin";
            public const string LoginMode = "ModeLogin";
            public const string ThemeLogin = "ThemeLogin";
            public const string MedioPresentacion = "MedioPresentacion";
            public const string UserNameRequiredErrorTextLogin = "UserNameRequiredErrorTextLogin";
            public const string UserNameLabelLogin = "UserNameLabelLogin";
            public const string PasswordLabelLogin = "PasswordLabelLogin";
            public const string DisplayMailLogin = "DisplayMailLogin";
            public const string TitleLogin = "TituloLogin";
            public const string ButtonTextLogin = "ButtonTextLogin";
            public const string MailTextLogin = "MailTextLogin";
            public const string FielTitleLogin = "FielTitleLogin";
            public const string FielNavigateUrlLogin = "FielNavigateUrlLogin";
            public const string FielTextUrlLogin = "FielTextUrlLogin";
            public const string CiecFNavigateUrlLogin = "CiecFNavigateUrlLogin";
            public const string CiecFTextUrlLogin = "CiecFTextUrlLogin";
            public const string FailureMessageLogin = "FailureMessageLogin";
            public const string MailPageUrl = "MailPageUrl";
            public const string MembershipProviderLogin = "MembershipProviderLogin";

            public const string TitleHeader = "TitleHeader";
            public const string TituloReceptionHeader = "TituloReceptionHeader";
            public const string ThemeHeader = "ThemeHeader";

            public const string MedioInternetHeader = "MedioInternetHeader";
            public const string ModoFielHeader = "ModoFielHeader";
            public const string UrlImageLeftModuloHeader = "UrlImageLeftModuloHeader";
            public const string UrlImageLeftModuloHeaderWidth = "UrlImageLeftModuloHeaderWidth";
            public const string UrlImageLeftModuloHeaderHeight = "UrlImageLeftModuloHeaderHeight";

            public const string UrlImageRightModuloHeader = "UrlImageRightModuloHeader";
            public const string UrlImageRightModuloHeaderWidth = "UrlImageRightModuloHeaderWidth";
            public const string UrlImageRightModuloHeaderHeight = "UrlImageRightModuloHeaderHeight";

            public const string UrlImageInternetHeader = "UrlImageInternetHeader";
            public const string UrlImageInternetHeaderWidth = "UrlImageInternetHeaderWidth";
            public const string UrlImageInternetHeaderHeight = "UrlImageInternetHeaderHeight";

            //Email
            public const string TitleEmail = "TitleEmail";
            public const string ErrorOcurrEmail = "ErrorOcurrEmail";            
            public const string MailChangedHiddenEmail = "MailChangedHiddenEmail";
            public const string MembershipProviderEMail = "MembershipProviderEMail";
            public const string DestinationPageUrlEMail = "DestinationPageUrlEMail";
            public const string RegExEmail = "RegExEmail";
            public const string ExceptionTextEmail = "ExceptionTextEmail";
            public const string SuccesfullTextEmail = "SuccesfullTextEmail";
            public const string FailureRequireCharEmail = "FailureRequireCharEmail";
            public const string FailureRequireTextEmail = "FailureRequireTextEmail";
            public const string FailureTextEmail = "FailureTextEmail";
            public const string FooterTextEmail = "FooterTextEmail";
            public const string CancelButtonTextEmail = "CancelButtonTextEmail";
            public const string RfcTextEmail = "RfcTextEmail";
            public const string RazonSocialTextEmail = "RazonSocialTextEmail";
            public const string UserLabelTextEmail = "UserLabelTextEmail";
            public const string MailLabelTextEmail = "MailLabelTextEmail";
            public const string CurrentEmail = "CurrentEmail";
            public const string NewEmailHidden = "NewEmailHidden";
            public const string ContinuosButtonTextEmail = "ContinuosButtonTextEmail";
            public const string UpdateButtonTextEmail = "UpdateButtonTextEmail";


            
        }

        public class AppSettings
        {
            public const string MedioPresentacion = "MedioPresentacion";
            public const string TitleLogin = "TitleLogin";
            public const string UserNameLogin = "UserNameLogin";
            public const string PasswordLogin = "PasswordLogin";
            public const string UserNameRequiredErrorLogin = "UserNameRequiredErrorLogin";
            public const string PasswordRequiredErrorLogin = "PasswordRequiredErrorLogin";
            public const string FailureTextLogin = "FailureTextLogin";
            public const string ButtonTextLogin = "ButtonLogin";
            public const string TitleHeader = "TitleHeader";
            public const string TituloReceptionHeader = "TituloReceptionHeader";
        }

        public class Message
        {
            public const string llaveNoEncontrada = "No se encontro la llave {0} en el archivo de configuración.";
            public const string LlaveNoConvertible = "No es posible convertir a entero el parametro {0} que se encuentra en el archivo de configuración.";
            public const string ErrorAutenticacion = "Error al autenticarse.";
            public const string ProveedorNoEncontrado = "No se pudo encontrar el proveedor.";
        }

        public class ID
        {
            public const string Title = "Title";
            public const string UserName = "UserName";
            public const string Password = "Password";
            public const string UserNameLabel = "UserNameLabel";
            public const string PasswordLabel = "PasswordLabel";
            public const string UserNameRequired = "UserNameRequired";
            public const string PasswordRequired = "PasswordRequired";
            public const string Loggin = "Loggin";
            public const string FielLinkButton = "FielLinkButton";
            public const string CiecFLink = "CiecFLink";
            public const string CiecLinkButton = "CiecLinkButton";
            public const string Mail = "MailCheckBox";

            // R2. Agregamos la Constante para el Control HyperLink
            public const string MailUpdateLink = "MailUpdateLink";

            public const string FielTitle = "FielTitle";
            public const string FailureText = "FailureText";
            public const string Container = "Container";
            public const string Principal = "Principal";
            public const string IsValidCertificateHiddenField = "IsValidCertificateHiddenField";
            public const string Firma = "Firma";
            public const string RFC = "RFC";
            public const string FielImage = "FielImage";
            public const string Serie ="Serie";
            public const string CadenaO = "CadenaO";
            public const string UrlOriginal = "URLoriginal";
            public const string HiddenType = "hidden";
            public const string ContainerMail = "ContainerMail";


            public const string LeftHeaderImage = "LeftHeaderImage";
            public const string CenterHeaderImage = "CenterHeaderImage";
            public const string RightHeaderImage = "RightHeaderImage";
            public const string TitleHeaderLabel = "TitleHeaderLabel";

            //Email
            public const string CurrentMailTextBoxEmail = "CurrentMailTextBoxEmail";
        }

        public class Text
        {
            public const string Title = "Acceso a los Servicios Electrónicos del SAT";
            public const string TitleMedio = "RECEPCION MEDIO PRESENTACION";
            public const string UserName = "User Name: ";
            public const string Password = "Password: ";
            public const string Required = "*";
            public const string Entrar = "Entrar";
            public const string Aceptar = "Aceptar";
            public const string Fiel = "e.firma";
            public const string Encriptar = "(Encriptar)";
            public const string Ciecf = "Obtener CIECF";
            public const string Ciec = "Autenticarse con CIEC";

            // R2. Se Actualizó el Mensaje para Añadir Email
            public const string Mail = "Actualizar y/o Adicionar Correo Electrónico";

            public const string FielTitle = "Ahora también puede autenticarse con su<br/>Firma Electrónica Avanzada ";
            public const string EncriptarTitle = "Encriptar archivo ";
            public const string Continuar = "Continuar";

            //Email
            public const string TitleEMail = "Servicio de Actualización de Correo Electrónico.";
            public const string MailLabelEmail = "Correo electrónico : ";
            public const string RfcEmail = "RFC: ";
            public const string ContinueEmail  = "Continuar";
            public const string ActualizarEmail = "Actualizar";
            public const string CancelarEmail  = "Cancelar";
            public const string FooterTextEmail  = "Estimado Contribuyente, actualice su Correo Electrónico y pulse 'Actualizar'.<br>Si este es correcto, pulse 'Cancelar' para ingresar a la aplicación deseada.";
            public const string FailureTextEmail  = "Formato de Correo Electrónico no valido";            
            public const string FailureRequireTextEmail = "Correo Electrónico requerido.";
            public const string FailureRequireChar  = "(*)";
            public const string SuccesfullTextEmail = "Su Correo Electrónico ha sido actualizado. Pulse 'Continuar' para ingresar a la aplicación deseada.<br/><br/>";
            public const string ExceptionTextEmail = "Servicio de actualización de Correo Electrónico no disponible, favor de intentarlo mas tarde."; 
            public const string RegExEmail  = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";            
        }

        public class Url
        { 
            public const string Fiel = @"Fiel.aspx";
            public const string Ciec = @"Ciec.aspx";
            public const string CiecF = @"CiecF.aspx";
        }

        public class Style
        {
            public const string background_repeat = "background-repeat";
            public const string background_position = "background-position";
            public const string no_repeat = "no-repeat";
            public const string top = "top";
            public const string font_name = "Verdana";
            public const string font_ArialSans = "Arial,helvetica,sans-serif";
            public const string center = "center";
        }

        public class mimeType
        {
            public const string image = "image/jpg";
            public const string imagegif = "image/gif";
            public const string javascript = "text/javascript";
            
        }

        public class CommandName
        {
            public const string LoginTypeCiec = "LoginTypeCiec";
            public const string LoginTypeFiel = "LoginTypeFiel";
            public const string LoginButtonCommandName = "Login";
        }



    }
}
