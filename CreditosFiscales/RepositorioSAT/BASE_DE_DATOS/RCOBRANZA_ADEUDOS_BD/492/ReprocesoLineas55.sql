USE [MotorTraductor]
GO
begin 
declare @IdProceso bigint
declare @NombreArchivo nvarchar(50)
declare @Consecutivo int
declare @Contenido xml
declare @LineaCaptura varchar(20)
DECLARE @IdTipoDocumento smallint
declare @IdTipoLinea int
declare @ImporteTotalPagar bigint
declare @FechaPago datetime
declare @TipoDyP nchar(2)
declare @PagosProcesados bigint
declare @PagosTotal bigint

DECLARE SoloDyP CURSOR FOR
      SELECT IdProceso,NombreArchivo,Consecutivo,Contenido from dbo.TblSIATDetalleProcesoPagosSoloDyP
      where LineaCaptura is null
   SET @PagosProcesados  =1;
   SET @PagosTotal= (SELECT COUNT(*) FROM TblSIATDetalleProcesoPagosSoloDyP);
OPEN SoloDyP
FETCH SoloDyP into @IdProceso,@NombreArchivo,@Consecutivo,@Contenido
  WHILE (@@FETCH_STATUS = 0 )
  BEGIN 
  IF (@PagosProcesados%10000=0)
  BEGIN
  SELECT 'Procesando '+CAST(@PagosProcesados AS VARCHAR(MAX))+ ' de ' + CAST(@PagosTotal AS VARCHAR(MAX))+' pagos.';
      --Print ('Procesando '+CAST(@PagosProcesados AS VARCHAR(MAX))+ ' de ' + CAST(@PagosTotal AS VARCHAR(MAX))+' pagos.');
  END
	select @LineaCaptura = T.item.value('.', 'varchar(20)')
	FROM   @Contenido.nodes('CreditosFiscales/DatosGenerales') AS T(item)
	select @IdTipoDocumento = T.item.value('.', 'smallint')
	FROM   @Contenido.nodes('CreditosFiscales/DatosGenerales/TipoDocumento') AS T(item)
    select @IdTipoLinea = T.item.value('.', 'int')
	FROM   @Contenido.nodes('CreditosFiscales/DatosGenerales/TipoLinea') AS T(item)
	select @ImporteTotalPagar = T.item.value('.', 'bigint')
	FROM   @Contenido.nodes('CreditosFiscales/DatosGenerales/PagoEfectivo/Importe') AS T(item)
	select @FechaPago = T.item.value('.', 'datetime')
	FROM   @Contenido.nodes('CreditosFiscales/DatosGenerales/PagoEfectivo/FechaPago') AS T(item)
    SET @TipoDyP =substring(@LineaCaptura,11,12)
	 
	 IF @TipoDyP='55' 
	 BEGIN
	    SET @IdTipoDocumento=10
	    SET @IdTipoLinea=1
	    SET @Contenido.modify('
	    replace value of (/CreditosFiscales/DatosGenerales/TipoDocumento/text())[1] 
	    with 10
	    ');
		SET @Contenido.modify('
		replace value of (/CreditosFiscales/DatosGenerales/TipoLinea/text())[1] 
		with 1
		');
	 END

      update TblSIATDetalleProcesoPagosSoloDyP
      SET Contenido=@Contenido,LineaCaptura=@LineaCaptura,IdTipoDocumento=@IdTipoDocumento,
      IdTipoLinea=@IdTipoLinea,ImporteTotalPagar=@ImporteTotalPagar,FechaPago=@FechaPago
      where IdProceso=@IdProceso and NombreArchivo=@NombreArchivo and Consecutivo=@Consecutivo
   SET @PagosProcesados=@PagosProcesados+1;
   FETCH SoloDyP into @IdProceso,@NombreArchivo,@Consecutivo,@Contenido
  END
CLOSE SoloDyP
DEALLOCATE SoloDyP
 	 select COUNT(*) [Líneas Nulas]  from TblSIATDetalleProcesoPagosSoloDyP 
 	 where LineaCaptura is null
end

      
