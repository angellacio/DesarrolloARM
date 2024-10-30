USE [MotorTraductor]
GO




IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pObtenerFolioPorAplicacion'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pObtenerFolioPorAplicacion

END
GO

/****************************************************************************************
Autor:			Abril Hernández Velasco
Fecha creación: 29 Mayo de 2013
Descripción:	Obtiene el consecutivo de folio que se debe asignar a la nueva petición.
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:				Abril Hernández Velasco
Fecha modificación:	30 Mayo de 2013
Observaciones:		Se obtiene todo el número de secuencia IdFoliador + ALR + Folio + 01

Autor:				
Fecha modificación:	
Observaciones:		
******************************************************************************************/
-- Exec pObtenerFolioPorAplicacion 1, 1
CREATE PROCEDURE [dbo].[pObtenerFolioPorAplicacion](
	@pIdAplicacion	SMALLINT
	,@pIdALR		TINYINT
)
AS
BEGIN

	DECLARE @vIdFoliador SMALLINT;
	DECLARE @vAnioActual SMALLINT;
	
	DECLARE @tFoliador TABLE (
		Folio BIGINT
	);

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	
	-- Busca el foliador que le corresponde a la aplicación.
	SELECT
		@vIdFoliador = ISNULL(IdFoliador, 0)
	FROM
		dbo.CatAplicacion
	WHERE
		IdAplicacion = @pIdAplicacion
	; 
	
	-- Obtiene le año actual de ejecución
	SET @vAnioActual = YEAR(GETDATE());


	IF @vIdFoliador > 0
	BEGIN

		BEGIN TRY
			
			BEGIN TRAN
		
				-- Verifica que exista un registro de folio, sino existe lo inicializa.
				IF	(
						SELECT
							COUNT(1)
						FROM
							dbo.TblFoliador
						WHERE
							IdFoliador	= @vIdFoliador
						AND	IdALR		= @pIdALR
						AND	Anio		= @vAnioActual
					) = 0
				BEGIN
				
					-- Ingresa un folio.
					INSERT INTO dbo.TblFoliador(
						IdFoliador
						,Anio
						,IdALR
						,Folio
					)		
					VALUES(
						@vIdFoliador
						,@vAnioActual
						,@pIdALR
						,0
					)
				
				END
				
				-- Actualiza el nuevo folio y coloca el valor en la variable @tFoliador
				-- para el control de concurrencia.
				UPDATE
					dbo.TblFoliador
				SET
					Folio = Folio + 1
				OUTPUT
					inserted.Folio
				INTO @tFoliador
				WHERE
					IdFoliador	= @vIdFoliador
				AND	IdALR		= @pIdALR
				AND	Anio		= @vAnioActual
				
			COMMIT TRAN
			
			SELECT
				(RIGHT('00' + CONVERT(VARCHAR, @vIdFoliador), 2) + RIGHT('00' + CONVERT(VARCHAR, @pIdALR), 2) + RIGHT('00000' + CONVERT(VARCHAR, Folio), 5) + '01') Folio
			FROM
				@tFoliador
		
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
	ELSE
	BEGIN
	
		RAISERROR ( N'No existe un foliador relacionado a la aplicación', 10, 1 )
	END
END