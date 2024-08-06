<<<<<<< HEAD

CREATE OR ALTER procedure pActualizaEstadoProceso
=======
USE MotorTraductor
GO
IF OBJECT_ID('pActualizaEstadoProceso', 'P') IS NOT NULL
DROP PROCEDURE pActualizaEstadoProceso;
GO

 CREATE procedure pActualizaEstadoProceso
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
(
	@pidProceso bigint,
	@pidArchivo bigint, 
	@pEstado tinyint
)
as

set nocount on

<<<<<<< HEAD
	update tblControlPagosHead set IdEstado = @pEstado where IdProceso = @pidProceso and idArchivo = @pidArchivo
=======
	update tblControlPagosHead set IdEstado = @pEstado, FechaProceso = getdate() where IdProceso = @pidProceso and idArchivo = @pidArchivo
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b

set nocount off
return 0

