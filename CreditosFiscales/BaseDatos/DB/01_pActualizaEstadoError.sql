
CREATE OR ALTER procedure pActualizaEstadoError
(
	@pidArchivo int, 
	@pEstado tinyint
)
as
set nocount on
	update tblControlPagosHead set IdEstado = 99,  IdError = @pEstado where idarchivo = @pidArchivo
set nocount off
return 0