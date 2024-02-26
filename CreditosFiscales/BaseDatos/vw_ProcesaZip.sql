create view vw_ProcesaZip
AS
	select idProceso, NombreArchivo,Contenido, reenviar, 1 as origen from dbo.TblSIATDetalleProcesoPagos
union all
	select idProceso, NombreArchivo,Contenido, 0 as reenviar, 2 as origen from dbo.TblSIATDetalleProcesoPagosSoloDyP


