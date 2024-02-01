declare @pTipoPago int, @pIdEstado int,
@pNumLinea varchar(25),
@pIdBanco  int,
@pFechaIni datetime,
@pFechaFin datetime 

SET @pTipoPago = -1
SET @pIdEstado = -1
SET @pNumLinea = ''
SET @pIdBanco = -1
SET @pFechaIni = '01/01/2024 00:00:00'
SET @pFechaFin = '31/01/2024 23:59:59'

--exec pObtenerMonitorPagoDetalle @pTipoPago, @pIdEstado, @pNumLinea, @pIdBanco, @pFechaIni, @pFechaFin

exec pObtenerMonitorArchivoZIP @pTipoPago, '', @pFechaIni, @pFechaFin




