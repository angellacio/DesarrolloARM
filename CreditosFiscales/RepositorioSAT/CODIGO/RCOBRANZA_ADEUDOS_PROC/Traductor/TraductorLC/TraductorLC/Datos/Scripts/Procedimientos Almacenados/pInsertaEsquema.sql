USE [MotorTraductor]
GO



IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pInsertaEsquema'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pInsertaEsquema

END
GO

/****************************************************************************************
Autor:			Abril Hernández Velasco
Fecha creación: 20 Mayo de 2013
Descripción:	Inserta en la tabla CatEsquemas un esquema.
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:			
Fecha modificación:
Observaciones:
******************************************************************************************/
CREATE PROCEDURE [dbo].[pInsertaEsquema](
	@pDescripcion		VARCHAR(150)
	,@pEsquema			NVARCHAR(MAX)
	,@pTargetNamespace	VARCHAR(150)
)
AS
BEGIN

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

	BEGIN TRY
	
		BEGIN TRAN
		
			IF (
					SELECT
						COUNT(1)
					FROM
						dbo.CatEsquemas
					WHERE
						Descripcion		= @pDescripcion
					AND TargetNamespace = @pTargetNamespace
				) > 0
			BEGIN
			
				UPDATE
					dbo.CatEsquemas 
				WITH(READPAST, UPDLOCK)
				SET
					Esquema				= @pEsquema
				WHERE
					Descripcion			= @pDescripcion
					AND TargetNamespace = @pTargetNamespace
				;
				
				SELECT
					IdEsquema
				FROM
					dbo.CatEsquemas
				WHERE
					Descripcion		= @pDescripcion
				AND TargetNamespace = @pTargetNamespace
			
			END
			ELSE
			BEGIN
			
				INSERT INTO dbo.CatEsquemas(
					Descripcion
					,Esquema
					,TargetNamespace
				)
				VALUES(
					@pDescripcion
					,@pEsquema
					,@pTargetNamespace
				)
				
				SELECT @@IDENTITY;
				
			END	
		
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