/*-- Elimina del catálogo de fechas del INPC las fechas  del 2021 rma 3026752 --*/
USE CreditosFiscales
GO
BEGIN TRY
BEGIN TRAN

DELETE FROM CatFechaINPC WHERE YEAR(fecha) IN ('2021','2022');
DELETE FROM CatDiaInhabil WHERE YEAR(fecha) IN ('2020','2021');

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

SELECT [Fecha]
  FROM [CreditosFiscales].[dbo].[CatFechaINPC]
GO

SELECT [IdFecha]
      ,[Fecha]
  FROM [CreditosFiscales].[dbo].[CatDiaInhabil]
GO
