/*-- Inserta en catálogo de fechas del INPC las fechas  del 2021 rma-3026752 --*/

USE CreditosFiscales
GO
BEGIN TRY
BEGIN TRAN
insert into CatFechaINPC values('2021-02-10') --enero
insert into CatFechaINPC values('2021-03-10') --febrero
insert into CatFechaINPC values('2021-04-09') --marzo
insert into CatFechaINPC values('2021-05-10') --abril
insert into CatFechaINPC values('2021-06-10') --mayo
insert into CatFechaINPC values('2021-07-09') --junio
insert into CatFechaINPC values('2021-08-10') --julio
insert into CatFechaINPC values('2021-09-10') --agosto
insert into CatFechaINPC values('2021-10-08') --septiembre
insert into CatFechaINPC values('2021-11-10') --octubre
insert into CatFechaINPC values('2021-12-10') --noviembre
insert into CatFechaINPC values('2022-01-10') --dicembre	

/*-- Inserta en catálogo de fechas de Dias inhabiles las fechas del 2021 rma-3026752 --*/
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201217');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201218');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201221');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201222');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201223');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201224');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201228');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201229');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201230');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201231');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210104');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210105');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210106');

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
