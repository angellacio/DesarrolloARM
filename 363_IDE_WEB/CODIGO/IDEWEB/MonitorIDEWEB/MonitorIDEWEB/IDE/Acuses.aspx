<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Acuses.aspx.cs" Inherits="IDE_Acuses" %>
<!DOCTYPE html5 PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Servicio de Administración Tributaria (México)</title>
</head>
<body>
    <div class="row">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-9">
            El Estado de su Declaración es el siguiente:
            </div>
            <div class="col-sm-1">
                
                <a href="#" style="border:0;" id="imprimirAcuseMasHyperLink" name="imprimirAcuseMasHyperLink" 
                class="btn btn-link pull-right" onclick="javascript:window.print();window.close();">
                <img src="../Image/icon-print.jpg" width="26px" />
                 Imprimir
                </a>

            </div>
        </div>

    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    
    
    </form>
</body>
</html>
