<%@ Page Language="C#" MasterPageFile="~/Declaracion.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sat.Scade.Net.IDE.Presentacion.Web.Default" Title="Servicio de Administración Tributaria (México)" %>
<%@ Register Assembly="Sat.Scade.Net.IDE.Presentacion.Web" Namespace="Sat.Scade.Net.IDE.Presentacion.Web"
    TagPrefix="Sat" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:HiddenField ID="Firma" runat="server" Value="*" />
    <asp:HiddenField ID="Encriptar" runat="server" Value="*" />
    <asp:HiddenField ID="archivoEncripta" runat="server" Value="*" />
    
      
     <div>
        <asp:Label ID="lblPOST" runat="server" Width="472px"></asp:Label>&nbsp;
     </div>

</asp:Content>