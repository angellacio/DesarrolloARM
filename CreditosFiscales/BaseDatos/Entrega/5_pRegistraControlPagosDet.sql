USE MotorTraductor
GO
IF OBJECT_ID('pRegistraControlPagosDet', 'P') IS NOT NULL
DROP PROCEDURE pRegistraControlPagosDet;
GO


CREATE procedure pRegistraControlPagosDet
(
	@IdArchivo	bigint,
	@NumLinea	int,
	@Consecutivo	int,
	@LineaCaptura	varchar(25),
	@FechaPago	char(8),
	@HoraPago	char(5),
	@Importe	decimal(18,0),
	@NumOperacion	varchar(15),
	@MedioRecepcion	tinyint,
	@Version	smallint,
	@TipoPago	tinyint,
	@IdEstado	tinyint,
	@IdZip	bigint,
	@NombreXML	varchar(20), 
	@idProcesamiento uniqueidentifier = null
)
as
declare
	@vMensaje varchar(300),
	@vConsecutivo int 
set nocount on

	insert into tblControlPagosDet (IdArchivo, NumLinea, Consecutivo, LineaCaptura, FechaPago, HoraPago, Importe,
									NumOperacion, MedioRecepcion, Version, TipoPago, IdEstado, IdZip, NombreXML, IdError, IdProcesamiento)
	values(@IdArchivo, @NumLinea, @Consecutivo, @LineaCaptura, @FechaPago, @HoraPago,@Importe,
									@NumOperacion, @MedioRecepcion, @Version, @TipoPago, @IdEstado, @IdZip, @NombreXML, 0, @idProcesamiento )


	if @@ERROR <> 0 begin
		select @vMensaje = 'Error al registrar linea de captura ' + @LineaCaptura
		goto Errores
	end
set nocount off
return 0
Errores:
	set nocount off
	raiserror(@vMensaje,18,1)
	return -1
