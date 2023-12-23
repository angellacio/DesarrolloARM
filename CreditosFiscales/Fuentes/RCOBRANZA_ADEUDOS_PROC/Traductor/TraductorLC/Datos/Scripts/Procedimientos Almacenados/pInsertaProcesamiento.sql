USE [MotorTraductor]
GO



IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pInsertaProcesamiento'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pInsertaProcesamiento

END
GO

/****************************************************************************************
Autor:			Abril Hern�ndez Velasco
Fecha creaci�n: 20 Mayo de 2013
Descripci�n:	Ingresa un registro en la bit�cora.
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:			
Fecha modificaci�n:
Observaciones:
******************************************************************************************/
CREATE PROCEDURE [dbo].[pInsertaProcesamiento](
	@pIdAplicacion		SMALLINT
	,@pIdTipoDocPago	SMALLINT
	,@pIdProcesamiento	UNIQUEIDENTIFIER
	,@pPaso				TINYINT
	,@pMensaje			XML = NULL
	,@pObservaciones	NVARCHAR(4000) = NULL
)
AS
BEGIN

	BEGIN TRY
	
		BEGIN TRAN
		
			INSERT INTO dbo.TblBitacoraProcesamiento (
				IdAplicacion
				,IdTipoDocPago
				,IdProcesamiento
				,Paso
				,Mensaje
				,Observaciones
			)
			VALUES (
				@pIdAplicacion
				,@pIdTipoDocPago
				,@pIdProcesamiento
				,@pPaso
				,@pMensaje
				,@pObservaciones
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
				+ '. Descripci�n: ' 
				+ @ErrorMessage + ' Origen del Error: '
				+ @ErrorProcedure 
	        
		RAISERROR ( @msg, @ErrorSeverity, @ErrorState )
	
	END CATCH
END