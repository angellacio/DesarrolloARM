USE MotorTraductor
GO
IF OBJECT_ID('pActualizaEstadoProceso', 'P') IS NOT NULL
DROP PROCEDURE pActualizaEstadoProceso;
GO

 CREATE procedure pActualizaEstadoProceso
(
	@pidProceso bigint,
	@pidArchivo bigint, 
	@pEstado tinyint
)
as

set nocount on

	update tblControlPagosHead set IdEstado = @pEstado, FechaProceso = getdate() where IdProceso = @pidProceso and idArchivo = @pidArchivo

set nocount off
return 0

