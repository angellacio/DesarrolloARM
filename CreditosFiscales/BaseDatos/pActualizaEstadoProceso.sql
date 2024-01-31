
CREATE OR ALTER procedure pActualizaEstadoProceso
(
	@pidProceso bigint,
	@pidArchivo char(12), 
	@pEstado tinyint
)
as

set nocount on

	update tblControlPagosHead set IdEstado = @pEstado where IdProceso = @pidProceso and NombreArchivo = @pidArchivo

set nocount off
return 0

