declare @pTipoPago int = null, @pIdEstado int = null,
@pNumLinea varchar(25) = '', @pArchivoZIP varchar(25) = '',
@pIdBanco  int = null,
@pFechaIni datetime = null,
@pFechaFin datetime = null 

SET @pTipoPago = -1
SET @pIdEstado = 3
SET @pNumLinea = ''
SET @pArchivoZIP = '';
SET @pIdBanco = 40025
SET @pFechaIni = '01/01/2024 00:00:00'
SET @pFechaFin = '31/01/2024 23:59:59'

exec pObtenerMonitorPagoDetalle @pTipoPago, @pIdEstado, @pNumLinea, @pIdBanco, @pFechaIni, @pFechaFin
--exec pObtenerMonitorArchivoZIP @pTipoPago, '', @pFechaIni, @pFechaFin
--exec pObtenerMonitorTProgramada @pTipoPago, @pIdEstado, @pFechaIni, @pFechaFin




