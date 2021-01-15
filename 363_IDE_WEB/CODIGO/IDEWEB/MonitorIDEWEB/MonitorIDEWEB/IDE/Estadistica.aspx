<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="Estadistica.aspx.cs" Inherits="IDE_Estadistica" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultContent" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generar Reporte" />
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Tipo de Archivo:
                        </td>
                        <td>
                            <asp:DropDownList ID="TipoArchivo" runat="server" DataSourceID="ObjectDataSource2"
                                DataTextField="Descripcion" DataValueField="IdCatalogo">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Medio de Recepción:
                        </td>
                        <td>
                            <asp:DropDownList ID="MedioRecepcion" runat="server" DataSourceID="ObjectDataSource3"
                                DataTextField="Descripcion" DataValueField="IdCatalogo">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Periodo:
                        </td>
                        <td>
                            <asp:DropDownList ID="Periodo" runat="server">
                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                <asp:ListItem Value="0">Anual</asp:ListItem>
                                <asp:ListItem Selected="True" Value="-1">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fecha presentación Inicial:
                        </td>
                        <td>
                            <asp:Calendar ID="FechaInicioC" runat="server" OnSelectionChanged="FechaInicioC_SelectionChanged"
                                Visible="False"></asp:Calendar>
                            <asp:TextBox ID="FechaInicio" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar.JPG"
                                OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fecha presentación Final:
                        </td>
                        <td>
                            <asp:Calendar ID="FechaFinC" runat="server" OnSelectionChanged="FechaFinC_SelectionChanged"
                                Visible="False"></asp:Calendar>
                            <asp:TextBox ID="FechaFin" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar.JPG"
                                OnClick="ImageButton2_Click1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            RFC:
                        </td>
                        <td>
                            <asp:TextBox ID="Rfc" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="TraerCatalogos"
                    TypeName="ServiceMonitor.ServicioMonitorClient">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="1" Name="id" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="TraerCatalogos"
                    TypeName="ServiceMonitor.ServicioMonitorClient">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="2" Name="id" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                    Width="1000px" Height="100%" Visible="False">
                    <LocalReport ReportPath="IDE\Report1.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="TraerEstadistica"
                    TypeName="ServiceMonitor.ServicioMonitorClient">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="EE" Name="estadistica" Type="Object" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
