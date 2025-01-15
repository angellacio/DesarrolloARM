USE [MotorTraductor]
GO

IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pInsertaErrores'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pInsertaErrores

END
GO

/****************************************************************************************
Autor:			Abril Hernández Velasco
Fecha creación: 20 Mayo de 2013
Descripción:	Inserta en la tabla TblBitacoraErrores un registro de error.
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:			
Fecha modificación:
Observaciones:
******************************************************************************************/
CREATE PROCEDURE [dbo].[pInsertaErrores](
	@pIdAplicacion		SMALLINT
	,@pIdTipoDocPago	SMALLINT
	,@pIdProcesamiento	UNIQUEIDENTIFIER
	,@pErrores			VARCHAR(MAX)	
)
AS
BEGIN

	BEGIN TRY
	
		BEGIN TRAN

			INSERT INTO dbo.TblBitacoraErrores (
				IdAplicacion
				,IdTipoDocPago
				,IdProcesamiento
				,Errores
			)
			VALUES (
				@pIdAplicacion
				,@pIdTipoDocPago
				,@pIdProcesamiento
				,@pErrores		
			)
		
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