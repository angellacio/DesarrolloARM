
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
		select NombreArchivo,Contenido from dbo.TblSIATDetalleProcesoPagos
		where (IdProceso = @pIdProceso or reenviar = 1) and NombreArchivo <> 'Virtual' 
		union all
		select NombreArchivo,Contenido from dbo.TblSIATDetalleProcesoPagosSoloDyP
		where IdProceso = @pIdProceso and NombreArchivo <> 'Virtual' 
		order by NombreArchivo
	OFFSET @pagina ROWS
	FETCH NEXT @registros ROWS ONLY;

	update dbo.TblSIATDetalleProcesoPagos set reenviar = 0 where reenviar = 1 and NombreArchivo <> 'Virtual' 

		-- Actualiza Estado a generando XML
	update tblControlPagosDet set IdEstado = 4
	WHERE IdArchivo in (select IdArchivo from tblControlPagosHead where IdProceso = @pIdProceso and NombreArchivo <> 'Virtual') and IdEstado = 3 and IdZip =  0

END
ELSE
BEGIN
		select NombreArchivo,Contenido from dbo.TblSIATDetalleProcesoPagos
		where (IdProceso = @pIdProceso or reenviar = 1) and NombreArchivo = 'Virtual' 
		union all
		select NombreArchivo,Contenido from dbo.TblSIATDetalleProcesoPagosSoloDyP
		where IdProceso = @pIdProceso and NombreArchivo = 'Virtual' 
		order by NombreArchivo
	OFFSET @pagina ROWS
	FETCH NEXT @registros ROWS ONLY;


		update dbo.TblSIATDetalleProcesoPagos set reenviar = 0 where reenviar = 1 and NombreArchivo = 'Virtual' 

			-- Actualiza Estado a generando XML
	update tblControlPagosDet set IdEstado = 4
	WHERE IdArchivo in (select IdArchivo from tblControlPagosHead where IdProceso = @pIdProceso and NombreArchivo <> 'Virtual') and IdEstado = 3 and IdZip =  0

END



set nocount off


GO
