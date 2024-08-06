<<<<<<< HEAD

CREATE OR ALTER procedure pActualizaIdArchivoZip
=======
USE MotorTraductor
GO
IF OBJECT_ID('pActualizaIdArchivoZip', 'P') IS NOT NULL
DROP PROCEDURE pActualizaIdArchivoZip;
GO


CREATE procedure pActualizaIdArchivoZip
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
(
	
	@IdZip int,
	@LC as varchar(25),
	@NombreXML varchar(30),
	@idProceso bigint,
	@final tinyint,
	@TipoPago tinyint,
	@NumOperacion varchar(15) = null
)
as
declare
	@vMensaje varchar(300),
	@vConsecutivo int
set nocount on


  --- verificar que el registro exista sino se va a tener que insertar con los datos basicos

	if @TipoPago = 1
		UPDATE tblControlPagosDet set NombreXML = @NombreXML, IdZip = @IdZip, IdEstado = 5 
		WHERE LineaCaptura = @LC and IdEstado = 4 and IdError = 0 and 
		IdArchivo in (select IdArchivo from tblControlPagosHead where IdProceso =  @idProceso)
	else
		UPDATE tblControlPagosDet set NombreXML = @NombreXML, IdZip = @IdZip, IdEstado = 5 
		WHERE NumOperacion  = @NumOperacion and IdEstado = 4 and IdError = 0 and 
		IdArchivo in (select IdArchivo from tblControlPagosHead where IdProceso =  @idProceso)



	if @final = 1
	begin
		declare @NumPagos smallint,
			@Importe bigint

		select @NumPagos = count(idarchivo) from tblControlPagosDet where IdZip = @IdZip

		SELECT @Importe = SUM(CAST(Importe AS BIGINT)) 
		FROM tblControlPagosDet
		WHERE ISNUMERIC(Importe) = 1 and IdZip = @IdZip

		update tblArchivosZIP  set NumPagos = @NumPagos, Importe = @Importe 
		WHERE  IdZip = @IdZip
	end


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
