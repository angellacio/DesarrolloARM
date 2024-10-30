/* Inserta en catálogo de fechas del INPC las fechas  del 2020 id-1778*/
USE CreditosFiscales
GO
BEGIN TRY
BEGIN TRAN
insert into CatFechaINPC values('2020-01-10') --diciembre
insert into CatFechaINPC values('2020-02-10') --enero
insert into CatFechaINPC values('2020-03-10') --febrero
insert into CatFechaINPC values('2020-04-08') --marzo
insert into CatFechaINPC values('2020-05-08') --abril
insert into CatFechaINPC values('2020-06-10') --mayo
insert into CatFechaINPC values('2020-07-10') --junio
insert into CatFechaINPC values('2020-08-10') --julio
insert into CatFechaINPC values('2020-09-10') --agosto
insert into CatFechaINPC values('2020-10-09') --septiembre
insert into CatFechaINPC values('2020-11-10') --octubre
insert into CatFechaINPC values('2020-12-10') --noviembre
insert into CatFechaINPC values('2021-01-08') --dicembre	
/* Inserta en catálogo de fechas de Dias inhabiles las fechas del 2020 id-1778 */
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200101');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200102');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200103');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200106');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200107');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200203');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200316');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200409');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200410');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200501');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200505');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200720');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200721');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200722');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200723');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200724');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200727');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200728');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200729');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200730');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200731');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20200916');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201116');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20201225');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210101');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210102');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210315');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210401');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210402');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210505');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20210916');
INSERT INTO dbo.CatDiaInhabil VALUES ((select MAX(IdFecha)+1 from dbo.CatDiaInhabil),'20211115');

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
