USE [MotorTraductor]
GO
/****** Object:  StoredProcedure [dbo].[pPagosSIATDetalleInsertar]    Script Date: 04/25/2017 12:50:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Actualiza TblSIATDetalleProcesoPagosSoloDyP
IF (select COUNT(*) from sys.tables t
inner join sys.columns c
on t.object_id=c.object_id
where t.name ='TblSIATDetalleProcesoPagosSoloDyP')<>9
begin
ALTER TABLE [dbo].[TblSIATDetalleProcesoPagosSoloDyP]
ADD	[LineaCaptura] [varchar](20) NULL,
	[IdTipoDocumento] [smallint]  NULL,
	[IdTipoLinea] [int]  NULL,
	[ImporteTotalPagar] [bigint]  NULL,
	[FechaPago] [datetime]  NULL
end
GO
PRINT('TblSIATDetalleProcesoPagosSoloDyP')
GO
--Actualiza PagosSIATDetalleInsertar
ALTER procedure [dbo].[pPagosSIATDetalleInsertar]
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
	 insert into dbo.TblSIATDetalleProcesoPagos
	 values(@pIdProceso,@pNombreArchivo,@vConsecutivo,@pIdProcesamiento,@pContenido)
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
PRINT('pPagosSIATDetalleInsertar')
GO
--Actualiza pPagosSolicitudActualizar
ALTER procedure [dbo].[pPagosSolicitudActualizar]
(
	@pIdSolicitud uniqueidentifier,
	@pFechaPago datetime = null
)
as

if @pFechaPago is null
	set @pFechaPago = getdate();
	
	if(select COUNT(*) from TblDatosGenerales where IdProcesamiento=@pIdSolicitud and ImporteTotalPagar=0)=1
	begin
	--Si es virtual la fecha de fecha de pago es la fecha en que se emitio la linea de captura
	SET @pFechaPago	=(select FechaEmision from TblDatosGenerales  where IdProcesamiento=@pIdSolicitud)
	end

set nocount on
	update TblDatosGenerales 
	set IdEstatus = 2, FechaPago = @pFechaPago
	where IdProcesamiento = @pIdSolicitud
set nocount off
return 0
GO
PRINT('pPagosSolicitudActualizar')
GO
--CatPasosProcesos
IF (select COUNT(*) from CatPasosProcesos where IdPasoProceso=16)=0
begin 
insert into CatPasosProcesos values (16,'dummy');
end
IF (select COUNT(*) from CatPasosProcesos where IdPasoProceso=17)=0
begin 
insert into CatPasosProcesos values (17,'dummy');
end
IF (select COUNT(*) from CatPasosProcesos where IdPasoProceso=18)=0
begin 
insert into CatPasosProcesos values (18,'dummy');
end
IF (select COUNT(*) from CatPasosProcesos where IdPasoProceso=19)=0
begin 
insert into CatPasosProcesos values (19,'dummy');
end
update CatPasosProcesos SET descripcion='AplicaReglasAntesCanonicoSIAT'where IdPasoProceso=4;
update CatPasosProcesos SET descripcion='AplicaReglasAntesCanonico'where IdPasoProceso=5;
update CatPasosProcesos SET descripcion='CompletaTransformacionCanonica'where IdPasoProceso=6;
update CatPasosProcesos SET descripcion='CompletaTransformacionCanonicaSIAT'where IdPasoProceso=7;
update CatPasosProcesos SET descripcion='ValidaConceptoPeriodicidadPorTipoPersona'where IdPasoProceso=8;
update CatPasosProcesos SET descripcion='Agrupa conceptos'where IdPasoProceso=9;
update CatPasosProcesos SET descripcion='BuscaLineaExistenteBD'where IdPasoProceso=10;
update CatPasosProcesos SET descripcion='InsertaDatosCanonicoDB'where IdPasoProceso=11;
update CatPasosProcesos SET descripcion='InsertaDatosCanonicoDBSIAT'where IdPasoProceso=12;
update CatPasosProcesos SET descripcion='AplicaReglasDespuesCanonico'where IdPasoProceso=13;
update CatPasosProcesos SET descripcion='CompletaEsquemaDyP'where IdPasoProceso=14;
update CatPasosProcesos SET descripcion='ValidaXSDSalida'where IdPasoProceso=15;
update CatPasosProcesos SET descripcion='OrdenamientoTransacciones'where IdPasoProceso=16;
update CatPasosProcesos SET descripcion='EnviadoDyP'where IdPasoProceso=17;
update CatPasosProcesos SET descripcion='ActualizaDatosDB'where IdPasoProceso=18;
update CatPasosProcesos SET descripcion='GeneraPDF'where IdPasoProceso=19;
select * from CatPasosProcesos
GO
