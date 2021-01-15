Option Explicit

'Objeto para ingresar a la raiz del web server
Dim objRoot, MAIN_WEB_PATH, oWShell, sParam

CONST MSTV_WS = "routerConfiguration"
CONST MSTV_APPPOOL = "DyP.Router.Config"
CONST IIS_ROOT = "IIS://LocalHost/W3SVC/1/ROOT"

ON ERROR RESUME NEXT

'Obtener la ruta de instalación del instalador
MAIN_WEB_PATH = Session.Property("CustomActionData")


'ELIMINAR EL DIRECTORIO VIRTUAL MAXCOM BILLING BRIDGE
CALL DELETE_VIRTUAL_DIRECTORY ( MSTV_WS )

IF (Err.Number <> 0) THEN

      MsgBox "No fue posible ELIMINAR el directorio virtual " & MSTV_WS & ". Err: " & Err.Description
      wscript.Quit

END IF

'ELIMINAR EL APP POOL PARA LOS DIRECTORIOS VIRTUALES DE LA APLICACION HTTP DE BIZTALK
CALL DELETE_APP_POOLS( MSTV_APPPOOL )

IF (Err.Number <> 0) THEN

      MsgBox "No fue posible ELIMINAR el APPLICATION POOL " & MSTV_APPPOOL & ". Err: " & Err.Number & " " & Err.Description
      wscript.Quit

END IF


SET objRoot = Nothing
SET oWShell = Nothing


'MsgBox "Termino el proceso de creación de Directorios Virtuales"


PUBLIC FUNCTION DELETE_VIRTUAL_DIRECTORY ( sVirtualDirName )

'Objecto que contendra la información de los directorios virutales
DIM objVDir, objRoot

ON ERROR RESUME NEXT

'MsgBox "Eliminando directorio virtual: " & sVirtualDirName 

SET objVDir = GetObject(IIS_ROOT & "/" & sVirtualDirName )

if ( IsObject(objVDir) ) then

    SET objRoot = GetObject(IIS_ROOT)

    objRoot.Delete "IIsWebVirtualDir", sVirtualDirName

end if 


if (Err.Number <> -2147024893 and Err.Number <> 0 ) then

      MsgBox "El directorio virtual " & sVirtualDirName & " no pudo ser eliminado. Err: " & Err.number & " " & Err.Description
      wscript.Quit
else
	Err.Clear
end if

END FUNCTION


PUBLIC FUNCTION DELETE_APP_POOLS( sAppPoolName )

'DELETE THE APP POOL TO USE

'Objectos para la creacion de los app pools
DIM objAppPools, objAppPool

ON ERROR RESUME NEXT

Set objAppPools = GetObject("IIS://localhost/W3SVC/AppPools")

objAppPools.Delete "IIsApplicationPool", sAppPoolName

IF (Err.Number <> -2147024893 and Err.Number <> 0) THEN
    MsgBox "El application pool " & sAppPoolName & " no pudo ser eliminado. Err: " & Err.Description
    wscript.Quit
ELSE
    Err.Clear
END IF 


SET objAppPool = Nothing
SET objAppPools = Nothing

END FUNCTION
