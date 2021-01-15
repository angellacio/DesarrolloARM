USE MonitorPaquetes;
GO
IF(OBJECT_ID('Observaciones') IS NOT NULL) DROP TABLE Observaciones;
GO
CREATE TABLE Observaciones(
    ID              INT             PRIMARY KEY IDENTITY(1,1),
    IdIncidente     nVarChar(50)    NOT NULL,
    IdPaquete       nVarChar(12)    NULL,
    FechaCaptura    DateTime        NULL,
    Observaciones   nVarChar(MAX)   NOT NULL
);
GO