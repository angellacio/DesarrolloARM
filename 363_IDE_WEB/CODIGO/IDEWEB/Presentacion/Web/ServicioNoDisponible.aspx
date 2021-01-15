<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServicioNoDisponible.aspx.cs" Inherits="Sat.Scade.Net.IDE.Presentacion.Web.ServicioNoDisponible" %>
<%@ Register Assembly="Sat.Scade.Net.IDE.Presentacion.Web" Namespace="Sat.Scade.Net.IDE.Presentacion.Web"  TagPrefix="Sat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Servicio de Administración Tributaria (México)</title>
</head>
<body>
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
                        <td style="font-weight: bold; font-size: 18px; color: #000000; font-family: Verdana">
                            <br />
                            <br />
                            <br />
                            Por el momento el servicio no está disponible.
                            <br />
                            Favor de intentarlo más tarde.
                            <br />
                            <br />
                            <br />
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
