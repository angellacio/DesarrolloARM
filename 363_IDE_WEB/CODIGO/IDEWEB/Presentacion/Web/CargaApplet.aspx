<%@ Page Language="C#" MasterPageFile="~/Declaracion.Master" AutoEventWireup="true"
    CodeBehind="CargaApplet.aspx.cs" Inherits="Sat.Scade.Net.IDE.Presentacion.Web.CargaApplet"
    Title="Servicio de Administración Tributaria (México)" %>

<%@ Register Assembly="Sat.Scade.Net.IDE.Presentacion.Web" Namespace="Sat.Scade.Net.IDE.Presentacion.Web"
    TagPrefix="Sat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="Firma" runat="server" Value="*" />
    <asp:HiddenField ID="Encriptar" runat="server" Value="*" />
    <asp:HiddenField ID="archivoEncripta" runat="server" Value="*" />
    <table width="100%">
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="razonSocialLabel" runat="server" Font-Bold="true" SkinID="LabelVerdana13">
                </asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td align="right" style="width: 33%" valign="bottom">
                <asp:Image ID="ImagenUsuarioImage" runat="server" SkinID="UserImage" />
                <asp:Label ID="UsuarioLabel" runat="server" Text="Usuario Autenticado: " SkinID="LabelVerdana13"></asp:Label>
            </td>
            <td align="left" style="width: 28%" valign="bottom">
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
                <applet code="Firma.class" archive="EncriptorIDE.jar" width="100%" height="300" id="appFEA"
                    runat="server">
                </applet>
            </td>
        </tr>
        <tr>
            <td style="width: 33%">
            </td>
            <td align="center" style="width: 28%">
                <Sat:ButtonEncripta ID="enviarEncriptaButton" runat="server" SkinID="EnviarEncriptaButton"
                    OnClick="EnviarDeclaracion_Click" Text="Enviar" Height="25px" Width="100px" Visible="false" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
