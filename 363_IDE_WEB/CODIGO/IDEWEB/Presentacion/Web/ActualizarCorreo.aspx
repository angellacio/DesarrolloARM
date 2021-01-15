<%@ Page Language="C#" MasterPageFile="~/Declaracion.Master" AutoEventWireup="true" CodeBehind="ActualizarCorreo.aspx.cs" Inherits="Sat.Scade.Net.IDE.Presentacion.Web.ActualizarCorreo"  Title="Servicio de Administración Tributaria (México)"%>
<%@ Register Assembly="Sat.Scade.Net.IDE.Presentacion.Web" Namespace="Sat.Scade.Net.IDE.Presentacion.Web" TagPrefix="Sat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <sat:email id="Email1" runat="server" DestinationPageUrl="RecepcionDeclaracion.aspx" MembershipProvider="IDCMembershipProvider"></sat:email>
</asp:Content>
