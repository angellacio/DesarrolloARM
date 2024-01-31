
CREATE OR ALTER procedure pObtenerArchivoQProcesar
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

	select idarchivo, IdProceso, 'Q' + NombreArchivo as NombreArchivo,IdBanco, FechaPago, NumRegistros, Importe, FechaRegistro, Idestado, NombreEquipo 
	from tblControlPagosHead where idarchivo = @vIdArchivo

set nocount off
return 0
