<%@ Page Language="C#" MasterPageFile="~/Declaracion.Master" ValidateRequest="false"
    AutoEventWireup="true" CodeBehind="RecepcionDeclaracion.aspx.cs" Inherits="Sat.Scade.Net.IDE.Presentacion.Web.RecepcionDeclaracion"
    Title="Servicio de Administración Tributaria (México)" %>

<%@ Register Assembly="Sat.Scade.Net.IDE.Presentacion.Web" Namespace="Sat.Scade.Net.IDE.Presentacion.Web"
    TagPrefix="Sat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/jscript">BloquearF5();</script>
    <asp:HiddenField ID="HValidacion" runat="server" Value="" />
    <table width="100%">
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="razonSocialLabel" runat="server" Font-Bold="true" SkinID="LabelVerdana13">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <hr />
                <asp:Label ID="SeleccionLabel" runat="server" Text="Seleccione el Archivo" SkinID="LabelVerdana13"></asp:Label>
                <hr />
            </td>
        </tr>
        <tr align="center">
            <td align="right" style="width: 40%" valign="bottom">
                <asp:Image ID="ImagenUsuarioImage" runat="server" SkinID="UserImage" />
                <asp:Label ID="UsuarioLabel" runat="server" Text="Usuario Autenticado: " SkinID="LabelVerdana13"></asp:Label>
            </td>
            <td align="left" style="width: 30%" valign="bottom">
                <asp:Label ID="rfcLabel" runat="server" Font-Bold="True" SkinID="LabelVerdana13Bold"></asp:Label>
            </td>
            <td align="left" style="width: 30%" valign="bottom">
                <asp:LoginStatus ID="satSesionLoginStatus" runat="server" LogoutAction="RedirectToLoginPage"
                    SkinID="LoginStatusScade2" OnLoggedOut="satSesionLoginStatus_LoggedOut" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="height: 21px">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="ArchivoLabel" runat="server" Text="Nombre del Archivo:" SkinID="LabelVerdana13"></asp:Label>
                <asp:FileUpload ID="CargaFileUpload" runat="server" SkinID="CargaFileUploadSkin" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="height: 21px">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <Sat:ButtonBlocked ID="enviarButton" runat="server" SkinID="EnviarButton" OnClick="EnviarDeclaracion_Click" Text="Enviar" />
            </td>
        </tr>
    </table>
</asp:Content>
