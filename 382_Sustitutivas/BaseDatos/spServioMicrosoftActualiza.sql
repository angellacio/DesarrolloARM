--DROP PROCEDURE spServioMicrosoftActualiza;

CREATE PROCEDURE "ocpnpg".spServioMicrosoftActualiza(
   p_nIdSusWSM INT, 
   p_nRenglones INT,
   p_nGEstatus INT,  
   p_sPagado nVarChar(2), 
   p_sLineaCaptura nVarChar(20),
   p_sTipoOperacion nVarChar(2),
   p_sMensaje nVarChar(250),
   p_sNumOperBanco nVarChar(14),
   p_sFOperacion nVarChar(10),
   p_sHOperacion nVarChar(5),
   p_sNumOpeDecla nVarChar(14),
   p_sPresentacion nVarChar(8),
   p_dModificacion DATETIME YEAR TO SECOND,
   p_nEstatus Int, 
   p_dOperacion DATETIME YEAR TO SECOND,
   p_dPresentacion DATETIME YEAR TO SECOND)
{
--static char drversion[]="@(#) : STORE PROCEDURES : spServioMicrosiftActualiza.sql"
--*******************************************************************************************************
-- Programa:            spServioMicrosiftActualiza
-- Modulo:              Sustitutivas Microsoft
-- Descripcion:         Actualiza renglones a procesar servicio Microsoft.
-- Autor del Programa:  Angel Ramirez Mancera
-- Fecha:               28/Julio/2020
-- Modificacion:
-- Nota:
--*******************************************************************************************************
}
	DEFINE pa_nConsecutivo                 Integer;
   --SET DEBUG FILE TO '/tmp/spServioMicrosiftActualiza.txt';
   --TRACE ON;
   IF (p_nGEstatus = 2) THEN
      UPDATE m_cbSerSustitutivas 
         SET dModificacion = p_dModificacion, nEstatus = p_nGEstatus, nRenglones = p_nRenglones
      WHERE nID = p_nIdSusWSM;
   END IF
   IF (p_nGEstatus = -1) THEN
      
	  SELECT CASE WHEN COUNT(nConsecutivo) IS NULL THEN 1 ELSE COUNT(nConsecutivo) + 1 END INTO pa_nConsecutivo
	  FROM d_cbSerSustitutivasRes 
	  WHERE nID = p_nIdSusWSM;
	  
      INSERT INTO d_cbSerSustitutivasRes (nID, nConsecutivo, sPagado, sLineaCaptura, sTipoOperacion, sMensaje, sNumOperBanco, 
               sFOperacion, sHOperacion, sNumOpeDecla, sPresentacion, dModificacion, nEstatus, dOperacion, 
               dPresentacion)
         VALUES (p_nIdSusWSM, pa_nConsecutivo, p_sPagado, p_sLineaCaptura, p_sTipoOperacion, p_sMensaje, p_sNumOperBanco, 
               p_sFOperacion, p_sHOperacion, p_sNumOpeDecla, p_sPresentacion, p_dModificacion, p_nEstatus, p_dOperacion, 
               p_dPresentacion);
   END IF
   IF (p_nGEstatus = 3) THEN
       UPDATE m_cbSerSustitutivas 
         SET dModificacion = p_dModificacion, nIntentos = nIntentos + 1, nEstatus = p_nGEstatus, nRenglones = p_nRenglones, sEquipo = ''
      WHERE nID = p_nIdSusWSM;
   END IF
   --TRACE OFF;
END PROCEDURE;