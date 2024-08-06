<<<<<<< HEAD

-- drop procedure pObtenerMonitorArchivoZIP 

/****** Object:  StoredProcedure pObtenerMonitorArchivoZIP
SYE Enero 2024 - AMA
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure pObtenerMonitorArchivoZIP 
=======
USE MotorTraductor
GO
IF OBJECT_ID('pObtenerMonitorArchivoZIP') > 0 DROP PROCEDURE pObtenerMonitorArchivoZIP;
GO
CREATE procedure pObtenerMonitorArchivoZIP (
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
@pTipoPago int,
@pNombreZIP varchar(25),
@pFechaIni datetime,--[FechaCreacion]
@pFechaFin datetime --[FechaCreacion]
<<<<<<< HEAD
=======
)
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
as
set nocount on
-- Nombre ZIP
If @pNombreZIP <> '' 
<<<<<<< HEAD
Begin                
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
  FROM tblControlPagosDet D
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where  NombreZip=@pNombreZIP;
=======
Begin
	SELECT DISTINCT TipoPago,
		Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
	FROM tblControlPagosDet D
		left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	WHERE NombreZip=@pNombreZIP
	GROUP BY TipoPago, FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos;
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
End;
-- Por FechaCreacion
If @pTipoPago =-1 
Begin
<<<<<<< HEAD
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
  FROM tblControlPagosDet D
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaCreacion between @pFechaIni and @pFechaFin;
=======
	SELECT DISTINCT TipoPago,
		Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
	FROM tblControlPagosDet D
		left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	WHERE Z.FechaCreacion between @pFechaIni and @pFechaFin
	GROUP BY TipoPago, FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos;
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
End;
-- 1 TipoPago
If @pTipoPago <>-1 
Begin
<<<<<<< HEAD
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
  FROM tblControlPagosDet D
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaCreacion between @pFechaIni and @pFechaFin
	  and TipoPago = @pTipoPago;
=======
	SELECT DISTINCT TipoPago,
		Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos
	FROM tblControlPagosDet D
		left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	WHERE Z.FechaCreacion between @pFechaIni and @pFechaFin and TipoPago = @pTipoPago
	GROUP BY TipoPago, FechaCreacion, D.IdZip, NombreZip, Z.Importe, NumPagos;
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
End;

set nocount off



