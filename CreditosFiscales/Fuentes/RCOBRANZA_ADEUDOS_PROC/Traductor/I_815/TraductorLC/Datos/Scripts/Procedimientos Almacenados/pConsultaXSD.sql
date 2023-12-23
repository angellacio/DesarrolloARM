USE [MotorTraductor]
GO

IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pConsultaXSD'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pConsultaXSD

END
GO

/****************************************************************************************
Autor:			Abril Hern�ndez Velasco
Fecha creaci�n: 20 Mayo de 2013
Descripci�n:	Obtiene la el contrato configurado por aplicaci�n, tipo de documento y
				la direcci�n del mismo.
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:			
Fecha modificaci�n:
Observaciones:
******************************************************************************************/
CREATE PROCEDURE [dbo].[pConsultaXSD](
	@pIdAplicacion		SMALLINT = 0
	,@pIdTipoDocPago	SMALLINT = 0
	,@pDireccion		TINYINT
)
AS
BEGIN

	SET NOCOUNT ON;
	
	IF (
			SELECT
				COUNT(1)
			FROM
				dbo.TblAdmonEsquemas
			WHERE
				IdAplicacion	= @pIdAplicacion
			AND	IdTipoDocPago	= @pIdTipoDocPago
			AND	Direccion		= @pDireccion
		) = 0
	BEGIN
	
		SET @pIdAplicacion	= 0	
	
	END
	
	SELECT
		e.IdEsquema
		,e.Descripcion
		,e.Esquema
		,e.TargetNamespace
	FROM
		dbo.TblAdmonEsquemas	ae
	INNER JOIN
		dbo.CatEsquemas			e
	ON	ae.IdEsquema		= e.IdEsquema
	WHERE
		ae.IdAplicacion		= @pIdAplicacion
	AND	ae.IdTipoDocPago	= @pIdTipoDocPago
	AND ae.Direccion		= @pDireccion
	
	SET NOCOUNT OFF;


	
END