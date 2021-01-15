USE MonitorPaquetes;
GO
IF OBJECT_ID('ManejoObservaciones') IS NOT NULL DROP PROCEDURE ManejoObservaciones;
GO
CREATE PROCEDURE ManejoObservaciones(
    @IdObservacion INT,
    @Incidente nVarChar(MAX), 
    @Paquete nVarChar(MAX),
    @Observaciones nVarChar(MAX) = null,
    @TipoMov Int = 4)
AS
 BEGIN
    IF (@TipoMov = 1)
        INSERT INTO Observaciones VALUES(@Incidente, @Paquete, GetDate(), @Observaciones)

    IF (@TipoMov = 2)
        UPDATE Observaciones SET Observaciones = @Observaciones, IdIncidente = @Incidente, IdPaquete = @Paquete WHERE ID = @IdObservacion

    IF (@TipoMov = 3)
        DELETE FROM Observaciones WHERE ID = @IdObservacion

    IF (@Paquete IS NULL)
        SELECT ID, IdIncidente, IdPaquete, FechaCaptura, Observaciones 
        FROM Observaciones 
        WHERE IdIncidente = @Incidente AND IdPaquete IS NULL 
        ORDER BY IdIncidente, IdPaquete, FechaCaptura DESC
    ELSE
        SELECT ID, IdIncidente, IdPaquete, FechaCaptura, Observaciones 
        FROM Observaciones 
        WHERE IdIncidente = @Incidente AND IdPaquete = @Paquete 
        ORDER BY IdIncidente, IdPaquete, FechaCaptura DESC
 END;
GO