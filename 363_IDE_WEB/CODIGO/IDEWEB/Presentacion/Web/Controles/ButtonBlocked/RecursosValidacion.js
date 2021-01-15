﻿
var _validaFirma = false;
var _validaEncripta = false;

    function validaStrArchivo() {
        var strPath = document.getElementById("ctl00_ContentPlaceHolder1_CargaFileUpload").value       
        

        var bandera = false;
        var strNarchivo = checkPath(strPath);
        var strValidacion = "";

        if (strNarchivo.length == 28 && checkExt(strNarchivo)) {
            bandera = true;
        } else {
            strValidacion += "0|";
        }

        if (!checkTpDec(strNarchivo)) {
            strValidacion += "1|";
        }

        if (!validarRFC(strNarchivo)) {
            strValidacion += "2|";
        }

        if (!checkMes(strNarchivo)) {
            strValidacion += "3|";
        }

        if (!checkAnio(strNarchivo) || !validaAnio(strNarchivo)) {
            strValidacion += "4|";
        }

        if (!checkIdentificador(strNarchivo)) {
            strValidacion += "5|";
        } else {

            if (!checkPeriodoNormal(strNarchivo)) {
                strValidacion += "5|";
            }
        }

        if (!checkNdecl(strNarchivo)) {
            strValidacion += "6|";
        } /*else {

            if (!checkPeriodoNormal(strNarchivo)) {
               // alert(arrMensaje[6]); //NOTA: agregar otro mensaje...
                strValidacion += "6|";
            }
        }*/
      //  alert(strValidacion):
        document.getElementById("ctl00_ContentPlaceHolder1_HValidacion").value = strValidacion;       
    }

    function checkPeriodoNormal(strNarchivo) {
        var identificador = strNarchivo.substring(21, 22);
        var periodo = strNarchivo.substring(19, 21);
        var mesAnual = strNarchivo.substring(0, 3);
        if (identificador == "N" && periodo == "00" && mesAnual == "DIA") {
            return true;
        } else if (identificador == "C" && periodo == "00" && mesAnual == "DIA") {
            return true;
        } else if (identificador == "C" && periodo != "00" && mesAnual == "DIM") {
            return true;
        } else if (identificador == "N" && periodo != "00" && mesAnual == "DIM") {
            return true;
        } else {
            return false;
        }
    }



    function validaAnio(strNarchivo) {
        var strAnio = strNarchivo.substring(15, 19);
        var f = new Date();
        var anioActual = f.getFullYear();
        if (strAnio <= anioActual) {
            return true;
        } else {
            return false;
        }

    }

    function checkPath(strPath) {
        var archivo = strPath.split(/\\/g)
        var nArchivo = archivo[archivo.length - 1];
        return nArchivo;

    }

    function checkExt(strNarchivo) {//use in a form event or ina input
        var value = strNarchivo;
        if (!value.match(/.(xml)|(zip)$/)) {//here your extensions
            return false;
        }
        else {//right extension
            return true;
        }
    }

    function checkTpDec(strNarchivo) {
        var value = strNarchivo.substring(0, 3);
        if (!value.match(/(DIM)|(DIA)$/)) {//here your tpDeclaracion
            return false;
        }
        else {
            return true;
        }
    }

    function checkMes(strNarchivo) {
        var value = strNarchivo.substring(19, 21);
        if (!value.match(/[0][0-9]|[1][0-2]$/)) {//here your Month
            return false;
        }
        else {
            return true;
        }
    }

    function checkAnio(strNarchivo) {
        var value = strNarchivo.substring(15, 19);
        if (!value.match(/[1-9]/)) {
            return false;
        }
        else {
            return true;
        }
    }

    function checkIdentificador(strNarchivo) {
        var value = strNarchivo.substring(21, 22);
        if (!value.match(/(N)|(C)$/)) {
            return false;
        }
        else {
            return true;
        }
    }

    function checkNdecl(strNarchivo) {
        var value = strNarchivo.substring(22, 24);
        if (!value.match(/[0-9]{2}$/)) {
            return false;
        }
        else {
            return true;
        }
    }

    function validarRFC(strNarchivo) {
        var value = strNarchivo.substring(3, 15);
        if (!value.match(/(([A-Z]|[a-z]|[Ñ]|[ñ]|[&]){3})([0-9]{2})([0][1-9]|[1][0-2])([0][1-9]|[12][0-9]|3[01])(([A-Z]|[a-z]|[0-9]){3})/)) {
            return false;
        }
        else {
            return true;
        }

    }

    function ValidaEncripta() {
        try {
            

            if (!(_validaFirma))  {

                var _datosFirma = document.getElementById("ctl00_ContentPlaceHolder1_Firma");
                var _FEA= document.getElementById("ctl00_ContentPlaceHolder1_appFEA");
                _datosFirma.value = _FEA.getSign();
                var _valorFirma = _datosFirma.value;



                if (_valorFirma == "*") {
                    alert("Debe confirmar previamente sus datos.\nPresione el botón Confirmar para continuar.");

                    return false;
                } else {
                    _validaFirma = true;

                }

                
            }


            if (!(_validaEncripta)) {
                var _encriptar = document.getElementById("ctl00_ContentPlaceHolder1_Encriptar");
                var _FEA = document.getElementById("ctl00_ContentPlaceHolder1_appFEA");
                document.getElementById("ctl00_ContentPlaceHolder1_archivoEncripta").value = _FEA.getNombreArchivo();
                _encriptar.value = _FEA.getEncriptado();
                var _valorEncriptar = _encriptar.value;
                if (_valorEncriptar == "*") {
                    alert("Debe encriptar previamente sus datos.\nPresione el boton Encriptar para continuar.");

                    return false;
                } else {

                    _validaEncripta = true;
                }
            }
            return true;
        }
        catch (e) {
            alert("Debe confirmar previamente sus datos.\nPresione el botón Confirmar para continuar." + e.Message);
            return false;
        }
        return false;
    }
