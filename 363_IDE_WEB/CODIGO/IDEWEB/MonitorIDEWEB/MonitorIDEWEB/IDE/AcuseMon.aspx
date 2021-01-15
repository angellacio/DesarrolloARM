<%@ Page Title="Servicio de Administración Tributaria (México)" Language="C#" MasterPageFile="~/Shared/MasterPage2.master" AutoEventWireup="true" CodeFile="AcuseMon.aspx.cs" Inherits="Shared_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-sm-8">
            <ol class="breadcrumb">
                <li><a href="http://www.gob.mx/"><i class="icon icon-home"></i></a></li>
                <li>
                    <asp:LinkButton ID="Inicio" runat="server" OnClick="satSesionLoginStatus_LoggedOut">Inicio</asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="enviarMasHyperLink" runat="server">Monitor</asp:LinkButton>
                </li>
                <li class="active">Acuse</li>
            </ol>
        </div>
        <div class="col-sm-4" id="cSesion">
                 <asp:LinkButton id="LinkButton1" runat="server" OnClick="satSesionLoginStatus_LoggedOut" style="text-decoration: underline" class="btn btn-link pull-right">
                            Cerrar sesión</asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-8">
            <img src="../Image/logoSHCP.jpg" width="120px" alt="Logo SHCP" />&nbsp;
            <br />
            <img src="../Image/REIMaguila.gif" width="120px" alt="Logo SHCP" />
        </div>
        <div class="row" align="right">
            <img src="../Image/logoSAT.jpg" width="120px" alt="Logo SAT" />
        </div>
        <div class="col-sm-3">
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <h1 id="Titulo"><asp:Label ID="LabelTitulo" runat="server" Text="Titulo"></asp:Label></h1>
        </div>
    </div>
    <div class="row" id="grande">
        <div class="col-sm-12">
            <h2 id="SubTitulo"><asp:Label ID="LabelNotificacion" runat="server" Text="Notificacion"></asp:Label></h2>
            <hr class="red" />
        </div>
    </div>
    <div class="row">
        <div class="col-sm-1">
        </div>
        <div class="col-sm-9">
        </div> 
        <div class="col-sm-1">
            <button type="button" id="imprimirAcuseMasHyperLink" name="imprimirAcuseMasHyperLink"
                class="btn btn-link pull-right" style="text-decoration: underline" onclick="javascript: window.print(); return true;">
                Imprimir</button>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-1">
        </div>
        <div class="col-sm-10">
            <div class="panel panel-default">
                <div class="panel-body">
                    <center>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                   
                    </center>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

