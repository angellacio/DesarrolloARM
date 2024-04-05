USE MotorTraductor
GO
IF OBJECT_ID('pPagosSIATDetalleInsertarNoProcesados', 'P') IS NOT NULL
DROP PROCEDURE pPagosSIATDetalleInsertarNoProcesados;
GO


CREATE procedure [dbo].[pPagosSIATDetalleInsertarNoProcesados]
(
	@pIdProceso bigint,
	@pNombreArchivo nvarchar(50),
	@pIdProcesamiento uniqueidentifier
)
as
declare
	@vMensaje varchar(300),
	@vMax int
set nocount on
     Select @vMax=count(Consecutivo)+1 from TblSIATDetalleNoProcesados where idProceso=@pIdProceso;
     
	 insert into dbo.TblSIATDetalleNoProcesados
	 values(@pIdProceso,@vMax,@pNombreArchivo,@pIdProcesamiento)

	update tblControlPagosDet set IdEstado = 99, IdError = 13 where IdProcesamiento =  @pIdProcesamiento

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
