
CREATE OR ALTER procedure pRegistraControlPagosH
(
	@pIdProceso bigint,
	@pNombreArchivo nvarchar(100),
	@pIdBanco int,
	@pFechaPago nvarchar(8),
	@pNumRegistros int,
	@pimporte decimal(18,0)
)
as
declare
	@vMensaje varchar(300),
	@vConsecutivo int 
set nocount on

	insert into tblControlPagosHead (IdProceso, NombreArchivo,IdBanco, FechaPago, NumRegistros, Importe, FechaRegistro, Idestado, IdError )
	values(@pIdProceso, @pNombreArchivo, @pIdBanco, @pFechaPago, @pNumRegistros, @pimporte, GETDATE(), 1, 0)

	--select max(idArchivo) from tblControlPagosHead
	select SCOPE_IDENTITY()

	if @@ERROR <> 0 begin
		select @vMensaje = 'Error al registrar archivo '
		goto Errores
	end
set nocount off
return @vConsecutivo

Errores:
	set nocount off
	raiserror(@vMensaje,18,1)
	return -1
