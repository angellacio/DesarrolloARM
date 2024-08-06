<<<<<<< HEAD

CREATE OR ALTER procedure pRegistraArchivoZip
=======
USE MotorTraductor
GO
IF OBJECT_ID('pRegistraArchivoZip', 'P') IS NOT NULL
DROP PROCEDURE pRegistraArchivoZip;
GO

CREATE procedure pRegistraArchivoZip
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
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
