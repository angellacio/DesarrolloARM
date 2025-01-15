USE [MotorTraductor]
GO



IF	(
		SELECT 
			COUNT(1)
		FROM
			INFORMATION_SCHEMA.ROUTINES
		WHERE
			ROUTINE_NAME = 'pInsertaMensajeOriginal'
	) > 0
BEGIN

	DROP PROCEDURE dbo.pInsertaMensajeOriginal

END
GO

/****************************************************************************************
Autor:			Abril Hernández Velasco
Fecha creación: 20 Mayo de 2013
Descripción:	Ingresa en la base de datos el mensaje original proveniente de la 
				aplicación que hizo una petición al motor.
------------------------------------------------------------------------------------------
Historial de Modificaciones
------------------------------------------------------------------------------------------
Autor:			
Fecha modificación:
Observaciones:
******************************************************************************************/
CREATE PROCEDURE [dbo].[pInsertaMensajeOriginal](
	@pIdProcesamiento	UNIQUEIDENTIFIER
	,@pMensaje	XML
)
AS
BEGIN
	
	
	DECLARE @concepto XML;
	DECLARE @idConcepto UNIQUEIDENTIFIER;
		
	BEGIN TRY
	
	
		BEGIN TRANSACTION;
		
		INSERT INTO dbo.TblDatosGenerales(
			IdPeticion
			,IdDocumento
			,Solicitud
			,IdContribuyente
			,OrigenInformacion
			,ClaveGenerica
			,NumeroConceptos
			,ImporteTotal
			,FechaEmision
			,FechaRecepcion
			,RFC
			,Nombre
			,ApellidoPaterno
			,ApellidoMaterno
			,RazonSocial
			,ClaveAlr
			,TipoDocumento
			,DescripcionTipoDocumento
		)
		SELECT 
			@pIdProcesamiento IdPeticion
			,S.c.value('IdDocumento[1]', 'bigint') IdDocumento
			,S.c.value('Solicitud[1]', 'bigint') Solicitud
			,S.c.value('IdContribuyente[1]', 'int') IdContribuyente
			,S.c.value('OrigenInformacion[1]', 'int') OrigenInformacion
			,S.c.value('ClaveGenerica[1]', 'varchar(100)') ClaveGenerica
			,S.c.value('NumeroConceptos[1]', 'int') NumeroConceptos
			,S.c.value('ImporteTotal[1]', 'bigint') ImporteTotal
			,S.c.value('FechaEmision[1]', 'smalldatetime') FechaEmision
			,S.c.value('FechaRecepcion[1]', 'smalldatetime') FechaRecepcion
			,S.c.value('RFC[1]', 'varchar(13)') RFC
			,S.c.value('Nombre[1]', 'varchar(60)') Nombre
			,S.c.value('ApellidoPaterno[1]', 'varchar(60)') ApellidoPaterno
			,S.c.value('ApellidoMaterno[1]', 'varchar(60)') ApellidoMaterno
			,S.c.value('RazonSocial[1]', 'varchar(120)') RazonSocial
			,S.c.value('ClaveAlr[1]', 'smallint') ClaveAlr
			,S.c.value('TipoDocumento[1]', 'smallint') TipoDocumento
			,S.c.value('DescripcionTipoDocumento[1]', 'varchar(50)') TipoDocumento
		FROM
			@pMensaje.nodes('SolicitudGeneracionLC/DatosGenerales')S(c)
			
		
		DECLARE CCONCEPTO CURSOR FOR
			SELECT 
				NEWID() IdConcepto
				,S.c.query('.') Concepto
			FROM
				@pMensaje.nodes('SolicitudGeneracionLC/Conceptos/Concepto')S(c)

		OPEN CCONCEPTO;

		FETCH NEXT FROM CCONCEPTO
		INTO @idConcepto, @concepto

		WHILE @@FETCH_STATUS = 0
		BEGIN
			
			INSERT INTO dbo.TblConceptos
			SELECT 
				@pIdProcesamiento IdPeticion
				,@idConcepto IdConcepto
				,S.c.value('NumeroSecuencia[1]', 'smallint') NumeroSecuencia
				,S.c.value('Clave[1]', 'int') Clave
				,S.c.value('Descripcion[1]', 'varchar(300)') Descripcion
				,S.c.value('ClaveGenerica[1]', 'varchar(200)') ClaveGenerica
				,S.c.value('ImporteTotalCargos[1]', 'bigint') ImporteTotalCargos
				,S.c.value('ImporteTotalAbonos[1]', 'bigint') ImporteTotalAbonos
				,S.c.value('CantidadPagar[1]', 'bigint') CantidadPagar
				,S.c.value('DatosIcep[1]/IcepCorrecto[1]', 'bit') IcepCorrecto
				,S.c.value('DatosIcep[1]/Ejercicio[1]', 'smallint') Ejercicio
				,S.c.value('DatosIcep[1]/ClavePeriodicidad[1]', 'tinyint') ClavePeriodicidad
				,S.c.value('DatosIcep[1]/DescripcionPeriodicidad[1]', 'varchar(30)') DescripcionPeriodicidad
				,S.c.value('DatosIcep[1]/ClavePeriodo[1]', 'tinyint') ClavePeriodo
				,S.c.value('DatosIcep[1]/DescripcionPeriodo[1]', 'varchar(10)') DescripcionPeriodo
				,S.c.value('DatosIcep[1]/FechaCausacion[1]', 'date') FechaCausacion	
			FROM
				@concepto.nodes('Concepto')S(c)
			
			INSERT INTO dbo.TblTransacciones
			SELECT 
				@idConcepto IdConcepto
				,S.c.value('Clave[1]', 'smallint') Clave
				,S.c.value('Descripcion[1]', 'varchar(300)') Descripcion
				,S.c.value('Valor[1]', 'varchar(200)') Valor		
			FROM
				@concepto.nodes('Concepto/Transacciones/Transaccion')S(c)
				
			FETCH NEXT FROM CCONCEPTO
			INTO @idConcepto, @concepto

		END

		CLOSE CCONCEPTO;
		DEALLOCATE CCONCEPTO;
		
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH	
	
		ROLLBACK TRANSACTION;
		
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