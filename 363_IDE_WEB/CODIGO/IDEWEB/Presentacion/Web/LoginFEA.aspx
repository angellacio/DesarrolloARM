<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginFEA.aspx.cs" Inherits="Sat.Scade.Net.IDE.Presentacion.Web.LoginFEA" %>

<%@ Register Assembly="Sat.Scade.Net.IDE.Presentacion.Web" Namespace="Sat.Scade.Net.IDE.Presentacion.Web"
    TagPrefix="Sat" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autentifiacion por Firma Electronica IdeWeb</title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <center>
            <table width="100%" cellpadding="4" cellspacing="4">
                <tr>
                    <td>
                    </td>
                    <td colspan="2" align="center">
                        <Sat:Header ID="HeaderLogin" runat="server" SkinID="HeaderScade2">
                        </Sat:Header>
                        <br />
                        <br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblTitulo" runat="server" Style="font-size: 2ex; font-family: Verdana;
                            font-weight: bold"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;">
                    </td>
                    <td align="right" style="width: 40%;">
                        Certificado (*.cer):&nbsp;
                    </td>
                    <td align="left" style="width: 40%;">
                        <asp:FileUpload ID="fuCertificado" runat="server" Width="350px" />
                    </td>
                    <td style="width: 10%;">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Clave Privada (*.key):&nbsp;
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="fuKey" runat="server" Width="350px" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        RFC:&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRFC" runat="server" Width="218px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Contraseña:&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtContraseña" runat="server" Width="219px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="Loggin" Style="border-top-style: solid; font-size: 13px; border-left-style: solid;
                            font-family: Arial,helvetica,sans-serif; border-top-color: #7a8a99; border-bottom-style: solid;
                            border-left-color: #7a8a99; font-weight: bold; border-bottom-color: #7a8a99;
                            border-right-style: solid; border-right-color: #7a8a99; background-color: #d4e2ef"
                            runat="server" Text="a" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:LinkButton ID="MailUpdateLink" runat="server" Style="font-size: 13px; font-family: Arial,helvetica,sans-serif">a</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:LinkButton ID="CiecLinkButton" runat="server" Style="font-size: 13px; font-family: Arial,helvetica,sans-serif"
                            OnClick="lkbAutContraseña_Click">a</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center"> <br /><br />
                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #FF0000;">IMPORTANTE:</label><label style="font-family: Arial, Helvetica, sans-serif; font-size: 13px"> Para un correcto funcionamiento del aplicativo utilizar Internet Explorer 
                        y realizar la siguiente </label>
                        <a href="AyudaConfigura.html" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #0000FF;">Configuraciones</a>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
