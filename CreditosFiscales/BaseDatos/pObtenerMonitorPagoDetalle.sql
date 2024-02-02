
-- drop procedure pObtenerMonitorPagoDetalle 

/****** Object:  StoredProcedure pObtenerMonitorPagoDetalle
SYE Enero 2024 - AMA
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure pObtenerMonitorPagoDetalle 
@pTipoPago int,
@pIdEstado int,
@pNumLinea varchar(25),
@pIdBanco  int,
@pFechaIni datetime,--[FechaRegistro]
@pFechaFin datetime --[FechaRegistro]
as
set nocount on
-- Línea de Captura
If @pNumLinea <> '' 
Begin                
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where  LineaCaptura=@pNumLinea;
End;
-- Por FechaRegistro
If @pTipoPago =-1 and @pIdEstado =-1 and @pIdBanco  =-1
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaRegistro between @pFechaIni and @pFechaFin;
End;
-- 1 Tipo Pago
If @pTipoPago <>-1 and @pIdEstado =-1 and @pIdBanco  =-1
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaRegistro between @pFechaIni and @pFechaFin
	      and TipoPago = @pTipoPago;
End;
-- 2 Tipo Pago, IdEstado
If @pTipoPago <>-1 and @pIdEstado <>-1 and @pIdBanco  =-1
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaRegistro between @pFechaIni and @pFechaFin
	      and TipoPago = @pTipoPago and H.IdEstado = @pIdEstado;
End;
-- 3 Tipo Pago, IdEstado, IdBanco
If @pTipoPago <>-1 and @pIdEstado <>-1 and @pIdBanco  <>-1
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaRegistro between @pFechaIni and @pFechaFin
	      and TipoPago = @pTipoPago and H.IdEstado = @pIdEstado
		  and IdBanco=@pIdBanco;
End;
-- 4  IdEstado 
If @pTipoPago =-1 and @pIdEstado <>-1 and @pIdBanco  =-1
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaRegistro between @pFechaIni and @pFechaFin
	      and H.IdEstado = @pIdEstado;
End;
-- 5  IdEstado, cIdBanco
If @pTipoPago =-1 and @pIdEstado <>-1 and @pIdBanco  <>-1
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaRegistro between @pFechaIni and @pFechaFin
	      and H.IdEstado = @pIdEstado and IdBanco=@pIdBanco;
End;
-- 6 IdBanco
If @pTipoPago =-1 and @pIdEstado =-1 and @pIdBanco  <>-1
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaRegistro between @pFechaIni and @pFechaFin
	      and IdBanco=@pIdBanco;
End;
-- 7 Tipo Pago, IdBanco
If @pTipoPago <>-1 and @pIdEstado =-1 and @pIdBanco  <>-1
Begin
SELECT TipoPago,
      Case when TipoPago=1 then 'Físico' when TipoPago=2 then 'Virtual' End as TipoPagoDesc,
	   substring(D.FechaPago,7,2) + '/' + substring(D.FechaPago,5,2) + '/' + substring(D.FechaPago,1,4) as FechaPago, 
	   HoraPago, D.Importe, NumOperacion, LineaCaptura, MedioRecepcion, 
	   Case when MedioRecepcion=1 then ' Ventanilla bancaria'  when MedioRecepcion=3 then 'Internet' Else 'No Definido'  End as DescMedio, 
	   D.IdEstado, Es.Descripcion DescEstado , 
	   FechaProceso, NombreXML, D.IdZip, NombreZip, D.IdError, E.Descripcion DescError, IdBanco 
  FROM tblControlPagosHead H inner join tblControlPagosDet D
       on H.idArchivo=D.IdArchivo
	   left outer join CatErrorProceso E 
	                on D.IdError=E.IdError
	   inner join CatEstadoProceso Es on D.IdEstado = IdEstatus
	   left outer join tblArchivosZIP Z on D.IdZip=Z.IdZip
	where FechaRegistro between @pFechaIni and @pFechaFin
	      and TipoPago = @pTipoPago and IdBanco=@pIdBanco;
End;
set nocount off



