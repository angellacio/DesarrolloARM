IF OBJECT_ID('pRegistraArchivoZip', 'P') IS NOT NULL
DROP PROCEDURE pRegistraArchivoZip;
GO

CREATE procedure pRegistraArchivoZip
(
	@pNombreArchivo nvarchar(18)
)
as
declare
	@vMensaje varchar(300),
	@vConsecutivo int 
set nocount on

	insert into tblArchivosZIP (NombreZip, FechaCreacion)
	values(@pNombreArchivo, GETDATE())

--	select @vConsecutivo =  max(IdZip) from tblArchivosZIP

	select SCOPE_IDENTITY()

--	select @@IDENTITY  as IdZip

	


	if @@ERROR <> 0 begin
		select @vMensaje = 'Error al registrar archivo '
		goto Errores
	end
set nocount off
return 0

Errores:
	set nocount off
	raiserror(@vMensaje,18,1)
	return -1
