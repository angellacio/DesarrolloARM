USE MotorTraductor
GO
IF OBJECT_ID('pObtenerMonitorArchivoZIP') > 0 DROP PROCEDURE pObtenerMonitorArchivoZIP;
GO
CREATE procedure pObtenerMonitorArchivoZIP (
@pTipoPago int,
@pNombreZIP varchar(25),
@pFechaIni datetime,--[FechaCreacion]
@pFechaFin datetime --[FechaCreacion]
)
as
set nocount on
-- Nombre ZIP
If @pNombreZIP <> '' 
Begin
	SELECT DISTINCT TipoPago,
		Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
	FROM tblControlPagosDet D
		left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	WHERE NombreZip=@pNombreZIP
	GROUP BY TipoPago, FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos;
End;
-- Por FechaCreacion
If @pTipoPago =-1 
Begin
	SELECT DISTINCT TipoPago,
		Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
	FROM tblControlPagosDet D
		left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	WHERE Z.FechaCreacion between @pFechaIni and @pFechaFin
	GROUP BY TipoPago, FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos;
End;
-- 1 TipoPago
If @pTipoPago <>-1 
Begin
	SELECT DISTINCT TipoPago,
		Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
	FROM tblControlPagosDet D
		left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	WHERE Z.FechaCreacion between @pFechaIni and @pFechaFin and TipoPago = @pTipoPago
	GROUP BY TipoPago, FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos;
End;

set nocount off



