USE [MotorTraductor]
GO


IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pConsultaReglas'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pConsultaReglas

END
GO

/****************************************************************************************
Autor:			Abril Hern�ndez Velasco
Fecha creaci�n: 20 Mayo de 2013
Descripci�n:	Obtiene la lista de reglas configuradas para una aplicaci�n y un tipo de
				documento.
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:			
Fecha modificaci�n:
Observaciones:
******************************************************************************************/
CREATE PROCEDURE [dbo].[pConsultaReglas](
	@pIdAplicacion		SMALLINT = 0
	,@pIdTipoDocPago	SMALLINT = 0
)
AS
BEGIN

	SET NOCOUNT ON;

	IF (
			SELECT 
				COUNT(1)
			FROM
				dbo.TblAdmonReglas
			WHERE
				IdAplicacion	= @pIdAplicacion
			AND	IdTipoDocPago	= @pIdTipoDocPago
		) = 0
	BEGIN
	
		SET @pIdAplicacion = 0;
	END

	SELECT 
		reg.IdRegla
		,Regla
		,Descripcion
		,EsValidacion
	FROM
		dbo.TblAdmonReglas	admon
	INNER JOIN
		dbo.CatReglas		reg
	ON	admon.IdRegla		= reg.IdRegla
	WHERE
		admon.IdAplicacion	= @pIdAplicacion
	AND	admon.IdTipoDocPago = @pIdTipoDocPago

	SET NOCOUNT OFF;


	
END