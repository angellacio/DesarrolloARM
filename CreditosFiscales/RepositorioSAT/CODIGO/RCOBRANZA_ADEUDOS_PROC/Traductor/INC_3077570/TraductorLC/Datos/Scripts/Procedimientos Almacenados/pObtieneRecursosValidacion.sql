USE [MotorTraductor]
GO




IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pObtieneRecursosValidacion'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pObtieneRecursosValidacion

END
GO

/****************************************************************************************
Autor:			Abril Hernández Velasco
Fecha creación: 20 Mayo de 2013
Descripción:	Obtiene la lista de contratos y reglas ligadas a una aplicación y un tipo
				de documento.
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:				Abril Hernández Velasco
Fecha modificación: 31 Mayo de 2013
Observaciones:		Se cambia el tipo de dato IdRegla y se obtiene de dos fuentes.
------------------------------------------------------------------------------------------
Autor:			
Fecha modificación:
Observaciones:
******************************************************************************************/
-- exec pObtieneRecursosValidacion 1, 30
CREATE PROCEDURE [dbo].[pObtieneRecursosValidacion](
	@pIdAplicacion		SMALLINT = 0
	,@pIdTipoDocPago	SMALLINT = 0
)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @vAplicacion SMALLINT;
	
	SET @vAplicacion = @pIdAplicacion
	
	IF (
	
			SELECT
				COUNT(1)
			FROM
				dbo.TblAdmonEsquemas 
			WHERE
				IdAplicacion	= @pIdAplicacion
			AND	IdTipoDocPago	= @pIdTipoDocPago	
		
		) = 0
	BEGIN	
		SET @vAplicacion = 0	
	END
	
	SELECT
		e.IdEsquema
		,e.Descripcion
		,e.Esquema
		,e.TargetNamespace
		,ae.Direccion
	FROM
		dbo.TblAdmonEsquemas	ae
	INNER JOIN
		dbo.CatEsquemas			e
	ON	ae.IdEsquema		= e.IdEsquema
	WHERE
		ae.IdAplicacion		= @vAplicacion
	AND	ae.IdTipoDocPago	= @pIdTipoDocPago
	;
	
	SET @vAplicacion = @pIdAplicacion
	
	IF (
	
			SELECT
				COUNT(1)
			FROM
				dbo.TblAdmonEsquemas 
			WHERE
				IdAplicacion	= @pIdAplicacion
			AND	IdTipoDocPago	= @pIdTipoDocPago	
		
		) = 0
	BEGIN	
		SET @vAplicacion = 0	
	END
	
	
	SELECT 
		IdRegla
		,Regla
		,Descripcion
		,EsValidacion
		,EsNET
		,Secuencia
	FROM (
			SELECT 
				reg.IdRegla
				,Regla
				,Descripcion
				,EsValidacion
				,Secuencia
				,CAST(0 AS BIT) EsNET
			FROM
				dbo.TblAdmonReglas	admon
			INNER JOIN
				dbo.CatReglas		reg
			ON	admon.IdRegla		= reg.IdRegla
			WHERE
				admon.IdAplicacion	= @vAplicacion
			AND	admon.IdTipoDocPago	= @pIdTipoDocPago
			
			UNION
			
			SELECT 
				IdRegla
				,reg.NombreRegla Regla
				,Descripcion
				,CAST(1 AS BIT) EsValidacion
				,Secuencia
				,CAST(1 AS BIT) EsNET
			FROM
				dbo.TblAdmonReglas	admon
			INNER JOIN
				dbo.CatReglasNet	reg
			ON	admon.IdRegla		= reg.IdReglaNet
			WHERE
				admon.IdAplicacion	= @vAplicacion
			AND	admon.IdTipoDocPago	= @pIdTipoDocPago
		) T
	ORDER BY
		Secuencia
	
	
	SELECT
		Ruta
	FROM
		dbo.CatDirectorioReglas
	;
	
	SET NOCOUNT OFF;
	
END