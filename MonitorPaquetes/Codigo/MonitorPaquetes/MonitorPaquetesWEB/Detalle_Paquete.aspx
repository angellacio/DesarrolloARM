<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_Paquete.aspx.cs" Inherits="MonitorPaquetesWEB.Detalle_Paquete" %>

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
                <asp:Button ID="btnReportes" runat="server" CssClass="btn btn-primary navbar-btn" Text="Reportes" />
                <asp:Button ID="btnIncidentes" runat="server" 
                    CssClass="btn btn-success navbar-btn" Text="Incidentes" 
                    onclick="btnIncidentes_Click" />
            </div>
        </nav>

            <div class="row">
                <div class="col-sm-8">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Detale del Paquete</div>
                        <div class="panel-body">
                        
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="lblRdl" runat="server" CssClass="control-label" Text="RDL:"></asp:Label>
                                <asp:TextBox ID="txtRdl" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblPaquete" runat="server" CssClass="control-label" Text="Paquete:"></asp:Label>
                                <asp:TextBox ID="txtPaquete" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblDesarrolador" runat="server" CssClass="control-label" Text="Desarrollador:"></asp:Label>
                                <asp:TextBox ID="txtDesarrollador" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-8">
                                <asp:Label ID="lblEstado" runat="server" CssClass="control-label" Text="Estado:"></asp:Label>
                                <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                <asp:Label ID="lblFecha" runat="server" CssClass="control-label" Text="Fecha Registro:"></asp:Label>
                                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-6">
                                <asp:Label ID="lblPaquetesRela" runat="server" CssClass="control-label" Text="Paquetes Relacionados:"></asp:Label>
                                <asp:TextBox ID="txtPaquetesRela" runat="server" CssClass="form-control" 
                                    Enabled="False" TextMode="MultiLine" Height="140px"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <asp:Label ID="lblIncidentesRela" runat="server" CssClass="control-label" 
                                    Text="Incidentes o PPMC Relacionados:"></asp:Label>
                                <asp:TextBox ID="txtIncidentesRela" runat="server" CssClass="form-control" 
                                    Enabled="False" TextMode="MultiLine" Height="140px"></asp:TextBox>
                            </div>
                        </div>

                        </div>  
                    </div>   
                </div>
                 <div class="col-sm-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Información del Desarrollador</div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblNombre" runat="server" CssClass="control-label" Text="Nombre:"></asp:Label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblCorreo" runat="server" CssClass="control-label" Text="Correo:"></asp:Label>
                                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" 
                                        Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblTelefono" runat="server" CssClass="control-label" Text="Telefono Oficina:"></asp:Label>
                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" 
                                        Enabled="False" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblMovil" runat="server" CssClass="control-label" Text="Telefono Movil:"></asp:Label>
                                    <asp:TextBox ID="txtMovil" runat="server" CssClass="form-control" 
                                        Enabled="False" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>

                    <div class="panel panel-warning">
                        <div class="panel-heading">Detalle del Rechazo</div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtRechazo" runat="server" CssClass="form-control" 
                                        Enabled="False" Height="50px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
            <nav class="navbar navbar-inverse">
            <p class="navbar-text">Monitro de Paquetes APE4</p>
            </nav>

        </div>
    </form>
</body>
</html>
