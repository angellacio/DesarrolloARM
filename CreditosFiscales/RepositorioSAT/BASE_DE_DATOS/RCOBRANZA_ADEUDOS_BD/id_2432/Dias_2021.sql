USE CreditosFiscales
GO
BEGIN TRY
BEGIN TRAN	
/* Inserta en catálogo de fechas de Dias inhabiles las fechas del 2021 id-2432 */
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210719');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210720');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210721');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210722');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210723');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210726');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210727');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210728');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210729');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210730');

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

SELECT [IdFecha]
      ,[Fecha]
  FROM [CreditosFiscales].[dbo].[CatDiaInhabil]
GO
