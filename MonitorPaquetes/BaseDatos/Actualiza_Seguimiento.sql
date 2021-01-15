USE MonitorPaquetes;
GO
IF OBJECT_ID('Actualiza_Seguimiento') IS NOT NULL DROP PROCEDURE Actualiza_Seguimiento;
GO
CREATE PROCEDURE dbo.Actualiza_Seguimiento(
    @Incidente nVarChar(MAX), 
    @Paquete nVarChar(MAX), 
    @Retroalimentado BIT =  null,
    @DocPru BIT = null, 
    @Identificador nVarChar(MAX) = null, 
    @IdentificadorPaq nVarChar(MAX) = null, 
    @NotLiberar nVarChar(MAX) = null,
    @sArea nVarChar(10) = null,
    @nDesarrollador INT = null,
    @PPMC BIT = null,
    @IncidenteNew nVarChar(MAX) = null, 
    @PaqueteNew nVarChar(MAX) = null,
    @Notificado BIT = null,
    @Observaciones nVarChar(MAX) = null,
    @ObservacionesIns nVarChar(MAX) = null)
AS
 BEGIN
    IF (@Retroalimentado IS NOT NULL) UPDATE Incidentes SET Retroalimentacion = @Retroalimentado WHERE Incidente = @Incidente
    IF (@PPMC IS NOT NULL) UPDATE Incidentes SET PPMC = @PPMC WHERE Incidente = @Incidente
    IF (@DocPru  IS NOT NULL) UPDATE Paquetes SET DOC_PRU = @DocPru WHERE Paquete = @Paquete
    IF (@Identificador  IS NOT NULL) UPDATE Incidentes SET Descripcion = @Identificador WHERE Incidente = @Incidente
    IF (@IdentificadorPaq  IS NOT NULL) UPDATE Paquetes SET Observaciones = @IdentificadorPaq WHERE Paquete = @Paquete
    IF (@NotLiberar  IS NOT NULL) UPDATE Incidentes SET NotaLibera = @NotLiberar WHERE Incidente = @Incidente
    IF (@sArea IS NOT NULL) UPDATE Incidentes SET Id_Area = @sArea WHERE Incidente = @Incidente
    IF (@nDesarrollador IS NOT NULL) UPDATE Paquetes SET Id_Desa = @nDesarrollador WHERE Paquete = @Paquete
    IF (@IncidenteNew IS NOT NULL)
     BEGIN
        UPDATE Relacion_Inc SET Incidente = @IncidenteNew WHERE Incidente = @Incidente
        UPDATE Incidentes SET Incidente = @IncidenteNew WHERE Incidente = @Incidente
     END
    IF (@PaqueteNew IS NOT NULL)
     BEGIN
        UPDATE Relacion_Inc SET Paquete = @PaqueteNew WHERE Paquete = @Paquete
        UPDATE Paquetes SET Paquete = @PaqueteNew WHERE Paquete = @Paquete
     END
    IF (@Notificado IS NOT NULL) UPDATE Paquetes SET Notificado = @Notificado WHERE Paquete = @Paquete
    IF (@Observaciones IS NOT NULL) UPDATE Observaciones SET Observaciones = @Observaciones WHERE IdIncidente = @Incidente AND IdPaquete = @Paquete
    IF (@ObservacionesIns IS NOT NULL) INSERT INTO Observaciones VALUES (@Incidente, @Paquete, GetDate(), @ObservacionesIns)
 END;
