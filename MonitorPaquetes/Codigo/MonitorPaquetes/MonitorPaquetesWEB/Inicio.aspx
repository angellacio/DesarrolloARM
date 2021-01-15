<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="MonitorPaquetesWEB.Inicio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Monitor de Paquetes</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid" id="Principal">
        <div class="well"><h2>Monitor de Paquetes e Incidentes APE4</h2></div>
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                <asp:LinkButton ID="lnkInicio" runat="server" CssClass="navbar-brand" 
                        onclick="lnkInicio_Click">Inicio</asp:LinkButton>
                </div>
                <asp:Button ID="btnReportes" runat="server" CssClass="btn btn-primary navbar-btn" onclick="Button1_Click" Text="Reportes" />
                <asp:Button ID="btnIncidentes" runat="server" 
                    CssClass="btn btn-success navbar-btn" Text="Incidentes" 
                    onclick="btnIncidentes_Click" />
                
            </div>
        </nav>

            <div class="row">
                <div class="col-sm-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Paquetes en Transito</div>
                        <div class="panel-body">
                        <asp:GridView ID="GdvPaquetes" 
                                CssClass="table table-striped table-bordered table-hover" runat="server" 
                                onrowcommand="GdvPaquetes_RowCommand" >
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Detalle" HeaderText="Detalle" 
                                    ShowHeader="True" Text="Detalle" ControlStyle-CssClass="btn btn-primary"/>
                            </Columns>
                        </asp:GridView>
                        </div>  
                    </div>   
                </div>

                <div class="col-sm-6">
                    <div class="panel panel-warning">
                        <div class="panel-heading">Notificaciones Realizadas</div>
                        <div class="panel-body">
                        <asp:GridView ID="GdvNotificaciones" CssClass="table table-striped table-bordered table-hover" runat="server">
                        </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <nav class="navbar navbar-inverse">
            <p class="navbar-text">Monitro de Paquetes APE4</p>
            </nav>
        </div>
    
    </form>
</body>
</html>
