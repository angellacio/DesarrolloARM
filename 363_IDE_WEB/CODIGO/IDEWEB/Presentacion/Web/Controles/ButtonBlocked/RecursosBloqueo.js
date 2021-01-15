function BloquearF5()
{
    if (document.all)
    { 
       document.onkeydown = MonitorearF5;
    }
    else if (document.layers || document.getElementById)
    { 
       document.onkeypress = MonitorearF5;
    }
}

function MonitorearF5(evt)
{
    var evento = (window.event) ? window.event : evt; 
    var codigo = evento.keyCode ? evento.keyCode : evento.which ? evento.which : void 0;
    
    //F5 = 116
    if ((evento.charCode == null || evento.charCode == 0) && codigo == 116)
    { 
        window.status = '';
        window.defaultStatus = '';
        
        try 
        { 
            if (document.all) //IE 
            { 
               evento.cancelBubble = true;
               evento.returnValue = false; 
               evento.keyCode = 0;
            }
            else //NS
            { 
                //Equivalente a: cancelBubble
                if(evento.stopPropagation)
                {
                    evento.stopPropagation();
                }
                
                //Equivalente a: returnValue
                if(evento.preventDefault)
                {
                    evento.preventDefault();
                }
            }
            
            window.status = "El F5 está desactivado en está página.";
            window.defaultStatus = "El F5 está desactivado en está página.";
        }
        catch(e)
        { 
        }
        
        return false;
    }
    
    return true;
}


function ValidarRutaArchivo(mensajeRutaRequerida, mensajeRutaInvalida)
{
    var control = document.getElementById("ctl00_ContentPlaceHolder1_CargaFileUpload");
    var resultado = false;
    
    if(control)
    {
        var rutArchivo = control.value.replace(/^\s+|\s+$/g, '');
        
        if( rutArchivo == '' )
        {
            alert(mensajeRutaRequerida);
        }
        else
        {
            var regex = /^([A-Za-z]:\\|\\{2})[^\/:\*\?<>\|]+$/;
            
            if( !(resultado = regex.test(control.value)) )
            {
                alert(mensajeRutaInvalida.replace('{0}', rutArchivo));
            }
        }
    }
    
    return resultado;
}    

function DeshabilitarEnviar()
{   
    var control = document.getElementById("ctl00_ContentPlaceHolder1_enviarButton");
        
    if(control)
    {
        control.disabled = 'true';
    }
}

function MostrarEnviando()
{        
    var divEnviando = document.getElementById("LyrUploading");
    
    if (divEnviando)
    {
        divEnviando.style.visibility="visible";
    }                    
}

function Enviar(mensajeRutaRequerida, mensajeRutaInvalida)
{
    if(ValidarRutaArchivo(mensajeRutaRequerida, mensajeRutaInvalida))
    {
        __doPostBack("ctl00$ContentPlaceHolder1$enviarButton", '');
        DeshabilitarEnviar();
        MostrarEnviando();
    }
    else
    {
        return false;
    }
}

function EnviarEncripta() {
    __doPostBack("ctl00$ContentPlaceHolder1$enviarEncriptaButton", '');
    
        MostrarEnviando();
    
}