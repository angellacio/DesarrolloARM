/* 
ValidacionAccesos.js	 v. 1.0
Angel Ramirez Mancera 		01/09/2015	 -- Creacion para la validacion del acceso a los aplicativos.
*/
var  BANDERA_ERROR_RFC
function MuestraMensajes(sStyle, sMsjInicio, sMensaje)
{
	var lblError = document.getElementById('lblMensaje');
	var divErroes = document.getElementById('rowError');
	var sMensajeCompleto = "";
	
	if (sMsjInicio == "") {
		if (sStyle == "alert alert-success") sMensajeCompleto = "<strong>¡ Felicidades !</strong> " + sMensaje;
		else if(sStyle == "alert alert-info") sMensajeCompleto = "<strong>¡ Aviso !</strong> " + sMensaje;
		else if(sStyle == "alert alert-warning") sMensajeCompleto = "<strong>¡ Atención !</strong> " + sMensaje;
		else if(sStyle == "alert alert-danger") sMensajeCompleto = "<strong>¡Error!</strong> " + sMensaje;
		else sMensajeCompleto = sMensaje;
	}
	else {
		sMensajeCompleto = "<strong>¡ " + sMsjInicio + " !</strong> " + sMensaje;
	}
	
	lblError.innerHTML =  sMensajeCompleto;
	lblError.className = sStyle;
	divErroes.style.display = 'block';
}


/*
* Validacion 
*
*/
function ejecutarProcesoFirma(){

	var fileCertificado = document.getElementById('certificate');
	var fileLlavePrivada= document.getElementById('privateKey');
    var contrasena=document.getElementById('privateKeyPassword');   
	var vrfc = document.getElementById('RFC');
	var rfcEnvio = document.getElementById('rfcEnvio');
	var valorCer = document.getElementById('valorCert');
	var valorLlav = document.getElementById('valorLlav');		
	var errClavePrivadaAs = document.getElementById('errClavePrivadaAs');
	var errClavePrivada = document.getElementById('errClavePrivada');
	var errCertificado = document.getElementById('errCertificado');
	var errPassword = document.getElementById('errPassword');
	var errPasswordAs = document.getElementById('errPasswordAs');
	var errUsername = document.getElementById('errUsername');
	var errUsernameAs = document.getElementById('errUsernameAs');
	var compatibilidad = PKI.SAT.FielUtil.validaNavegador(fileCertificado);
        if (compatibilidad === false) {
           alert('Navegador NO es compatible.' + String.fromCharCode(13) + 'Compatible con todos los navegadores excepto: Internet Explorer 5,6,7,8,9; Safari (IOS), En Internet Explorer Verificar compatibilidad con F12-Emulación');
		   window.scrollTo(0,0);
		   return false;
         }      else if (compatibilidad != true) {
       MuestraMensajes("alert alert-danger", "", "Ocurrió un error al validar el navegador: \n" + PKI.SAT.FielUtil.obtenMensajeError(compatibilidad));
		window.scrollTo(0,0);
		return false;
    }
		
	rfcEnvio.value= document.getElementById('RFC').value;
	
	if (contrasena.value == '' || fileLlavePrivada.files.length == 0 || fileCertificado.files.length == 0 || vrfc.value == '')	
	{	
		if ( fileCertificado.files.length == 0){
			errCertificado.style.display = 'block';
			errCertificadoAs.className = 'form-text form-text-error';
			valorCer.className = 'form-control form-control-error';
            valorCer.value = '';
		}
		
		if ( fileLlavePrivada.files.length == 0){
			
			errClavePrivada.style.display = 'block';
			errClavePrivadaAs.className = 'form-text form-text-error';
			valorLlav.className = 'form-control form-control-error';	
            valorLlav.value = '';
		}
		
		if ( contrasena.value == ''){
			errPassword.style.display = 'block';
			errPasswordAs.className = 'form-text form-text-error';
			contrasena.className = 'form-control form-control-error';			
		}
			
		if (vrfc.value == ''){	
			errUsername.style.display = 'block';
			errUsernameAs.className = 'form-text form-text-error';
			vrfc.className = 'form-control form-control-error';			
		}
		
		MuestraMensajes("alert alert-danger", "", "Datos incompletos. Favor de verificar.")		
		window.scrollTo(0,0);
		
		return false;
	}
    	PKI.SAT.FielUtil.validaFielyFirmaCadena(
                fileCertificado,
                fileLlavePrivada,
                contrasena,
				
						
				function(certificado) {//Metodo que valida si la cadena orginal es una cadena vacia regresa la cadena
                	 inhabilitarBoton(true);
					 					 
                	 var cert = new PKI.SAT.Certificado(certificado);
                	 var serialNumber=cert.getNumeroSerie().replace(/ /g,"");
                     document.getElementById("numeroSerie").value=serialNumber;
                	 //document.getElementById("RFC").value=cert.getRFC().replace(/ /g,"");// se obtiene el RFC y se eliminan espacios en blanco
                	 var cadenaOriginal = document.getElementById("cadenaOriginal").value;
                   	 var rfcCert = cert.getRFC().replace(/ /g,"");
						
						if(!(vrfc.value.toUpperCase() == rfcCert.toUpperCase())){
								BANDERA_ERROR_RFC=false;								
						}				
																		 
					 if(isBlank(cadenaOriginal)){							
														
						cadenaOriginal = "|" + document.getElementById("RFC").value.trim().replace('&', '*') + "|" + document.getElementById("numeroSerie").value.trim() + "|"; 															
						//cadenaOriginal=document.getElementById("cadenaOriginal").value= "||" + document.getElementById("numeroSerie").value + "|UTF-8|" + document.getElementById("RFC").value + "|||";																	
                	    }
						
                    return cadenaOriginal;
                },
				
                function(error_code, certificado, firma, cadena_original) {//funcion callback
                    if (error_code === 0) {
                    	var cert = new PKI.SAT.Certificado(certificado);
                    	var certificadoVigente=cert.isVigente();
                  
							if(BANDERA_ERROR_RFC == false){
							BANDERA_ERROR_RFC =true;
							MuestraMensajes("alert alert-danger", "", "El RFC del certificado no corresponde al RFC proporcionado por usted." );
							window.scrollTo(0,0);
							return false;						
							}
					if(certificadoVigente){
                    		document.getElementById("cadenaOriginal").value = cadena_original;
                            document.getElementById("firmaDigital").value = firma;
                            document.getElementById("firmaDigital").click();
                        	BANDERA_ERROR_FIRMA = false;
							// AKI SERIA EL ENVIO
							window.document.frmApplet.submit();
                    	
                    	}else{
                    		//inhabilitarBoton(false);
                    		MuestraMensajes("alert alert-danger", "", "El certificado no es vigente")
							window.scrollTo(0,0);
							return false;
                    	}
                    } else {                    	
                    	BANDERA_ERROR_FIRMA = true;                        			
						var MsgError=PKI.SAT.FielUtil.obtenMensajeError(error_code) 																
						MuestraMensajes("alert alert-danger", "", "Ocurrió un error al validar : \n" + MsgError);
						window.scrollTo(0,0);
						return false;						
                    }
                }
        );
				

}


// Forma formslogin.asp   ********************************************************
function CamposLogin(event, opcion) {  
	var UsuarioEsc = document.getElementById('bUsername');
	var PasswordEsc = document.getElementById('Password');
	var btnEntrar = document.getElementById('submit1');
	var rowError = document.getElementById('rowError');
	var errUsername = document.getElementById('errUsername');
	var errPassword = document.getElementById('errPassword');
	
	rowError.style.display = 'none';
	errUsername.style.display = 'none';
	errPassword.style.display = 'none';
	UsuarioEsc.className = 'form-control smalltext';
	PasswordEsc.className = 'form-control smalltext';
	
	if (opcion == 1)
	{
		if (event.charCode == 0)
		{
			return true;
		}
		else
		{
			if (UsuarioEsc.value.length < 13)
			{
				return true;
			}
			return false;
		}
	}
	if (opcion == 2)
	{
		return true;
	}
}
function CamposLoginMayusculas(nPantalla) {  
	if (nPantalla == 1)
	{
		var UsuarioEsc = document.getElementById('bUsername');
		UsuarioEsc.value = UsuarioEsc.value.toUpperCase();
	}
	if (nPantalla == 2)
	{
		var UsuarioEsc = document.getElementById('RFC');
		UsuarioEsc.value = UsuarioEsc.value.toUpperCase();
	}
}
function ValidaEnvioLogin() {  
	var UsuarioEsc = document.getElementById('bUsername');
	var PasswordEsc = document.getElementById('Password');
	var rowError = document.getElementById('rowError');
	var errUsername = document.getElementById('errUsername');
	var errPassword = document.getElementById('errPassword');
	
	if (UsuarioEsc.value == '' || PasswordEsc.value == '')
	{
		rowError.style.display = 'block';
		if (UsuarioEsc.value == '')
		{
			errUsername.style.display = 'block';
			UsuarioEsc.className = 'form-control form-control-error';
		}
		if (PasswordEsc.value == '')
		{
			errPassword.style.display = 'block';
			PasswordEsc.className = 'form-control form-control-error';
		}
		window.scrollTo(0,0);
		return;
	}
	document.getElementById('Username').value=UsuarioEsc.value
	document.getElementById('bUsername').value=''
	window.document.frmLog.submit();
}

// Forma formsLoginFEA.asp ********************************************************
function CamposLogin_FEA(event, opcion) {  

	var UsuarioEsc = document.getElementById('RFC');
	var PasswordEsc = document.getElementById('privateKeyPassword');
	var rowError = document.getElementById('rowError');
	var errUsername = document.getElementById('errUsername');
	var errPassword = document.getElementById('errPassword');
	var errClavePrivada = document.getElementById('errClavePrivada');
	var errCertificado = document.getElementById('errCertificado');
	var errUsernameAs = document.getElementById('errUsernameAs');
	var errPasswordAs = document.getElementById('errPasswordAs');
	var errClavePrivadaAs = document.getElementById('errClavePrivadaAs');
	var errCertificadoAs = document.getElementById('errCertificadoAs');
	var valorLlav = document.getElementById('valorLlav');
	var valorCer = document.getElementById('valorCert');

	rowError.style.display = 'none';
	errUsername.style.display = 'none';
	errPassword.style.display = 'none';
	errClavePrivada.style.display = 'none';
	errCertificado.style.display = 'none';
	errUsernameAs.className = 'control-label';
	errPasswordAs.className = 'control-label';
	errClavePrivadaAs.className = 'control-label';
	errCertificadoAs.className = 'control-label';
	UsuarioEsc.className = 'form-control smalltext';
	PasswordEsc.className = 'form-control smalltext';
	valorLlav.className = "form-control fieldCFDi field ui-widget-content ui-corner-all";
	valorCer.className = "form-control fieldCFDi field ui-widget-content ui-corner-all";
	
	if (opcion == 1)
	{
		if (event.charCode == 0)
		{
			return true;
		}
		else
		{
			if (UsuarioEsc.value.length < 13)
			{
				return true;
			}
			return false;
		}
	}
	if (opcion == 2)
	{
		return true;
	}
  }
  
function SeleccionaArchivo(nTipoArchivo)
{
	var txtLlaPriv = document.getElementById('valorLlav');
	var txtCertif = document.getElementById('valorCert');
	var archivoCer = document.getElementById('certificate');
	var archivoKey = document.getElementById('privateKey');
	var sRutaArchivo = '';
    var txtRFC = document.getElementById('RFC');
	var compatibilidad = PKI.SAT.FielUtil.validaNavegador(archivoCer);
        if (compatibilidad === false) {
           alert('Navegador NO es compatible.' + String.fromCharCode(13) + 'Compatible con todos los navegadores excepto: Internet Explorer 5,6,7,8,9; Safari (IOS), En Internet Explorer Verificar compatibilidad con F12-Emulación');
		   window.scrollTo(0,0);
		   return false;
         }  else if (compatibilidad != true) {
        MuestraMensajes("alert alert-danger", "", "Ocurrió un error al validar el navegador: \n" + PKI.SAT.FielUtil.obtenMensajeError(compatibilidad));
		window.scrollTo(0,0);
		return false;
    }

	if (nTipoArchivo == 1)
	{
		archivoKey.click();
		sRutaArchivo=archivoKey.value
		txtLlaPriv.value = sRutaArchivo.replace(/fakepath/g,"");
		CamposLogin_FEA(3);
	}
	else if (nTipoArchivo == 2)
	{


		archivoCer.click();
		txtCertif.value = archivoCer.value; //.replace("fakepath\","");
		PKI.SAT.FielUtil.validaCertificado(
                archivoCer,                
				function(error_code, certificado) {//funcion callback
                    if (error_code === 0) {
						/////                	 
					 var cert = new PKI.SAT.Certificado(certificado);
                   	 var rfcCert = cert.getRFC().replace(/ /g,"");
						 txtRFC.value = rfcCert.toUpperCase();
						/////
							if(BANDERA_ERROR_RFC == false){
							BANDERA_ERROR_RFC =true;
							MuestraMensajes("alert alert-danger", "", "El RFC del certificado no corresponde al RFC proporcionado por usted." );
							window.scrollTo(0,0);
							return false;						
							}
                    }
                    
                });

		CamposLogin_FEA(4);
	}
	else
	{
		MuestraMensajes("alert alert-danger", "", "Configuración incorrecta, favor de salir y volver a entrar.")
		window.scrollTo(0,0);
		CamposLogin_FEA(5);
	}
}

function probando(aux)
{
	
 if (aux == 1){	
	 var datoCorrectoCert = document.getElementById("certificate").files[0].name;	
	 var valorCert = document.getElementById('valorCert');
	 valorCert.value = datoCorrectoCert;		
  }else if(aux == 2){	
	var datoCorrectoLlav = document.getElementById("privateKey").files[0].name;	
	var PrivateLlave = document.getElementById('valorLlav');
	PrivateLlave.value = datoCorrectoLlav;	
	}

}

function procesarFirmaFormulario(event) {
		if(BANDERA_ERROR_FIRMA) { 
			event.preventDefault();
		}else{
			//Se elimina el pwd para no enviarlo al server
			document.getElementById("privateKeyPassword").value = '';
		}
}


function inhabilitarBoton(valor){
	//$( "#btnSubmit" ).prop( "disabled", valor );
	 //setTimeout(function(){$('#'+boton).prop( "disabled", valor )}, 10);
}

function isBlank(str) {

    return (!str || /^\s*$/.test(str));
}
