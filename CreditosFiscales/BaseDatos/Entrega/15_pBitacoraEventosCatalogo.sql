USE MotorTraductor
GO
IF OBJECT_ID('pBitacoraEventosCatalogo') > 0 drop procedure pBitacoraEventosCatalogo
GO
CREATE PROCEDURE pBitacoraEventosCatalogo
	@pIdCatalogo INT
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
	  IF ( @pIdCatalogo = 4 )
        SELECT IdBanco AS valor,
               Descripcion AS Descripcion
        FROM   CatBanco
	  IF ( @pIdCatalogo = 5 )
        SELECT IdEstatus AS valor,
               Descripcion AS Descripcion
        FROM   CatEstadoProceso
  END