DROP PROCEDURE spServioMicrosoftObten;

CREATE PROCEDURE "ocpnpg".spServioMicrosoftObten(
   pe_nReglonesReserva Integer,
   pe_sEquipo nVarChar(250),
   pe_nIntentos Integer)
RETURNING Integer AS nId, nVarChar(13) AS RFC, DATETIME YEAR TO SECOND AS FechaRecepcion, Integer AS Ejercicio,
         nVarChar(3) AS Periodo, nVarChar(5) AS Banco, nVarChar(6) AS Concepto, DATETIME YEAR TO SECOND AS fModificacion,
         Integer AS nEstatus, nVarChar(250) AS sEquipo, Integer AS nIntentos
{
--static char drversion[]="@(#) : STORE PROCEDURES : spServioMicrosoftObten.sql"
--*******************************************************************************************************
-- Programa:            spServioMicrosoftObten
-- Modulo:              Sustitutivas Microsoft
-- Descripcion:         Consulta Reserva renglones a procesar servicio Microsoft.
-- Autor del Programa:  Angel Ramirez Mancera
-- Fecha:               28/Julio/2020
-- Modificacion:
-- Nota:
--*******************************************************************************************************
}
   
   DEFINE pa_nId                 Integer;
   DEFINE pa_RFC                 nVarChar(13);
   DEFINE pa_FechaRecepcion      DATETIME YEAR TO SECOND;
   DEFINE pa_Ejercicio           Integer;
   DEFINE pa_Periodo             nVarChar(3);
   DEFINE pa_Banco               nVarChar(5);
   DEFINE pa_Concepto            nVarChar(6);
   DEFINE pa_fModificacion       DATETIME YEAR TO SECOND;
   DEFINE pa_nEstatus            Integer;
   DEFINE pa_sEquipo             nVarChar(250);
   DEFINE pa_nIntentos           Integer;

   --SET DEBUG FILE TO '/tmp/spServioMicrosoftObten.txt';
   --TRACE ON;
   IF (pe_nReglonesReserva > 0) THEN
      UPDATE m_cbSerSustitutivas
      SET nEstatus = 1,
         dModificacion = sysdate,
         sEquipo = pe_sEquipo,
         nIntentos = 0
      WHERE nID in(SELECT nID
                   FROM (SELECT FIRST pe_nReglonesReserva nID
                         FROM m_cbSerSustitutivas
                         WHERE nEstatus IN (0, 3)
                         ORDER BY nID DESC) A);
   END IF
   FOREACH
      SELECT ss.nID, ss.sRFC, ss.dRecepcion, CASE WHEN ss.nEjercicio IS NULL THEN 0 ELSE ss.nEjercicio END, ss.sIDPeriodo, ss.sIDBanco, 
             CASE WHEN ic.sIDConcepto IS NULL THEN '000' ELSE ic.sIDConcepto END, ss.dModificacion, 
			 CASE WHEN ss.nEstatus IS NULL THEN 0 ELSE ss.nEstatus END, CASE WHEN ss.sEquipo IS NULL THEN '' ELSE ss.sEquipo END, 
			 CASE WHEN ss.nIntentos IS NULL THEN 0 ELSE ss.nIntentos END
        INTO pa_nId, pa_RFC, pa_FechaRecepcion, pa_Ejercicio, pa_Periodo, pa_Banco, pa_Concepto, pa_fModificacion,
            pa_nEstatus, pa_sEquipo, pa_nIntentos
      FROM m_cbSerSustitutivas ss LEFT OUTER JOIN
           c_cbInformaticaConcepto ic ON ss.nIDCveInformatica = ic.nIDCveInformatica
      WHERE sEquipo = pe_sEquipo AND nEstatus IN (1, 3) AND nIntentos < pe_nIntentos
      RETURN pa_nId, pa_RFC, pa_FechaRecepcion, pa_Ejercicio, pa_Periodo, pa_Banco, pa_Concepto, pa_fModificacion,
            pa_nEstatus, pa_sEquipo, pa_nIntentos WITH RESUME;
   END FOREACH;
   --TRACE OFF;
END PROCEDURE;
