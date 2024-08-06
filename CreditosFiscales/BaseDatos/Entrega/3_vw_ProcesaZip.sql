<<<<<<< HEAD
=======
USE MotorTraductor
GO
IF OBJECT_ID('vw_ProcesaZip', 'V') IS NOT NULL
DROP VIEW vw_ProcesaZip
GO

>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
create view vw_ProcesaZip
AS
	select idProceso, NombreArchivo,Contenido, reenviar, 1 as origen from dbo.TblSIATDetalleProcesoPagos
union all
	select idProceso, NombreArchivo,Contenido, 0 as reenviar, 2 as origen from dbo.TblSIATDetalleProcesoPagosSoloDyP


