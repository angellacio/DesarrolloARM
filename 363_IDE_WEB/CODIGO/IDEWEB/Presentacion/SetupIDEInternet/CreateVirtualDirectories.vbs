Option Explicit

'Objeto para ingresar a la raiz del web server
Dim objRoot, MAIN_WEB_PATH, oWShell, sParam, oParams, sUser, sPwd

CONST MSTV_WS = "IdeInternet"
CONST MSTV_APPPOOL = "Scade.Net.Ide.Internet"

ON ERROR RESUME NEXT

'Obtener la ruta de instalación del instalador
MAIN_WEB_PATH = Session.Property("CustomActionData")

'Dividir los parametros que vienen del instalador, se encuentra
'separador por pipes ( | )

'oParams = Split(MAIN_WEB_PATH, "|", -1, 1 )

'Asignacion de valores.
'MAIN_WEB_PATH = oParams(0)
'sUser         = oParams(1)
'sPwd          = oParams(2)

SET objRoot = GetObject("IIS://LocalHost/W3SVC/1/ROOT")

IF (Err.Number <> 0) THEN

      MsgBox "El servidor Web no se encuentra disponible. Err: " & Err.Description
      wscript.Quit
END IF

'****************WEB SERVICE********************
'CREAR EL DIRECTORIO VIRTUAL
CALL CREATE_VIRTUAL_DIRECTORY ( objRoot, MSTV_WS, MAIN_WEB_PATH)
'**************END WEB SERVICE******************
IF (Err.Number <> 0) THEN

      MsgBox "No fue posible crear el directorio virtual " & MSTV_WS & ". Err: " & Err.Description
      wscript.Quit

END IF

Set oWShell = CreateObject ("WScript.Shell")

IF (Err.Number <> 0) THEN

      MsgBox "No fue posible crear el objeto WScript.Shell(). Err: " & Err.Number & " " & Err.Description
      wscript.Quit

END IF

'CREAR EL APP POOL PARA LOS DIRECTORIOS VIRTUALES DE LA APLICACION HTTP DE BIZTALK
CALL CREATE_APP_POOLS( MSTV_APPPOOL , sUser, sPwd )

IF (Err.Number <> 0) THEN

      MsgBox "No fue posible crear el APPLICATION POOL " & MSTV_APPPOOL & ". Err: " & Err.Number & " " & Err.Description
      wscript.Quit

END IF


'ASIGNAR EL POOL CREADO A LOS DIRECTORIOS VIRTUALES
CALL ASSIGN_APP_POOL ( MSTV_WS , MSTV_APPPOOL )
IF (Err.Number <> 0) THEN

      MsgBox "Error al asignar el application pool '" & MSTV_APPPOOL & "' en el virtual directory '" & MSTV_RESET_VD & "'. Err: " & Err.Number & " " & Err.Description
      wscript.Quit

END IF


SET objRoot = Nothing
SET oWShell = Nothing


'MsgBox "Termino el proceso de creación de Directorios Virtuales"


PUBLIC FUNCTION CREATE_VIRTUAL_DIRECTORY ( oRootWS, sVirtualDirName, sAppFolder )

'Objecto que contendra la información de los directorios virutales
DIM objVDir

ON ERROR RESUME NEXT

Set objVDir = oRootWS.Create("IIsWebVirtualDir", sVirtualDirName)

objVDir.Path = sAppFolder

objVDir.AccessRead = True

objVDir.AccessWrite = False

objVDir.AccessScript = True

objVDir.AccessExecute = True

objVDir.EnableDirBrowsing = False

objVDir.EnableDefaultDoc = True

objVDir.DefaultDoc = "Login.aspx"

objVDir.AspEnableParentPaths = true

'Integrated Security
'AuthFlags Settings 
'1 ---  Anonymous access 
'2 --- Basic Authentication (password is sent in clear text) 
'4 --- Integrated Windows Authentication 
'5 --- AuthNTLM + AuthAnonymous
'16 -- Digest authentication for Windows domain servers 
objVDir.AuthFlags = 1

objVDir.SetInfo

'CREAR LA APLICACION
CALL CREATE_APPLICATION ( sVirtualDirName )

if (Err.Number <> 0) then

      MsgBox "El directorio virtual " & sVirtualDirName & " no pudo ser creado. Err: " & Err.Description
      wscript.Quit

end if

SET objVDir = Nothing


END FUNCTION

PUBLIC FUNCTION CREATE_APP_POOLS( sAppPoolName, sUserName, sPassword )

'CREATE THE APP POOL TO USE

'Objectos para la creacion de los app pools
DIM objAppPools, objAppPool

ON ERROR RESUME NEXT

Set objAppPools = GetObject("IIS://localhost/W3SVC/AppPools")

Set objAppPool = objAppPools.Create("IIsApplicationPool", sAppPoolName )

'***********************************IDENTITY******************************
' 0 = Local System
' 1 = Local Service
' 2 = Network Service
' 3 = Custom Identity -> also set WAMUserName and WAMUserPass
objAppPool.AppPoolIdentityType = 3
Dim IIsObject
Set IIsObject = GetObject ("IIS://localhost/w3svc")
objAppPool.WAMUserName = IIsObject.Get("WAMUserName")
objAppPool.WAMUserPass = IIsObject.Get("WAMUserPass")
objAppPool.LogonMethod = 1 
Set IIsObject = Nothing

' Note: WamUserName must be added to the IIS_WPG group to have correct security rights
'objAppPool.WAMUserName = "DOMAIN\Username"
'objAppPool.WAMUserPass = "Password"
'objAppPool.WAMUserName = sUserName
'objAppPool.WAMUserPass = sPassword
'objAppPool.LogonMethod = 1 

'***********************************PERFORMANCE*****************************
objAppPool.IdleTimeout = 20
objAppPool.AppPoolQueueLength = 4000
objAppPool.MaxProcesses = 1

'***********************************HEALTH**********************************
objAppPool.PingInterval = 30
objAppPool.RapidFailProtection = true
objAppPool.RapidFailProtectionInterval = 5
objAppPool.RapidFailProtectionMaxCrashes = 5
objAppPool.ShutdownTimeLimit = 3600

objAppPool.SetInfo

IF (Err.Number <> 0) THEN
    MsgBox "El application pool " & sAppPoolName & " no pudo ser creado. Err: " & Err.Description
    wscript.Quit
END IF 

SET objAppPool = Nothing
SET objAppPools = Nothing

END FUNCTION


PUBLIC FUNCTION CREATE_APPLICATION( sApplicationName )

'Objecto que contrandrá la aplicacion
DIM objIIS

ON ERROR RESUME NEXT

SET objIIS = GetObject("IIS://localhost/W3SVC/1/ROOT/" & sApplicationName)

'Create a process pooled process
objIIS.AppCreate2 (2)

objIIS.Put "AppFriendlyName", sApplicationName

objIIS.SetInfo

IF (Err.Number <> 0) THEN

	MsgBox "Error al crear la aplicacion en 'IIS://LocalHost/W3SVC/1/ROOT/" & sApplicationName & "'. Err: " & Err.Description
	WScript.Quit

END IF

SET objIIS = Nothing

END FUNCTION


PUBLIC FUNCTION ASSIGN_APP_POOL ( sVDirName , sAppPoolName )
	DIM objVDir

	ON ERROR RESUME NEXT

	SET objVDir = GetObject("IIS://localhost/W3SVC/1/ROOT/" & sVDirName )

	objVDir.AppPoolId = sAppPoolName

	objVDir.SetInfo

	IF (Err.Number <> 0) THEN

		MsgBox "Error al asignar el application pool '" & sAppPoolName & "' en el virtual directory '" & sVDirName & "'. Err: " & Err.Number & " " & Err.Description
		WScript.Quit
	END IF

	SET objVDir = Nothing
END FUNCTION
