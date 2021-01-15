<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="IDE_Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Login.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmLogin" runat="server">
    <asp:ToolkitScriptManager ID="Toolkitscriptmanager1" runat="server">
    </asp:ToolkitScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="width: 20%">
                &nbsp;
            </td>
            <td style="width: 80%">
                <table class="LoginTable">
                    <tr>
                        <td class="LoginLogosTD">
                            &nbsp;
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table class="LoginControlesTabla">
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LoginControlTituloTD" colspan="2">
                                                Ingresar al Sistema
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LoginControlLabelTD">
                                                Usuario:
                                            </td>
                                            <td class="LoginControlTxtTD">
                                                <asp:TextBoxWatermarkExtender ID="WaterMark_User" TargetControlID="txtUserName" WatermarkText="Usuario"
                                                    WatermarkCssClass="Watermark" runat="server">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:TextBox ID="txtUserName" CssClass="textEntry" runat="server" AutoCompleteType="None"
                                                    OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LoginControlLabelTD">
                                                Contraseña:
                                            </td>
                                            <td class="LoginControlTxtTD">
                                                <asp:TextBox ID="txtPlain" runat="server" AutoCompleteType="None" CssClass="Watermark"
                                                    Text="Contraseña"></asp:TextBox>
                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" AutoCompleteType="None"
                                                    CssClass="passwordEntry"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="text-align: center">
                                            </td>
                                            <td align="right" style="text-align: right">
                                                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="submitButton"
                                                    OnClick="btnLogin_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center" style="text-align: center;">
                                                <asp:Label ID="ErrorMarcado" CssClass="Fail" runat="server" Text=""></asp:Label>
                                                <asp:ValidationSummary ID="valSummary" CssClass="Fail" DisplayMode="List" runat="server" />
                                                <asp:RequiredFieldValidator ID="valUsr" runat="server" ErrorMessage="El Usuario es requerido"
                                                    Display="None" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="valPwd" runat="server" ErrorMessage="La contraseña es requerida"
                                                    Display="None" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="valPage" runat="server" CssClass="Fail" OnServerValidate="valPage_ServerValidate"
                                                    Display="None">
                                                </asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right" style="text-align: right">
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <a>Cargando...</a>
                                                        <img src="../Image/loading.gif" alt="" title="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server"
                                TargetControlID="UpdatePanel1">
                                <Animations>
                        <OnUpdating>
                            <Parallel duration="0">
                                <ScriptAction Script='UpdatingFunction();' />
                                <EnableAction enabled='false'/>
                            </Parallel>
                        </OnUpdating>
                        <OnUpdated>
                            <Parallel duration="0">
                                <ScriptAction Script='UpdateEnd();' />
                                <EnableAction enabled='true'/>
                            </Parallel>
                        </OnUpdated>
                                </Animations>
                            </asp:UpdatePanelAnimationExtender>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    <script language="javascript" type="text/javascript">
        function $$(id)
        { return document.all ? document.all[id] : window.document.getElementById(id); }

        function UpdatedFunction() {
            $$('txtPassword').style.display = 'none';
            $$('btnLogin').disabled = false;
        }

        function UpdatingFunction() {
            $$('btnLogin').disabled = true;
        }

        function plainFocus(a, b) {
            $$(a).style.display = 'none';
            $$(b).style.display = '';
            $$(b).focus();
        }

        function passBlur(a, b) {
            if ($$(b).value.length == 0) {
                $$(b).style.display = 'none';
                $$(a).style.display = '';
            }
        }

        function UpdateEnd() {

            $$('txtPassword').style.display = 'none';

            $('#txtPlain').focus(function () {
                plainFocus('txtPlain', 'txtPassword');
            });

            $('#txtPassword').blur(function () {
                passBlur('txtPlain', 'txtPassword');
            });
        }

        $(document).ready(function () {
            $$('txtPassword').style.display = 'none';

            $('#txtPlain').focus(function () {
                plainFocus('txtPlain', 'txtPassword');
            });

            $('#txtPassword').blur(function () {
                passBlur('txtPlain', 'txtPassword');
            });
        });

    </script>
</body>
</html>
