USE [MotorTraductor]
GO
/****** Object:  StoredProcedure [dbo].[pPagosSIATDetalleInsertar]    Script Date: 04/25/2017 12:50:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--RollBack TblSIATDetalleProcesoPagosSoloDyP
IF (select COUNT(*) from sys.tables t
inner join sys.columns c
on t.object_id=c.object_id
where t.name ='TblSIATDetalleProcesoPagosSoloDyP')=9
begin
ALTER TABLE [dbo].[TblSIATDetalleProcesoPagosSoloDyP]
drop column	[LineaCaptura] ,
	        [IdTipoDocumento],
	        [IdTipoLinea] ,
	        [ImporteTotalPagar],
	        [FechaPago] 
end
GO
--RollBack PagosSIATDetalleInsertar
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
	 insert into dbo.TblSIATDetalleProcesoPagosSoloDyP--No se tiene IdProcesamiento ya que no pertenece a Motor
	 values(@pIdProceso,@pNombreArchivo,@vConsecutivo,@pContenido)
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
go
--Respaldo pPagosSolicitudActualizar
ALTER procedure [dbo].[pPagosSolicitudActualizar]
(
	@pIdSolicitud uniqueidentifier,
	@pFechaPago datetime = null
)
as

if @pFechaPago is null
	set @pFechaPago = getdate();

set nocount on
	update TblDatosGenerales 
	set IdEstatus = 2, FechaPago = @pFechaPago
	where IdProcesamiento = @pIdSolicitud
set nocount off
return 0
GO
--Respaldo CatPasosProcesos
IF (select COUNT(*) from TblBitacoraProcesamiento where IdPasoProceso in (16,17,18,19))<>0
begin 
PRINT('No aplica rollback debido a que ya se tienen datos operativos en la tabla TblBitacoraProcesamiento')
end
else
begin
delete CatPasosProcesos 
where IdPasoProceso in (16,17,18,19)
update CatPasosProcesos SET descripcion='RecibePeticion'where IdPasoProceso=0;
update CatPasosProcesos SET descripcion='ObtieneReglas'where IdPasoProceso=1;
update CatPasosProcesos SET descripcion='ValidaXSDEntrada'where IdPasoProceso=2;
update CatPasosProcesos SET descripcion='AplicaReglasAntesCanonico'where IdPasoProceso=3;
update CatPasosProcesos SET descripcion='CompletaTransformacionCanonica'where IdPasoProceso=4;
update CatPasosProcesos SET descripcion='ValidaConceptoPeriodicidadPorTipoPersona'where IdPasoProceso=5;
update CatPasosProcesos SET descripcion='Agrupa conceptos'where IdPasoProceso=6;
update CatPasosProcesos SET descripcion='BuscaLineaExistenteBD'where IdPasoProceso=7;
update CatPasosProcesos SET descripcion='InsertaDatosCanonicoDB'where IdPasoProceso=8;
update CatPasosProcesos SET descripcion='AplicaReglasDespuesCanonico'where IdPasoProceso=9;
update CatPasosProcesos SET descripcion='CompletaEsquemaDyP'where IdPasoProceso=10;
update CatPasosProcesos SET descripcion='ValidaXSDSalida'where IdPasoProceso=11;
update CatPasosProcesos SET descripcion='OrdenamientoTransacciones'where IdPasoProceso=12;
update CatPasosProcesos SET descripcion='EnviadoDyP'where IdPasoProceso=13;
update CatPasosProcesos SET descripcion='ActualizaDatosDB'where IdPasoProceso=14;
update CatPasosProcesos SET descripcion='GeneraPDF'where IdPasoProceso=15;
end

select * from CatPasosProcesos