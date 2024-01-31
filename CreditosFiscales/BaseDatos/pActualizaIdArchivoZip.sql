
CREATE OR ALTER procedure pActualizaIdArchivoZip
(
	
	@IdZip int,
	@LC as varchar(25),
	@NombreXML varchar(30)
)
as
declare
	@vMensaje varchar(300),
	@vConsecutivo int 
set nocount on

	UPDATE tblControlPagosDet set NombreXML = @NombreXML, IdZip = @IdZip, IdEstado = 5 
	WHERE LineaCaptura = @LC and IdEstado = 4 and IdError = 0 

	


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
