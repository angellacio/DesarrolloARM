USE [MotorTraductor]
GO




IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pInsertaRegla'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pInsertaRegla

END
GO

/****************************************************************************************
Autor:			Abril Hernández Velasco
Fecha creación: 20 Mayo de 2013
Descripción:	Ingresa/Actualiza un registro de regla en la tabla CatReglas
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:			
Fecha modificación:
Observaciones:
******************************************************************************************/
CREATE PROCEDURE [dbo].[pInsertaRegla](
	@descripcion	VARCHAR(150)
	,@regla			NVARCHAR(MAX)
	,@esValidacion	BIT	
)
AS
BEGIN
	
	BEGIN TRY
	
		BEGIN TRAN
		
			IF (
					SELECT
						COUNT(1)
					FROM 
						dbo.CatReglas
					WHERE
						Descripcion		= @descripcion
					AND	EsValidacion	= @esValidacion
				) > 0
			BEGIN
			
				UPDATE
					dbo.CatReglas
				SET
					Regla				= @regla
				WHERE
						Descripcion		= @descripcion
					AND	EsValidacion	= @esValidacion
				;

			END
			ELSE
			BEGIN
			
				INSERT INTO dbo.CatReglas(
					Descripcion
					,Regla
					,EsValidacion
				)
				VALUES(
					@descripcion
					,@regla
					,@esValidacion
				)	
								
			END
			
			SELECT
				IdRegla
			FROM 
				dbo.CatReglas
			WHERE
				Descripcion		= @descripcion
			AND	EsValidacion	= @esValidacion
		
		COMMIT TRAN
	END TRY
	BEGIN CATCH
	
		ROLLBACK TRAN
		
		DECLARE 
			@ErrorNumber		INT
			,@ErrorMessage		NVARCHAR(4000)
			,@ErrorSeverity		INT
			,@ErrorState		INT
			,@ErrorProcedure	NVARCHAR(126)
			,@msg				NVARCHAR(2000)

		SELECT  
			@ErrorNumber		= ERROR_NUMBER()
			,@ErrorMessage		= ERROR_MESSAGE()
			,@ErrorSeverity		= ERROR_SEVERITY()
			,@ErrorState		= ERROR_STATE()
			,@ErrorProcedure	= ERROR_PROCEDURE()
	                
		SELECT  
			@msg = 
				'Error No. ' 
				+ CONVERT (NVARCHAR(10), @ErrorNumber)
				+ '. Descripción: ' 
				+ @ErrorMessage + ' Origen del Error: '
				+ @ErrorProcedure 
	        
		RAISERROR ( @msg, @ErrorSeverity, @ErrorState )
	
	END CATCH
END