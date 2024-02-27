
CREATE OR ALTER   procedure [dbo].[pActualizaEstadoErrorLC]
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