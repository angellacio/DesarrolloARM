--Select TOP 10 * from TDOTDocumentos
--select * from dbo.TDOTProcesos
USE DIOperaTerceros
GO
IF (OBJECT_ID('ServiciosAdministra') > 0) DROP TABLE ServiciosAdministra
GO
CREATE TABLE dbo.ServiciosAdministra(
    IDServicio INT IDENTITY(1,1) PRIMARY KEY, 
    SerNombre nVarChar(150) NOT NULL,
    SerRuta nVarChar(250) NOT NULL,
    SerEstado Bit NOT NULL DEFAULT 0
)
GO
IF (OBJECT_ID('FlujoDeclaracion') > 0) DROP TABLE FlujoDeclaracion
GO
IF (OBJECT_ID('EstadosFluDec') > 0) DROP TABLE EstadosFluDec
GO
CREATE TABLE dbo.EstadosFluDec(
    IDEstado INT NOT NULL, 
    Estado nVarChar(150) NOT NULL,
    CONSTRAINT PK_IDEstado PRIMARY KEY(IDEstado)
)
GO
CREATE TABLE dbo.FlujoDeclaracion(
    IDProceso INT NOT NULL, 
    Servidor nVarChar(150) NOT NULL,
    Mensaje nVarChar(max) NULL,
    NumIntentos INT NOT NULL default 1,
    IDEstado INT, -- 0 Sin archivo :: 1 Archivo FS :: 2 En proceso a DUCTO :: 3 Enviado a DUCTO :: 4 Error proceso DUCTO
    CONSTRAINT PK_IDProceso PRIMARY KEY(IDProceso),
    CONSTRAINT FK_IDProceso FOREIGN KEY (IDProceso) REFERENCES TDOTProcesos(IDProceso),
    CONSTRAINT FK_IDEstado FOREIGN KEY (IDEstado) REFERENCES EstadosFluDec(IDEstado)
)
GO
IF (OBJECT_ID('AdSeNu_ConsultaServicios') > 0) DROP PROCEDURE AdSeNu_ConsultaServicios
GO
CREATE PROCEDURE AdSeNu_ConsultaServicios
    @nServicio INT
AS
 BEGIN
    IF @nServicio = 0 
     BEGIN
        SELECT IDServicio, SerNombre, SerRuta, SerEstado
        FROM ServiciosAdministra
     END
    ELSE
     BEGIN
        SELECT IDServicio, SerNombre, SerRuta, SerEstado
        FROM ServiciosAdministra
        WHERE IDServicio = @nServicio
     END
 END
GO
IF (OBJECT_ID('ProNue_ConsultaSolicitudes') > 0) DROP PROCEDURE ProNue_ConsultaSolicitudes
GO
CREATE PROCEDURE ProNue_ConsultaSolicitudes
    @nTipCon INT, 
    @sEquipo nVarChar(150), 
    @numIntentos INT
AS
 BEGIN
    IF @nTipCon = 1 BEGIN -- Consulta Archivos cargar a BD
        SELECT tdp.IDProceso, tdp.NombreArchivo, tdp.IDMedio, cmed.Descripcion 'Medio',
               tdp.EntReceptora, tdp.FechaPresentacion, tdp.RFCAutenticacion, tdp.RFC, 
               tdp.d_materia, tdp.IDMensaje, cmen.Descripcion 'Mensaje'
        FROM TDOTProcesos tdp INNER JOIN 
             FlujoDeclaracion fd ON tdp.IDProceso = fd.IDProceso INNER JOIN
             CDOTMedio cmed ON tdp.IDMedio = cmed.IDMedio INNER JOIN
             CDOTMensajes cmen ON tdp.IDMensaje = cmen.IDMensaje
        WHERE IDEstado = 1
              AND fd.Servidor = @sEquipo 
              AND NumIntentos < @numIntentos
     END
 END
GO
IF (OBJECT_ID('ProNue_AltaArchivo') > 0) DROP PROCEDURE ProNue_AltaArchivo
GO
CREATE PROCEDURE dbo.ProNue_AltaArchivo
        @nIdProceso INT, 
        @nFileSize INT,
        @nFileSizeMax INT,
        @iDeclaracion Image
AS
 BEGIN
    DECLARE @nMedio INT, @nMensaje INT, @sMensaje nVarChar(250)

    SELECT @nMensaje = -1, @sMensaje = 'Proceso correcto.'
    IF ((@nFileSizeMax IS NULL) OR (@nFileSizeMax = 0)) BEGIN
        SELECT @nFileSizeMax = CAST(SettingValue AS INT) FROM ScadeNet..ApplicationSettings WHERE SettingName = 'SAT.SCADE.NET.Negocio.Declaraciones.InformativaDeTerceros::MaxFileSizeInternet'
     END
    SELECT @nMedio = IDMedio FROM TDOTProcesos WHERE IDProceso = @nIdProceso

    IF (@nMedio IS NULL) BEGIN
        SELECT @nMensaje = 300, @sMensaje = 'El número de operación no se encontro.'
        SELECT @nMensaje, @sMensaje
        RETURN @nMensaje
     END

    IF ((SELECT COUNT(IDProceso) FROM TDOTDocumentosGrandes WHERE IDProceso = @nIdProceso) > 0) BEGIN
        DELETE FROM TDOTDocumentosGrandes WHERE IDProceso = @nIdProceso
     END
    IF ((SELECT COUNT(IDProceso) FROM TDOTDocumentos WHERE IDProceso = @nIdProceso) > 0) BEGIN
        DELETE FROM TDOTDocumentos WHERE IDProceso = @nIdProceso
     END

    IF (@nFileSizeMax <= @nFileSize) BEGIN
        INSERT INTO TDOTDocumentosGrandes(IDProceso, Documento) VALUES(@nIdProceso, @iDeclaracion)
     END
    ELSE BEGIN
        INSERT INTO TDOTDocumentos(IDProceso, Documento) VALUES(@nIdProceso, @iDeclaracion)
     END

    UPDATE TDOTProcesos SET IDMensaje = 1 WHERE IDProceso = @nIdProceso 
    UPDATE FlujoDeclaracion SET IDEstado = 2 WHERE IDProceso = @nIdProceso

    SELECT @nMensaje, @sMensaje
    RETURN @nMensaje
 END
GO
INSERT INTO EstadosFluDec VALUES(0, 'Sin archivo')
INSERT INTO EstadosFluDec VALUES(1, 'Archivo en FileShare')
INSERT INTO EstadosFluDec VALUES(2, 'Declaracion en BD')
INSERT INTO EstadosFluDec VALUES(3, 'En proceso a DUCTO')
INSERT INTO EstadosFluDec VALUES(4, 'Enviado a DUCTO')
INSERT INTO EstadosFluDec VALUES(5, 'Error envia a DUCTO')
INSERT INTO ServiciosAdministra VALUES('Servicio Recepción', 'C:\Program Files (x86)\SAT\DIOT_Recepcion\DIOT_Recepcion.exe', 1)
INSERT INTO ServiciosAdministra VALUES('Servicio Respuesta', 'C:\Program Files (x86)\SAT\DIOT_Recepcion\DIOT_Respuesta.exe', 1)
GO

INSERT INTO FlujoDeclaracion VALUES(100000288, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000287, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000286, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000285, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000284, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000283, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000280, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000279, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000278, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000276, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000275, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000274, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000273, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000272, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000271, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000270, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000269, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000268, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000267, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000266, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000265, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000264, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000263, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000262, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000261, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000260, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000259, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000258, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000257, '6LVL0N2', '', 1, 1)
INSERT INTO FlujoDeclaracion VALUES(100000256, '6LVL0N2', '', 1, 1)

UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000288
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000287
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000286
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000285
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000284
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000283
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000280
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000279
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000278
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000276
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000275
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000274
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000273
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000272
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000271
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000270
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000269
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000268
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000267
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000266
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000265
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000264
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000263
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000262
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000261
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000260
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000259
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000258
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000257
UPDATE TDOTProcesos SET IDMensaje = 0 WHERE IDProceso = 100000256
