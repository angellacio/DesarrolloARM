<<<<<<< HEAD

CREATE OR ALTER procedure pRegistraControlPagosH
=======
USE MotorTraductor
GO
IF OBJECT_ID('pRegistraControlPagosH', 'P') IS NOT NULL
DROP PROCEDURE pRegistraControlPagosH;
GO


CREATE procedure pRegistraControlPagosH
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
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
