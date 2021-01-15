USE MonitorPaquetes
GO
IF (OBJECT_ID('PWin_Consultas') IS NOT NULL) DROP PROCEDURE PWin_Consultas
GO
CREATE PROCEDURE dbo.PWin_Consultas
    @nTipoConsulta INT = 0,
    @nParametro1 INT = 0,
    @sParametro1 nVarChar(50) = '',
    @nParametro2 INT = 0,
    @sParametro2 nVarChar(50) = ''
AS
 BEGIN
    DECLARE @sQuery nVarChar(MAX)

    IF (@nTipoConsulta = 1) BEGIN
        SET @sQuery = 'SELECT Orden ''nId'', Id_Area ''sId'', CorreoNotifica ''sAcronimo'', Area ''sDescripcion'', CONVERT(BIT, 1) ''bEstatus'' 
        FROM cat_Area '
        IF (@sParametro1 <> '') SET @sQuery = @sQuery + 'WHERE Id_Area IN(' + @sParametro1 + ') '
        SET @sQuery = @sQuery + 'ORDER BY Orden; '
     END

    IF (@nTipoConsulta = 2) BEGIN
        SET @sQuery = 'SELECT Id_Desa ''nId'', '''' ''sId'', Correo ''sAcronimo'', Nombre ''sDescripcion'', Activo ''bEstatus'' 
        FROM Cat_Desarrolladores 
        WHERE Id_Desa > 1 ';
        IF (@sParametro1 <> '') SET @sQuery = @sQuery + 'AND cat_Area IN(' + @sParametro1 + ') ';
        SET @sQuery = @sQuery + 'ORDER BY Id_Desa DESC; '

     END

    IF (@nTipoConsulta = 50) BEGIN
        SET @sQuery = 'SELECT cAre.Area, Incs.Incidente, Incs.Fecha, Incs.Descripcion, Incs.Retroalimentacion, rInPa.Paquete, Paqu.Fecha_Registro, 
               Incs.PPMC, Paqu.DOC_PRU, CONVERT(BIT, Paqu.Notificado) ''Notificado'', cDesa.Nombre ''Desarrollador'', 
               ISNULL(cEsta.Descripcion, '''') ''Estado'', Paqu.Observaciones
        FROM Incidentes Incs INNER JOIN 
             cat_Area cAre ON Incs.Id_Area = cAre.Id_Area LEFT OUTER JOIN
             Relacion_Inc rInPa ON Incs.Incidente = rInPa.Incidente LEFT OUTER JOIN
             Paquetes Paqu ON rInPa.Paquete = Paqu.Paquete LEFT OUTER JOIN
             Cat_Desarrolladores cDesa ON Paqu.Id_Desa = cDesa.Id_Desa LEFT OUTER JOIN
             Cat_Estados cEsta ON Paqu.Id_Estado = cEsta.Id_Estado 
        WHERE 1 = 1 ';
        IF (@nParametro1 = 0) SET @sQuery = @sQuery + 'AND Paqu.Notificado = 0 ';
        IF (@sParametro1 <> '') SET @sQuery = @sQuery + 'AND Incs.Id_Area IN(' + @sParametro1 + ') ';
        SET @sQuery = @sQuery + 'ORDER BY Paqu.Notificado, cAre.Orden, 
                 CONVERT(INT, SUBSTRING(REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(ISNULL(Incs.Incidente, ''PPMC''))), ''INC'', ''01''), ''INCS'', ''01''), ''POL'', ''10''), ''PPMC'', ''20''), 1, 2)), 
                 Incs.Incidente, Incs.Fecha;

        SELECT rdl.Paquete, rdl.RDL, rdl.FechaInicio, rdl.Id_Estado, ISNULL(cEs.Descripcion, '''') AS Descripcion 
        FROM RDLs rdl LEFT OUTER JOIN
             Cat_Estados cEs ON rdl.Id_Estado = cEs.Id_Estado LEFT OUTER JOIN
             Paquetes Paq ON rdl.Paquete = Paq.Paquete LEFT OUTER JOIN
             Relacion_Inc rIP ON rdl.Paquete = rIP.Paquete LEFT OUTER JOIN
             Incidentes inc ON rIP.Incidente = inc.Incidente 
        WHERE 1 = 1 ';
        IF (@nParametro1 = 0) SET @sQuery = @sQuery + 'AND Paq.Notificado = 0 ';
        IF (@sParametro1 <> '') SET @sQuery = @sQuery + 'AND Inc.Id_Area IN(' + @sParametro1 + ') ';
    END

    IF (@nTipoConsulta = 51) BEGIN
        SELECT Inc.Id_Area, cAr.Area, Inc.Incidente, Inc.Fecha AS AltaIncidente, Inc.Descripcion, Inc.Retroalimentacion,
               Inc.NotaLibera, Inc.PPMC
        FROM Incidentes Inc INNER JOIN
             cat_Area cAr ON Inc.Id_Area = cAr.Id_Area
        WHERE Inc.Incidente = @sParametro1
        ORDER BY cAr.Orden, 
                 CONVERT(INT, SUBSTRING(REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(ISNULL(Inc.Incidente, 'PPMC'))), 'INC', '01'), 'INCS', '01'), 'POL', '10'), 'PPMC', '20'), 1, 2)), 
                 Inc.Fecha DESC

        SELECT rInc.Incidente, rInc.Paquete, Paq.Fecha_Registro AS AltaPaquete, Paq.Id_Desa, catDec.Nombre, Paq.Id_Estado, 
               ISNULL(catEst.Descripcion, '') AS Estado, Paq.Rdl, Paq.DOC_PRU, CONVERT(BIT, Paq.Notificado) AS Notificado, 
               Paq.Observaciones
        FROM Relacion_Inc rInc LEFT OUTER JOIN 
             Paquetes Paq ON rInc.Paquete = Paq.Paquete  LEFT OUTER JOIN 
             Cat_Estados catEst ON Paq.Id_Estado = catEst.Id_Estado LEFT OUTER JOIN
             Cat_Desarrolladores catDec ON Paq.Id_Desa = catDec.Id_Desa
        WHERE rInc.Paquete = @sParametro2
        ORDER BY Paq.Notificado DESC

       SELECT tRdl.RDL, tRdl.Paquete, tRdl.FechaInicio, tRdl.Id_Estado, ISNULL(catEst.Descripcion, '') AS Descripcion
       FROM RDLs tRdl LEFT OUTER JOIN
            Cat_Estados catEst ON tRdl.Id_Estado = catEst.Id_Estado
       WHERE tRdl.Paquete = @sParametro2

       SELECT ID, IdIncidente, FechaCaptura, Observaciones
       FROM Observaciones
       WHERE IdIncidente = @sParametro1 AND IdPaquete IS NULL
       ORDER BY IdIncidente, IdPaquete, FechaCaptura DESC

       SELECT ID, IdIncidente, IdPaquete, FechaCaptura, Observaciones
       FROM Observaciones
       WHERE IdIncidente = @sParametro1 AND IdPaquete = @sParametro2
       ORDER BY IdIncidente, IdPaquete, FechaCaptura DESC
    END

    --SELECT @sQuery
    EXEC sp_executesql @sQuery
 END
GO
