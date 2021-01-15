<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sat.Scade.Net.IDE.Presentacion.Web.Login" %>

<%@ Register Assembly="Sat.Scade.Net.IDE.Presentacion.Web" Namespace="Sat.Scade.Net.IDE.Presentacion.Web"
    TagPrefix="Sat" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Servicio de Administración Tributaria (México)</title>
</head>
<body onload="HabilitarControlesAutenticacion();">
    <form id="form1" runat="server">
    <div>
        <center>
            <table width="100%">
                <tr>
                    <td>
                        <Sat:Header ID="HeaderLogin" runat="server" SkinID="HeaderScade2">
                        </Sat:Header>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Panel ID="pnlLogin" runat="server">
                            <Sat:LoginScade ID="LoginScade1" runat="server" SkinID="LoginScade2" MembershipProviderCiec="IDCMembershipProvider"
                                MembershipProviderFiel="FielMembershipProvider" UrlPaginaCorreoCiec="~/ActualizarCorreo.aspx"
                                UrlPaginaCorreoFiel="~/ActualizarCorreo.aspx" UrlPaginaDestinoCiec="CargaApplet.aspx"
                                UrlPaginaDestinoCiecMod="RecepcionDeclaracion.aspx" UrlPaginaDestinoFiel="CargaApplet.aspx"
                                OnLoggedInCiec="LoginScade1_LoggedInCiec" OnCambiandoModo="LoginScade1_CambiandoModo"
                                MedioPresentacion="Internet" CiecFortalecidaUrl="http://portalsat.plataforma.sat.gob.mx/CIECInternet/"
                                JavaArchive="AppletFirma.jar" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Panel ID="pnlErrorLogin" runat="server" Visible="false">
                            <div>
                                <table border="0" cellpadding="0" cellspacing="0" width="600">
                                    <tr>
                                        <td align="center" style="height: 324px">
                                            <h1 style="font-family: Arial; font-size: 22px;">
                                                Por el momento el servicio no está disponible.</h1>
                                            <h1 style="font-family: Arial; font-size: 22px;">
                                                Favor de intentarlo más tarde.</h1>
                                            <asp:HyperLink ID="lnkInicioSesionNovell" runat="server"></asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <br />
                        <br />
                        <asp:Image ID="FooterMaster" runat="server" SkinID="FooterScade2" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
