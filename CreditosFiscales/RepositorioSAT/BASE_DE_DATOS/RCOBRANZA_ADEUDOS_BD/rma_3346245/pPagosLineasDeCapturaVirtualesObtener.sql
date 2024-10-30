USE [MotorTraductor]
GO

ALTER procedure [dbo].[pPagosLineasDeCapturaVirtualesObtener]
as
set nocount on

select top 2000 L.IdProcesamiento,L.LineaCaptura,L.ImporteTotalPagar as ImporteTotal,
L.IdEstatus as IdEstadoSolicitud,L.RFC,L.IdTipoPersona,L.FechaEmision as FechaSolicitud,L.ClaveAlr as IdALR,
IdTipoDocumento,IdTipoPersona,NumeroParcialidad, L.IdAplicacion
from TblDatosGenerales L 
where IdTipoDocumento not in (30,40) and IdEstatus = 1 and ImporteTotalPagar = 0 and FolioDyP is not null
set nocount off
return 0 

GO


