
alter procedure [dbo].[pPagosSIATInfoArchivosObtener]
(
	@pIdProceso bigint
)
as
set nocount on

	select NombreArchivo,Contenido from dbo.TblSIATDetalleProcesoPagos
	where IdProceso = @pIdProceso or reenviar = 1
	union all
	select NombreArchivo,Contenido from dbo.TblSIATDetalleProcesoPagosSoloDyP
	where IdProceso = @pIdProceso
	order by NombreArchivo

	Update dbo.TblSIATDetalleProcesoPagos set reenviar = 0 where reenviar = 1 

set nocount off
return 0


