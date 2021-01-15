var PatronRutaArchivo = "([A-Za-z]:\\\\[^/:\\*\\?<>\\|]+\\.(dec|DEC))|(\\\\{2}[^/:\\*\\?<>\\|]+\\.(dec|DEC))";
var TeclaF5 = 116;

function ValidarRutaArchivo() 
{
    debugger;
    
    var archivo = document.getElementById('ctl00_ContentPlaceHolder1_CargaFileUpload');
    
    if( archivo != null )
    {
        var regex = new RegExp(PatronRutaArchivo);
        
        return regex.test(archivo.value);
    }
    
    return false;
}

function CargarManejadorEventoTeclado()
{
    debugger;
    
    if (document.all)
    { 
       document.onkeydown = ManejadorEventoTeclado;
    }
    else if (document.layers || document.getElementById)
    { 
       document.onkeypress = ManejadorEventoTeclado;
    }
}

function ManejadorEventoTeclado(evt) 
{ 
    debugger;
    var oEvent = (window.event) ? window.event : evt; 
    var nKeyCode = oEvent.keyCode ? oEvent.keyCode : oEvent.which ? oEvent.which : void 0; 
    var bIsFunctionKey = false; 
    var bRet = false;
    
    if (oEvent.charCode == null || oEvent.charCode == 0)
    { 
        bIsFunctionKey = (nKeyCode == TeclaF5)
    }
    
    window.status ='';
    
    if (bIsFunctionKey)
    { 
        bRet = false; 
        
        try 
        { 
            oEvent.returnValue = false; 
            oEvent.cancelBubble = true; 
            
            if (document.all)
            { //IE 
               oEvent.keyCode = 0;
            }else
            { //NS 
                oEvent.preventDefault();
                oEvent.stopPropagation();
            }
            
            window.status = "El F5 está desactivado en está página."; 
        }
         catch(ex)
        { 
        }
    }
    
    return bRet; 
}

