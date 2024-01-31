
CREATE OR ALTER   procedure [dbo].[pActualizaEstadoError]
(
	@pidArchivo int, 
	@pEstado tinyint
)
as
set nocount on
	update tblControlPagosHead set IdEstado = 99,  IdError = @pEstado where idarchivo = @pidArchivo
	update tblControlPagosDet set IdEstado = 99, IdError = 99 where idarchivo = @pidArchivo
set nocount off
return 0
GO


