<<<<<<< HEAD

CREATE OR ALTER procedure pObtenerArchivoQProcesar
=======
USE MotorTraductor
GO
IF OBJECT_ID('pObtenerArchivoQProcesar', 'P') IS NOT NULL
DROP PROCEDURE pObtenerArchivoQProcesar;
GO


CREATE procedure pObtenerArchivoQProcesar
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
(
	@pIdProceso bigint,
	@NombreEquipo varchar(50)
)
as
declare
	@vIdArchivo bigint
set nocount on

	select top 1 @vIdArchivo = idarchivo from tblControlPagosHead where IdProceso = @pIdProceso  and NombreEquipo is null and  Idestado = 1

	update tblControlPagosHead set NombreEquipo = @NombreEquipo, Idestado = 2, FechaProceso = getdate() where idarchivo = @vIdArchivo
	update tblControlPagosDet set IdEstado = 2 where IdArchivo = @vIdArchivo

	select idarchivo, IdProceso, 'Q' + NombreArchivo as NombreArchivo,IdBanco, FechaPago, NumRegistros, Importe, FechaRegistro, Idestado, NombreEquipo 
	from tblControlPagosHead where idarchivo = @vIdArchivo


set nocount off
return 0
