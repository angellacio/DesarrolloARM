USE MotorTraductor
GO
IF OBJECT_ID('pActualizaEstadoErrorLC', 'P') IS NOT NULL
DROP PROCEDURE pActualizaEstadoErrorLC;
GO

CREATE procedure [dbo].[pActualizaEstadoErrorLC]
(
	@pidProceso bigint,
	@pidArchivo int, 
	@pLC varchar(25),
	@pIdError tinyint

)
as
set nocount on
		update tblControlPagosDet set IdEstado = 99, IdError = @pIdError where idarchivo = @pidArchivo and LineaCaptura = @pLC
set nocount off
return 0
GO