var _pendingRequest = false;

function HabilitarControlesAutenticacion()
{
    try
    {           
    
        if(document.applets!=null && document.applets[0] != null && document.getElementById('divContent')!=null)
        {        
            document.getElementById('divContent').style.visibility = 'visible';
        }
    }
    catch(e)
    {            
        
    }    
}

	 
	function Validate()
	{	
	    try
	{
	    if(_pendingRequest && document.getElementById("pendingLogin")!=null)
	    {
	        document.getElementById("pendingLogin").style.visibility="visible";
	    
	        return false;
	    }
	
	    var _datosFirma = document.getElementById("Firma");
	    _datosFirma.value = document.AppFEA.getSign();
	    
	    var _valorFirma = new String(_datosFirma.value);
	   
	    
	    if(_valorFirma == "*")
	    {
	        alert("Debe confirmar previamente sus datos.\nPresione el botón Confirmar para continuar.");
	    
	        return false;
	    }

        var _rfc = document.getElementById("RFC");
        _rfc.value = document.AppFEA.getRFC();
        
        var _cadena = document.getElementById("CadenaO");
        _cadena.value = document.AppFEA.getStringToSign();
        
        var _serie = document.getElementById("Serie");
        _serie.value = document.AppFEA.getSerialNumber();
        
        var _url = document.getElementById("URLoriginal");
        _url.value = "CargarArchivo.aspx";
        
        _pendingRequest = true;
        
        return true;	    	   	   		
        }
        catch(e)
        {
            alert("Debe confirmar previamente sus datos.\nPresione el botón Confirmar para continuar.");
            return false;
        }
        return false;
	}