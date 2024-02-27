
CREATE OR ALTER procedure pActualizaEstadoProceso
(
	@pidProceso bigint,
	@pidArchivo bigint, 
	@pEstado tinyint
)
as

set nocount on

	update tblControlPagosHead set IdEstado = @pEstado where IdProceso = @pidProceso and idArchivo = @pidArchivo

set nocount off
return 0

