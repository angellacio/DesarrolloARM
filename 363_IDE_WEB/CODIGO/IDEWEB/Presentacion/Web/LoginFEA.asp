<%@  language="VBScript" %>
<%
    dim WIDTH
    dim HEIGHT
    dim JAVA_ARCHIVE
    dim JAVA_CODE
    dim ORIGINALURL
    dim PAGINA
    dim info
    dim FechaServidor
    dim FechaGenerica
	
    WIDTH = "0"
    HEIGHT = "0"
    ORIGINALURL = UCase(Request.ServerVariables("QUERY_STRING"))	
    PAGINA = "firma"    
    info = request.ServerVariables("HTTP_USER_AGENT")
    
    FechaServidor = Date() 
    FechaGenerica = Right(Cstr(Day(FechaServidor) + 100),2) & "/" & Right(Cstr(Month(FechaServidor) + 100),2) & "/" & Year(FechaServidor)
    FECHA_ACTUAL = FechaGenerica
    dim prot
    prot = "http" 
    if lcase(request.ServerVariables("HTTPS")) <> "off" then prot = "https"
	
%>
<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>SAT Autenticaci&oacute;n</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE; IE=9; IE=EmulateIE9; IE=8; IE=EmulateIE8; chrome=1;" />
    <meta name="content-language" content="es, es-mx" charset="utf-8" />
    <meta name="copyright" content="SAT-SHCP" />
    <style type="text/css">
        label
        {
            font-family: "Verdana, Arial, Helvetica";
            font-size: 12pt;
            color: 000000;
            font-weight: bold;
        }
        small
        {
            font-family: "Verdana, Arial, Helvetica";
            font-size: 8pt;
            color: red;
        }
        Mensaje
        {
            font-family: "Verdana, Arial, Helvetica";
            font-size: 12pt;
            color: red;
        }
    </style>
    <script language="Javascript" type="text/javascript" src="js/FielUtil-1.4.js"></script>
    <script language="Javascript" type="text/javascript" src="js/jsrsasign-4.7.0-all-min_sat-fix.js"></script>
    <script language="Javascript" type="text/javascript" src="js/jquery-1.6.4.min.js"></script>
    <script language="Javascript" type="text/javascript" src="js/ValidacionAccesos.js"></script>
</head>
<body>
    <table width="100%" border="0" style="font-size: 13px; font-family: Verdana;">
        <tr>
            <td width="100%" align="center" colspan="2">
                <br />
                <img id="Image1" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px;
                    border-top-width: 0px" src="App_Themes/Default/Images/HeaderHomolagado.jpg" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <table width='100%'>
                    <tr>
                        <td width='100%' align="center">
                            <span id="AccesoFIEL" style="font-size: 2ex; font-family: Verdana; font-weight: bold">
                                Acceso por e.firma</span>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td width='66%'>
                <table width='100%' border="0">
                    <tr>
                        <td width='60%' />
                        <td width='40%' align="left">
                            <label class=''>
                                Certificado (.cer)<samp id='errCertificadoAs' name='errCertificadoAs' class=''></samp>:
                            </label>
                            <br>
                        </td>
                    </tr>
                </table>
            </td>
            <td width='34%' />
        </tr>
        <tr>
            <td width='66%'>
                <table width='100%' border="0">
                    <tr>
                        <td width='60%' />
                        <td width='40%' align="left">
                            <input type='text' readonly='readonly' id='valorCert' name='valorCert' placeholder='Ubicación del certificado.'
                                class='' size="40" />
                            <input type="submit" id="btnCertificado" name="btnCertificado" value="Buscar" onclick="javascript:return SeleccionaArchivo(2);" />
                            <small id='errCertificado' name='errCertificado' class='' style='display: none'>Este
                                campo es obligatorio</small>
                        </td>
                    </tr>
                </table>
            </td>
            <td width='34%' />
        </tr>
        <tr>
            <td width='66%'>
                <table width='100%' border="0">
                    <tr>
                        <td width='60%' />
                        <td width='40%' align="left">
                            <label>
                                Clave privada (.key)<samp id='errClavePrivadaAs' name='errClavePrivadaAs'></samp>:
                            </label>
                            <br>
                        </td>
                    </tr>
                </table>
            </td>
            <td width='34%' />
        </tr>
        <tr>
            <td width='66%'>
                <table width='100%' border="0">
                    <tr>
                        <td width='60%' />
                        <td width='40%' align="left">
                            <input type='text' readonly='readonly' id='valorLlav' name='valorLlav' placeholder='Ubicación de llave privada.'
                                size="40" />
                            <input type="submit" id="btnLlavePrivada" name="btnLlavePrivada" value="Buscar" onclick="javascript:return SeleccionaArchivo(1);" />
                            <small id='errClavePrivada' name='errClavePrivada' style='display: none'>Este campo
                                es obligatorio</small>
                        </td>
                    </tr>
                </table>
            </td>
            <td width='34%' />
        </tr>
        <tr>
            <td width='66%'>
                <table width='100%' border="0">
                    <tr>
                        <td width='60%' />
                        <td width='10%' align="left">
                            <label>
                                Contraseña<samp id='errPasswordAs' name='errPasswordAs'></samp>:</label>
                            <span data-toggle='tooltip' data-placement='right' title='Esta contraseña es la misma de su CIEC o CIEC Fortalecida'>
                            </span>
                        </td>
                        <td width='30%' align="left">
                            <input type='password' id='privateKeyPassword' name='privateKeyPassword' size='30'
                                placeholder='Contraseña' onkeypress='return CamposLogin_FEA(event, 2)' onchange='CamposLoginMayusculas(2);' />
                            <small id='errPassword' name='errPassword' style='display: none;'>Este campo es obligatorio</small>
                        </td>
                    </tr>
                </table>
            </td>
            <td width='34%' />
        </tr>
        <tr>
            <td width='66%'>
                <table width='100%' border="0">
                    <tr>
                        <td width='60%'>
                            <td />
                            <td width='10%' align="left">
                                <label>
                                    RFC<samp id='errUsernameAs' name='errUsernameAs'></samp>:
                                </label>
                            </td>
                            <td width='30%' align="left">
                                <input type='text' id='RFC' name='RFC' size='29' placeholder='RFC' onkeypress='return CamposLogin_FEA(event, 1)'
                                    onchange='CamposLoginMayusculas(2);' />
                                <small id='errUsername' name='errUsername' style='display: none;'>Este campo es obligatorio</small>
                            </td>
                    </tr>
                </table>
            </td>
            <td width='34%' />
        </tr>
        <tr>
            <td width='66%'>
                <table width='100%' border="0">
                    <tr>
                        <td width='60%' />
                        <td align="left" width='40%'>
                            <div id="rowError" style="display: none">
                                <mensaje id="lblMensaje">Usuario o contraseña incorrecto.</mensaje>
                                <label name="loading" id="loading" style="display: none; width: 100%;">
                                    Descargando las herramientas necesarias...</label>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width='34%' />
        </tr>
        <tr>
            <td width='66%'>
                <table width='100%' border="0">
                    <tr>
                        <td width='70%' />
                        <td align="left" width='10%'>
                            <input type="button" id="ButtonRegresa" id="LinkCIEC1" onclick="return navigate('.');"
                                value="Contraseña" style="border-top-style: solid; font-size: 13px; border-left-style: solid;
                                font-family: Arial,helvetica,sans-serif; border-top-color: #7a8a99; border-bottom-style: solid;
                                border-left-color: #7a8a99; font-weight: bold; border-bottom-color: #7a8a99;
                                border-right-style: solid; border-right-color: #7a8a99; background-color: white">
                        </td>
                        <td align="center" width='1%' />
                        <td align="left" width='19%'>
                            <input type="submit" id="ButtonLogin" name="ButtonLogin" value="Aceptar" onclick="ejecutarProcesoFirma('FIELForm:procesa')"
                                style="border-top-style: solid; font-size: 13px; border-left-style: solid; font-family: Arial,helvetica,sans-serif;
                                border-top-color: #7a8a99; border-bottom-style: solid; border-left-color: #7a8a99;
                                font-weight: bold; border-bottom-color: #7a8a99; border-right-style: solid; border-right-color: #7a8a99;
                                background-color: #d4e2ef">
                        </td>
                    </tr>
                </table>
            </td>
            <td width='34%' />
        </tr>
        <tr>
            <td colspan="2">
                <br>
                <table width='100%' border="0">
                    <tr>
                        <td align="center" width='62%'>
                            <a href="https://tramitesdigitales.sat.gob.mx/Scade.Net.ServicioActualizacionEMail/Login.aspx?MainUri=">
                                Actualizar y/o Adicionar Correo Electrónico</a><br>
                            <br>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <table width='100%' border="0">
                    <tr>
                        <td align="Center">
                            <label style="font-size: 13px; font-family: Arial, Helvetica, sans-serif; color: #ff0000">
                                IMPORTANTE:</label>
                            <label style="font-size: 13px; font-family: Arial, Helvetica, sans-serif; color: black;
                                font-weight: normal">
                                Para un correcto funcionamiento del aplicativo utilizar Internet Explorer y realizar
                                la siguiente
                            </label>
                            <a style="font-size: 13px; font-family: Arial, Helvetica, sans-serif; color: #0000ff"
                                href="AyudaConfigura.html" target="_blank">Configuraciones</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <form method="post" id="frmApplet" name="frmApplet" action="LoginFEA.aspx?" style="width: -1;
    height: -1;">
        <input type="hidden" id="FechaCertificado" name="FechaCertificado" value="*">
        <input type="hidden" id="rfcEnvio" name="rfcEnvio" value="">
        <input type="hidden" id="firmaDigital" name="firmaDigital" value="*">
        <input type="hidden" id="numeroSerie" name="numeroSerie" value="*">
        <input type="hidden" id="cadenaOriginal" name="cadenaOriginal">
        <input type="hidden" id="URLoriginal" name="URLoriginal" value='*'>
        <input type="hidden" id="lblPasword" name="lblPasword" value='*'> 
    </form>
    <script type="text/javascript">
        document.getElementById("loading").style.display = "none";
    </script>
    <input type='file' style="visibility: hidden;" accept=".cer" id='certificate' name='certificate'
        onchange="javascript:return probando(1);" />
    <input type='file' style="visibility: hidden;" accept=".key" id='privateKey' name='privateKey'
        onchange="javascript:return probando(2);" />
    <script>

        var submitFirmaAsociado = true;
        if (submitFirmaAsociado == false) {
            submitFirmaAsociado = true;
            $("#FIELForm").submit(function (event) {
                procesarFirmaFormulario(event);
            });
        }
        $('form input').keydown(function (e) {
            if (e.keyCode == 13) {
                var inputs = $(this).parents("form").eq(0).find(":input");
                if (inputs[inputs.index(this) + 1] != null) {
                    inputs[inputs.index(this) + 1].focus();
                }
                e.preventDefault();
                return false;
            }
        });
	
    </script>
</body>
</html>
