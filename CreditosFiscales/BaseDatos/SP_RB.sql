USE [MotorTraductor]
GO

drop procedure pRegistraControlPagosH;
drop procedure pRegistraControlPagosDet
drop procedure pObtenerArchivoQProcesar;
drop procedure pActualizaEstadoProcesoLC;
drop procedure pActualizaEstadoError;
drop procedure pActualizaEstadoProceso;
drop procedure pRegistraArchivoZip;
drop procedure pActualizaIdArchivoZip;
drop procedure pObtenerMonitorPagoDetalle;
drop procedure pObtenerMonitorArchivoZIP;
drop procedure pObtenerMonitorTProgramada;

GO

alter procedure [dbo].[pPagosSIATInfoArchivosObtener]
(
	@pIdProceso bigint
)
as
set nocount on

	select NombreArchivo,Contenido from dbo.TblSIATDetalleProcesoPagos
	where IdProceso = @pIdProceso or reprocesar = 1
	union all
	select NombreArchivo,Contenido from dbo.TblSIATDetalleProcesoPagosSoloDyP
	where IdProceso = @pIdProceso
	order by NombreArchivo

	Update dbo.TblSIATDetalleProcesoPagos set reprocesar = 0 where reprocesar = 1 

set nocount off
return 0


GO


CREATE OR ALTER procedure [dbo].[pPagosSIATDetalleInsertarNoProcesados]
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


CREATE OR ALTER procedure [dbo].[pPagosSIATDetalleInsertar]
(
	@pIdProceso bigint,
	@pNombreArchivo nvarchar(50),
	@pIdProcesamiento uniqueidentifier,
	@pContenido XML
)
as
declare
	@vMensaje varchar(300),
	@vConsecutivo int 
set nocount on  
    if(@pIdProcesamiento is not null)
    begin
    select @vConsecutivo=isnull(max(Consecutivo),0)+1 from dbo.TblSIATDetalleProcesoPagos
	where IdProceso = @pIdProceso
	 insert into dbo.TblSIATDetalleProcesoPagos (
	 IdProceso, NombreArchivo, Consecutivo, IdProcesamiento, Contenido, reenviar)
	 values(@pIdProceso,@pNombreArchivo,@vConsecutivo,@pIdProcesamiento,@pContenido, 0)
	end
	else
	begin 
	select @vConsecutivo=isnull(max(Consecutivo),0)+1 from dbo.TblSIATDetalleProcesoPagosSoloDyP
	where IdProceso = @pIdProceso
	DECLARE @LineaCaptura varchar(20)
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

/****** Object:  StoredProcedure [dbo].[pBitacoraEventosCatalogo]    Script Date: 25/01/2024 11:04:25 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[pBitacoraEventosCatalogo] @pIdCatalogo INT
AS
  BEGIN
      IF ( @pIdCatalogo = 1 )
        SELECT IdAplicacion AS valor,
               Nombre       AS Descripcion
        FROM   CatAplicacion

      IF ( @pIdCatalogo = 2 )
        SELECT IdTipoDocumento AS valor,
               Nombre          AS Descripcion
        FROM   CatTipoDocumento

	  IF ( @pIdCatalogo = 3 )
        SELECT [IdPasoProceso] AS valor,
               [Descripcion] AS Descripcion
        FROM   CatPasosProcesos
  END





