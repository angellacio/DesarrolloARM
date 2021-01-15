<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="IDE_Default" Culture="auto" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .titulo1
        {
            font-size: small;
            text-align: center;
            font-family: Arial, Verdana;
            font-weight: bolder;
            color: Black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultContent" runat="Server">
    <!-- <div id="grid" style="width:100%; overflow:scroll;"> -->
    <table style="width: 343px;">
        <tr>
            <td class="titulo1" colspan="3">
                Declaraciones
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Consultar" />
            </td>
            <td>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Limpiar" />
            </td>
            <td>
                <a href="#" onclick="javascript:window.print();window.close();">
                    <img style="border: 0;" src="../Image/icon-print.jpg" width="26px" />
                    Imprimir</a>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table class="mGrid" style="width: 100%;">
        <tr>
            <td>
                Folio
            </td>
            <td colspan="2">
                Fecha presentación
            </td>
            <td>
                Estatus
            </td>
            <td>
                Nombre de Archivo
            </td>
            <td>
                RFC
            </td>
            <td>
                Razón Social
            </td>
            <td>
                Sector
            </td>
            <td>
                Medio de Recepción
            </td>
            <td>
                Forma de la Declaración
            </td>
            <td>
                Tipo de la declaración
            </td>
            <td>
                Ejercicio
            </td>
            <td>
                Periodo
            </td>
            <td>
                Operaciones
            </td>
            <td>
                Formato
            </td>
            <td>
                Motivo de Rechazo
            </td>
        </tr>
        <tr>
            <td class="style4">
                <asp:TextBox ID="Folio" runat="server" Width="40px" EnableViewState="False" TabIndex="10"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Calendar ID="FechaInicioC" runat="server" OnSelectionChanged="FechaInicioC_SelectionChanged1"
                            Height="57px" Width="90px" Visible="False" TabIndex="22"></asp:Calendar>
                        <table style="width: 130px;">
                            <tr>
                                <td style="border: none;">
                                    Inicial
                                </td>
                                <td style="border: none;">
                                    <asp:TextBox ID="FechaInicio" runat="server" Width="65px" TabIndex="20"></asp:TextBox>
                                </td>
                                <td style="border: none;">
                                    <asp:ImageButton ID="BCalen1" runat="server" ImageUrl="~/Image/Calendar.JPG" OnClick="BCalen1_Click"
                                        TabIndex="21" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="style4">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Calendar ID="FechaFinC" runat="server" OnSelectionChanged="FechaFinC_SelectionChanged"
                            Height="63px" Width="90px" Visible="False" TabIndex="32"></asp:Calendar>
                        <table style="width: 130px;">
                            <tr>
                                <td style="border: none;">
                                    Final
                                </td>
                                <td style="border: none;">
                                    <asp:TextBox ID="FechaFin" runat="server" Width="65px" TabIndex="30"></asp:TextBox>
                                </td>
                                <td style="border: none;">
                                    <asp:ImageButton ID="BCalen2" runat="server" ImageUrl="~/Image/Calendar.JPG" OnClick="BCalen2_Click"
                                        TabIndex="31" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="style4">
                <asp:DropDownList ID="Estatus" runat="server">
                    <asp:ListItem Selected="True" Value="0">Todas</asp:ListItem>
                    <asp:ListItem Value="1">Aceptadas</asp:ListItem>
                    <asp:ListItem Value="2">Rechazadas</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:TextBox ID="NombreArchivo" runat="server"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:TextBox ID="Rfc" runat="server" Width="65px"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:TextBox ID="RazonSocial" runat="server" Width="120px"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:DropDownList ID="Sector" runat="server" DataSourceID="ObjectDataSource5" DataTextField="Descripcion"
                    DataValueField="IdCatalogo" OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" SelectMethod="TraerCatalogos"
                    TypeName="ServiceMonitor.ServicioMonitorClient">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="4" Name="id" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td class="style4">
                <asp:DropDownList ID="MedioRecepcion" runat="server" DataSourceID="ObjectDataSource3"
                    DataTextField="Descripcion" DataValueField="IdCatalogo" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="TraerCatalogos"
                    TypeName="ServiceMonitor.ServicioMonitorClient">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="2" Name="id" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td class="style4">
                <asp:DropDownList ID="FormaDeclaracion" runat="server">
                    <asp:ListItem Selected="True" Value="2">Todas</asp:ListItem>
                    <asp:ListItem Value="1">Normal</asp:ListItem>
                    <asp:ListItem Value="0">Complementaria</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="EstadoDeclaracion" runat="server">
                    <asp:ListItem Value="1">Anual</asp:ListItem>
                    <asp:ListItem Value="0">Mensual</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">Todas</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="Ejercicio" runat="server" OnSelectedIndexChanged="Ejercicio_SelectedIndexChanged">
                    <asp:ListItem Selected="True">Todos</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="Periodo" runat="server" OnSelectedIndexChanged="Periodo_SelectedIndexChanged1">
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
            <td class="style4">
                <asp:TextBox ID="Operaciones" runat="server" Width="27px"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:TextBox ID="Formato" runat="server" Width="32px"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:TextBox ID="MotivoRechazo" runat="server" Width="120px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
        EnableModelValidation="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1"
        Width="2610px">
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <Columns>
            <asp:BoundField DataField="Folio" HeaderText="No. Folio" ItemStyle-Width="2%">
                <ItemStyle Width="2%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="fechaRecepcion" HeaderText="Fecha de Recepción" ItemStyle-Width="7%">
                <ItemStyle Width="7%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-Width="5%">
                <ItemStyle Width="5%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre del Archivo" ItemStyle-Width="10%">
                <ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Rfc" HeaderText="RFC" ItemStyle-Width="5%">
                <ItemStyle Width="5%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Banco" HeaderText="Razón Social" ItemStyle-Width="13%">
                <ItemStyle Width="13%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Sector" HeaderText="Sector" ItemStyle-Width="5%">
                <ItemStyle Width="5%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MedioRecepcion" HeaderText="Medio de Recepción" ItemStyle-Width="4%">
                <ItemStyle Width="4%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="FormaDeclaracion" HeaderText="Forma de Declaración" ItemStyle-Width="5%">
                <ItemStyle Width="5%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EstadoDeclaracion" HeaderText="Tipo de Declaración"
                ItemStyle-Width="3%">
                <ItemStyle Width="3%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Ejercicio" HeaderText="Ejericio" ItemStyle-Width="3%">
                <ItemStyle Width="3%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="sPeriodo" HeaderText="Periodo" ItemStyle-Width="3%">
                <ItemStyle Width="3%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Operaciones" HeaderText="Operaciones" ItemStyle-Width="3%">
                <ItemStyle Width="3%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Formato" HeaderText="Formato" ItemStyle-Width="2%">
                <ItemStyle Width="2%"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Motivo de Rechazo" ControlStyle-Width="100%">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MotivoRechazo") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MotivoRechazo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%" />
            </asp:TemplateField>
            <asp:HyperLinkField HeaderText="Acuses" DataTextField="Folio" DataNavigateUrlFields="Folio"
                ItemStyle-Width="3%" DataNavigateUrlFormatString="AcuseMon.aspx?Folio={0}" Target="_blank">
                <ItemStyle Width="3%"></ItemStyle>
            </asp:HyperLinkField>
        </Columns>
        <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
    <!-- </div> -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
