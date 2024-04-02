IF OBJECT_ID('pActualizaEstadoError', 'P') IS NOT NULL
DROP PROCEDURE pActualizaEstadoError;
GO


CREATE procedure [dbo].[pActualizaEstadoError]
(
	@pidArchivo int, 
	@pEstado tinyint, 
	@idProceso bigint = 0
)
as
set nocount on

	if @pidArchivo > 0
	begin 
		update tblControlPagosHead set IdEstado = 99,  IdError = @pEstado where idarchivo = @pidArchivo
		update tblControlPagosDet set IdEstado = 99, IdError = 99 where idarchivo = @pidArchivo
	end
	else
	-- Actualiza todos los archivos faltanes de procesar 
	begin
		update tblControlPagosDet set IdEstado = 99, IdError = @pEstado
		WHERE IdArchivo in (select IdArchivo from tblControlPagosHead where IdProceso = @idProceso and IdEstado in (2))

		update tblControlPagosHead set IdEstado = 99,  IdError = @pEstado where idArchivo in
				(select IdArchivo from tblControlPagosHead where IdProceso = @idProceso) and IdEstado in (2,3)
	end
set nocount off
return 0
GO


