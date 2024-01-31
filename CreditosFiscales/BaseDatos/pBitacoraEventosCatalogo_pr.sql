USE [MotorTraductor]
GO
/****** Object:  StoredProcedure [dbo].[pBitacoraEventosCatalogo]    Script Date: 25/01/2024 11:04:25 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[pBitacoraEventosCatalogo] @pIdCatalogo INT
AS
  BEGIN
      IF ( @pIdCatalogo = 1 )
        SELECT IdAplicacion AS valor,
               Nombre       AS Descripcion
        FROM   CatAplicacion

      IF ( @pIdCatalogo = 2 )
        SELECT IdTipoDocumento AS valor,
               Nombre          AS Descripcion
        FROM   CatTipoDocumento

	  IF ( @pIdCatalogo = 3 )
        SELECT [IdPasoProceso] AS valor,
               [Descripcion] AS Descripcion
        FROM   CatPasosProcesos
  END


