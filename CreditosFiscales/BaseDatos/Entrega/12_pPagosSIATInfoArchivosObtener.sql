
CREATE OR ALTER procedure [dbo].[pPagosSIATInfoArchivosObtener]
(
	@pIdProceso bigint, 
	@pagina int, 
	@registros int, 
	@tipoArchivo tinyint
)
as

set nocount on

if (@tipoArchivo = 1)
	begin 

		WITH OrderedTable AS
		(
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS row_number, *
			FROM vw_ProcesaZip
			where (IdProceso = @pIdProceso OR reenviar = 1) and NombreArchivo <> 'Virtual' 
		)
		SELECT NombreArchivo, Contenido
		FROM OrderedTable
		WHERE row_number BETWEEN (@pagina - 1) * @registros + 1 AND @pagina * @registros 
		order by NombreArchivo

		update dbo.TblSIATDetalleProcesoPagos set reenviar = 0 where reenviar = 1 and NombreArchivo <> 'Virtual' 

			-- Actualiza Estado a generando XML
		update tblControlPagosDet set IdEstado = 4
		WHERE IdArchivo in (select IdArchivo from tblControlPagosHead where IdProceso = @pIdProceso and NombreArchivo <> 'Virtual') and IdEstado = 3 and IdZip =  0

	END
ELSE
	BEGIN
		WITH OrderedTable AS
		(
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS row_number, *
			FROM vw_ProcesaZip
			where (IdProceso = @pIdProceso OR reenviar = 1) and NombreArchivo <> 'Virtual' 
		)
		SELECT NombreArchivo, Contenido
		FROM OrderedTable
		WHERE row_number BETWEEN (@pagina - 1) * @registros + 1 AND @pagina * @registros 
		order by NombreArchivo


		update dbo.TblSIATDetalleProcesoPagos set reenviar = 0 where reenviar = 1 and NombreArchivo = 'Virtual' 

				-- Actualiza Estado a generando XML
		update tblControlPagosDet set IdEstado = 4
		WHERE IdArchivo in (select IdArchivo from tblControlPagosHead where IdProceso = @pIdProceso and NombreArchivo = 'Virtual') and IdEstado = 3 and IdZip =  0

	END



set nocount off


GO
