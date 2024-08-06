<<<<<<< HEAD

-- drop procedure pObtenerMonitorTProgramada 

/****** Object:  StoredProcedure pObtenerMonitorTProgramada
SYE Enero 2024 - AMA
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
=======
USE MotorTraductor
GO
IF OBJECT_ID('pObtenerMonitorTProgramada') > 0 drop procedure pObtenerMonitorTProgramada 
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
GO
Create procedure pObtenerMonitorTProgramada 
@pTipoPago int,
@pIdEstado int,
@pFechaIni datetime,--[FechaProceso]
@pFechaFin datetime --[FechaProceso]
as
set nocount on

-- Por FechaProceso
If @pTipoPago =-1 and @pIdEstado=-1
Begin
<<<<<<< HEAD
SELECT TipoPago, Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion DescEstado, H.FechaPago, FechaProceso, 
      D.IdZip, NombreZip, Z.Importe, NumPagos, H.IdError, E.Descripcion DescError
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	   left outer join CatErrorProceso E on H.IdError=E.IdError
	   inner join CatEstadoProceso Es on H.IdEstado = IdEstatus
	where FechaProceso between @pFechaIni and @pFechaFin;
=======
	SELECT TipoPago, Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion DescEstado, H.FechaPago, FechaProceso, 
		null IdZip, null NombreZip, null Importe, null NumPagos, H.IdError, E.Descripcion DescError
	FROM tblControlPagosHead H inner join 
		 tblControlPagosDet D on H.idArchivo=D.IdArchivo left outer join 
		 /*tblArchivosZIP Z on D.IdZip=Z.IdZip left outer join */
		 CatErrorProceso E on H.IdError=E.IdError inner join 
		 CatEstadoProceso Es on H.IdEstado = Es.IdEstatus
	WHERE FechaProceso between @pFechaIni and @pFechaFin
	GROUP BY TipoPago, IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion, H.FechaPago, FechaProceso, 
		H.IdError, E.Descripcion;
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
End;
-- 1 TipoPago
If @pTipoPago <>-1 and @pIdEstado=-1
Begin
<<<<<<< HEAD
SELECT TipoPago, Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion DescEstado, H.FechaPago, FechaProceso, 
      D.IdZip, NombreZip, Z.Importe, NumPagos, H.IdError, E.Descripcion DescError
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	   left outer join CatErrorProceso E on H.IdError=E.IdError
	   inner join CatEstadoProceso Es on H.IdEstado = IdEstatus
	where FechaProceso between @pFechaIni and @pFechaFin
	  and TipoPago = @pTipoPago;
=======
	SELECT TipoPago, Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion DescEstado, H.FechaPago, FechaProceso, 
		null IdZip, null NombreZip, null Importe, null NumPagos, H.IdError, E.Descripcion DescError
	FROM tblControlPagosHead H inner join 
		 tblControlPagosDet D on H.idArchivo=D.IdArchivo left outer join 
		 /*tblArchivosZIP Z on D.IdZip=Z.IdZip left outer join */
		 CatErrorProceso E on H.IdError=E.IdError inner join 
		 CatEstadoProceso Es on H.IdEstado = Es.IdEstatus
	WHERE FechaProceso between @pFechaIni and @pFechaFin and TipoPago = @pTipoPago
	GROUP BY TipoPago, IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion, H.FechaPago, FechaProceso, 
		H.IdError, E.Descripcion;
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
End;
-- 2 TipoPago IdEstado 
If @pTipoPago <>-1 and @pIdEstado<>-1
Begin
<<<<<<< HEAD
SELECT TipoPago, Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion DescEstado, H.FechaPago, FechaProceso, 
      D.IdZip, NombreZip, Z.Importe, NumPagos, H.IdError, E.Descripcion DescError
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	   left outer join CatErrorProceso E on H.IdError=E.IdError
	   inner join CatEstadoProceso Es on H.IdEstado = IdEstatus
	where FechaProceso between @pFechaIni and @pFechaFin
	  and TipoPago = @pTipoPago and H.IdEstado=@pIdEstado;
=======
	SELECT TipoPago, Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion DescEstado, H.FechaPago, FechaProceso, 
		null IdZip, null NombreZip, null Importe, null NumPagos, H.IdError, E.Descripcion DescError
	FROM tblControlPagosHead H inner join 
		 tblControlPagosDet D on H.idArchivo=D.IdArchivo left outer join 
		 /*tblArchivosZIP Z on D.IdZip=Z.IdZip left outer join */
		 CatErrorProceso E on H.IdError=E.IdError inner join 
		 CatEstadoProceso Es on H.IdEstado = Es.IdEstatus
	WHERE FechaProceso between @pFechaIni and @pFechaFin and TipoPago = @pTipoPago and H.IdEstado=@pIdEstado
	GROUP BY TipoPago, IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion, H.FechaPago, FechaProceso, 
		H.IdError, E.Descripcion;
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
End;
-- 3  IdEstado 
If @pTipoPago =-1 and @pIdEstado<>-1
Begin
<<<<<<< HEAD
SELECT TipoPago, Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
      IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion DescEstado, H.FechaPago, FechaProceso, 
      D.IdZip, NombreZip, Z.Importe, NumPagos, H.IdError, E.Descripcion DescError
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
       left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	   left outer join CatErrorProceso E on H.IdError=E.IdError
	   inner join CatEstadoProceso Es on H.IdEstado = IdEstatus
	where FechaProceso between @pFechaIni and @pFechaFin
	  and H.IdEstado=@pIdEstado;
=======
	SELECT TipoPago, Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
		IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion DescEstado, H.FechaPago, FechaProceso, 
		null IdZip, null NombreZip, null Importe, null NumPagos, H.IdError, E.Descripcion DescError
	FROM tblControlPagosHead H inner join 
		 tblControlPagosDet D on H.idArchivo=D.IdArchivo left outer join 
		 /*tblArchivosZIP Z on D.IdZip=Z.IdZip left outer join */
		 CatErrorProceso E on H.IdError=E.IdError inner join 
		 CatEstadoProceso Es on H.IdEstado = Es.IdEstatus
	WHERE FechaProceso between @pFechaIni and @pFechaFin and H.IdEstado=@pIdEstado
	GROUP BY TipoPago, IdProceso, NombreArchivo, H.IdEstado,Es.Descripcion, H.FechaPago, FechaProceso, 
		H.IdError, E.Descripcion;
>>>>>>> b654aaca5e4f06d59d4b780c42a90456eaefbb5b
End;
set nocount off

