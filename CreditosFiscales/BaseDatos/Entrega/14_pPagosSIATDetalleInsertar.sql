<<<<<<< HEAD
--Actualiza PagosSIATDetalleInsertar
CREATE OR ALTER procedure [dbo].[pPagosSIATDetalleInsertar]
=======
USE MotorTraductor
GO
IF OBJECT_ID('pPagosSIATDetalleInsertar', 'P') IS NOT NULL
DROP PROCEDURE pPagosSIATDetalleInsertar;
GO


CREATE procedure [dbo].[pPagosSIATDetalleInsertar]
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
(
	@pIdProceso bigint,
	@pNombreArchivo nvarchar(50),
	@pIdProcesamiento uniqueidentifier,
	@pContenido XML
)
as
declare
	@vMensaje varchar(300),
	@vConsecutivo int,
	@idArchivo int,
    @LineaCaptura varchar(20) 
set nocount on  
    if(@pIdProcesamiento is not null)
    begin
		select @vConsecutivo=isnull(max(Consecutivo),0)+1 from dbo.TblSIATDetalleProcesoPagos
		where IdProceso = @pIdProceso
		 insert into dbo.TblSIATDetalleProcesoPagos (
		 IdProceso, NombreArchivo, Consecutivo, IdProcesamiento, Contenido, reenviar)
		 values(@pIdProceso,@pNombreArchivo,@vConsecutivo,@pIdProcesamiento,@pContenido, 0)

		-- Actualiza tabla de control
		select @LineaCaptura = T.item.value('.', 'varchar(20)')
		FROM   @pContenido.nodes('CreditosFiscales/DatosGenerales/LineaCaptura') AS T(item)

	end
	else
	begin 
		select @vConsecutivo=isnull(max(Consecutivo),0)+1 from dbo.TblSIATDetalleProcesoPagosSoloDyP
		where IdProceso = @pIdProceso
	--	DECLARE @LineaCaptura varchar(20)
		DECLARE @TipoDocumento smallint
		DECLARE @TipoLinea int
		DECLARE @ImporteTotalPagar bigint
		DECLARE @FechaPago datetime
	
		select @LineaCaptura = T.item.value('.', 'varchar(20)')
		FROM   @pContenido.nodes('CreditosFiscales/DatosGenerales') AS T(item)
		select @TipoDocumento = T.item.value('.', 'smallint')
		FROM   @pContenido.nodes('CreditosFiscales/DatosGenerales/TipoDocumento') AS T(item)
		select @TipoLinea = T.item.value('.', 'int')
		FROM   @pContenido.nodes('CreditosFiscales/DatosGenerales/TipoLinea') AS T(item)
		select @ImporteTotalPagar = T.item.value('.', 'bigint')
		FROM   @pContenido.nodes('CreditosFiscales/DatosGenerales/PagoEfectivo/Importe') AS T(item)
		select @FechaPago = T.item.value('.', 'datetime')
		FROM   @pContenido.nodes('CreditosFiscales/DatosGenerales/PagoEfectivo/FechaPago') AS T(item)
	      
		insert into dbo.TblSIATDetalleProcesoPagosSoloDyP--No se tiene IdProcesamiento ya que no pertenece a Motor
		values(@pIdProceso,@pNombreArchivo,@vConsecutivo,@pContenido,@LineaCaptura,@TipoDocumento,@TipoLinea,@ImporteTotalPagar,@FechaPago)

		PRINT (@LineaCaptura)
		PRINT (CONVERT(varchar(22),@TipoDocumento))
		PRINT (CONVERT(varchar(22),@TipoLinea))
		PRINT (@ImporteTotalPagar)
		PRINT (@FechaPago)
	end 


	if @pNombreArchivo <> 'Virtual'
	begin 
		select @idArchivo = idArchivo from tblControlPagosHead where IdProceso = @pIdProceso and NombreArchivo = SUBSTRING(ltrim(rtrim(@pNombreArchivo)), 2, 12)
				update tblControlPagosDet set IdEstado = 3, IdProcesamiento = @pIdProcesamiento  where IdArchivo = @idArchivo and LineaCaptura = @LineaCaptura
	end
	else
	begin 
		select @idArchivo = idArchivo from tblControlPagosHead where IdProceso = @pIdProceso and NombreArchivo = @pNombreArchivo
		update tblControlPagosDet set IdEstado = 3where IdArchivo = @idArchivo and LineaCaptura = @LineaCaptura
	end





	if @@ERROR <> 0 begin
		select @vMensaje = 'Error al insertar contenido de archivos'
		goto Errores
	end
set nocount off
return 0
Errores:
	set nocount off
	raiserror(@vMensaje,18,1)
	return -1
GO


