
-- drop procedure pObtenerMonitorArchivoZIP 

/****** Object:  StoredProcedure pObtenerMonitorArchivoZIP
SYE Enero 2024 - AMA
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure pObtenerMonitorArchivoZIP 
@pTipoPago int,
@pNombreZIP varchar(25),
@pFechaIni datetime,--[FechaCreacion]
@pFechaFin datetime --[FechaCreacion]
as
set nocount on
-- Nombre ZIP
If @pNombreZIP <> '' 
Begin                
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
  FROM tblControlPagosDet D
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where  NombreZip=@pNombreZIP;
End;
-- Por FechaCreacion
If @pTipoPago =-1 
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
  FROM tblControlPagosDet D
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaCreacion between @pFechaIni and @pFechaFin;
End;
-- 1 TipoPago
If @pTipoPago <>-1 
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
  FROM tblControlPagosDet D
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaCreacion between @pFechaIni and @pFechaFin
	  and TipoPago = @pTipoPago;
End;

set nocount off



