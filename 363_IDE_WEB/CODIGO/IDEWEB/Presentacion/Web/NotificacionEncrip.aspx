<%@ Page Language="C#" MasterPageFile="~/LinGobMX.Master" AutoEventWireup="true"
    CodeBehind="NotificacionEncrip.aspx.cs" Inherits="Sat.Scade.Net.IDE.Presentacion.Web.NotificacionEncrip"
    Title="Servicio de Administración Tributaria (México)" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-8">
            <ol class="breadcrumb">
                <li><a href="http://www.gob.mx/"><i class="icon icon-home"></i></a></li>
                <li>
                    <asp:LoginStatus ID="satSesionLoginStatus" runat="server" LogoutAction="RedirectToLoginPage"
                        LoginText="Inicio" LogoutText="Inicio" SkinID="LoginStatusScade2" OnLoggedOut="satSesionLoginStatus_LoggedOut" />
                </li>
                <li>
                    <asp:LinkButton ID="enviarMasHyperLink" runat="server" OnClick="enviarMasHyperLink_Click">Carga de archivo</asp:LinkButton>
                </li>
                <li class="active">Notificación</li>
            </ol>
        </div>
        <div class="col-sm-4">
            <div class="user-credencials">
                <ul class="list-unstyled" id="LabelRfc">
                    <li>
                        <asp:Label ID="rfcLabel" CssClass="user-credencials__name" runat="server">RFC</asp:Label>
                        <button type="button" class="btn btn-link pull-right" style="text-decoration: underline" id="cSesion"
                            onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$satSesionLoginStatus$ctl02','')">
                            Cerrar sesión</button>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-8">
            <img src="App_Themes/Default/Images/logoSHCP.jpg" width="120px" alt="Logo SHCP" />&nbsp;
            <img src="App_Themes/Default/Images/logoSAT.jpg" width="120px" alt="Logo SAT" />
        </div>
        <div class="col-sm-3">
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <h1 id="Titulo">
                Impuesto a Los Depósitos en Efectivo</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <h2 id="SubTitulo">
                Notificación</h2>
            <hr class="red" />
        </div>
    </div>
    <div class="row">
        <div class="col-sm-1">
        </div>
        <div class="col-sm-9">
            El Estado de su Declaración es el siguiente:
        </div>
        <div class="col-sm-1">
            <button type="button" id="imprimirAcuseMasHyperLink" name="imprimirAcuseMasHyperLink"
                class="btn btn-link pull-right" style="text-decoration: underline" onclick="javascript: window.print(); return false;">
                Imprimir</button>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-1">
        </div>
        <div class="col-sm-10">
            <div class="panel panel-default">
                <div class="panel-body">
                    <asp:Xml ID="HtmlFormaterXml" runat="server"></asp:Xml>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //            function window.onbeforeprint() {
        //                try {
        //                    document.getElementById('imprimirAcuseMasHyperLink').style.visibility = 'hidden';
        //                    document.getElementById('<%=satSesionLoginStatus.ClientID%>').style.visibility = 'hidden';
        //                }
        //                catch (e) {
        //                }
        //            }
        //            function window.onafterprint() {
        //                try {
        //                    document.getElementById('imprimirAcuseMasHyperLink').style.visibility = 'visible';
        //                    document.getElementById('<%=satSesionLoginStatus.ClientID%>').style.visibility = 'visible';
        //                }
        //                catch (e) {
        //                }
        //            }
    </script>
</asp:Content>

