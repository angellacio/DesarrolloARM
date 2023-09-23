USE master
GO
IF EXISTS(SELECT * FROM DBO.SYSDATABASES WHERE NAME = 'ManejoRMA') -- SI EXISTE LA BASE DE DATOS CON ESTE NOMBRE ENTONCES HAREMOS LO SIGUIENTE
 BEGIN
	DECLARE @DatabaseName nvarchar(50)
	SET @DatabaseName = 'ManejoRMA'
	DECLARE @SQL varchar(max)

	SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
	FROM MASTER..SysProcesses
	WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId
	--SELECT @SQL
	EXEC(@SQL)
	DROP DATABASE ManejoRMA -- ELIMINAMOS LA BASE DE DATOS PORQ EXISTE
 END
GO
CREATE DATABASE ManejoRMA
GO
USE ManejoRMA
GO
CREATE TABLE Tablas(
	nIdTabla INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	Tabla nVarChar(100) NOT NULL, 
	Estado Bit NOT NULL, 
	CONSTRAINT AK_Tablas_NombreEmpleado  UNIQUE (Tabla))
GO
CREATE TABLE TablasDetalles(
	nIdTablaDetalle INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	nIdTabla INT NOT NULL REFERENCES Tablas(nIdTabla), 
	Acronimo nVarChar(150) NOT NULL, 
	Orden INT NOT NULL, 
	TablaDetalle nVarChar(250) NOT NULL, 
	Observaciones nVarChar(350) NULL, 
	Estado Bit NOT NULL, 
	CONSTRAINT AK_TablasDetalles_NombreEmpleado  UNIQUE (nIdTabla, TablaDetalle))
GO
CREATE TABLE Empleados(
	nIdEmpleado INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	nIdArea INT NULL, 
	nIdAplicativo INT NULL, 
	sNombre nVarChar(150) NOT NULL, 
	sApellido1 nVarChar(100) NOT NULL, 
	sApellido2 nVarChar(100) NULL, 
	sUsuario nVarChar(50) NOT NULL, 
	sContra nVarChar(150) NOT NULL, 
	Orden INT NULL, 
	Root Bit NOT NULL, 
	Estado Bit NOT NULL,
	CONSTRAINT AK_Empleados_NombreEmpleado  UNIQUE (sNombre, sApellido1, sApellido2), 
	CONSTRAINT AK_Empleados_Usuario  UNIQUE (sUsuario))
GO
CREATE TABLE EmpleadoArea(
	nIdEmpleado INT NOT NULL REFERENCES Empleados(nIdEmpleado),
	nIdArea INT NOT NULL REFERENCES TablasDetalles(nIdTablaDetalle), 
	CONSTRAINT AK_EmpleadoArea  UNIQUE (nIdEmpleado, nIdArea))
GO
CREATE TABLE AreaAplicativo(
	nIdArea INT NOT NULL REFERENCES TablasDetalles(nIdTablaDetalle),
	nIdAplicativo INT NOT NULL REFERENCES TablasDetalles(nIdTablaDetalle), 
	CONSTRAINT AK_AreaAplicativo  UNIQUE (nIdArea, nIdAplicativo))
GO
CREATE TABLE DatosReqs(
	nIdRequerimiento INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	nIdTipoRequerimiento INT NOT NULL REFERENCES TablasDetalles(nIdTablaDetalle), 
	nIdEstado INT NOT NULL REFERENCES TablasDetalles(nIdTablaDetalle), 
	nIdResponsable INT NOT NULL REFERENCES TablasDetalles(nIdTablaDetalle), 
	nIdAplicativo INT NOT NULL REFERENCES TablasDetalles(nIdTablaDetalle), 
	Requerimiento nVarChar(20) NOT NULL, 
	REQS nVarChar(20) NULL, 
	Titulo nVarChar(250) NULL, 
	Resumen nVarChar(350) NULL, 
	RFC nChar(15) NULL, 
	IdentificadorReq nVarChar(350) NULL, 
	FechaRecepcion DateTime NULL, 
	FechaEntrega DateTime NULL,
	ObservacionReq nVarChar(350) NULL, 
	CONSTRAINT AK_DatosReqs  UNIQUE (Requerimiento))
GO
CREATE TABLE DatosPaquete(
	nIdPaquete INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	nIdRequerimiento INT NOT NULL REFERENCES DatosReqs(nIdRequerimiento), 
	nIdRepositorio INT NOT NULL REFERENCES TablasDetalles(nIdTablaDetalle), 
	Paquete nChar(15) NULL, 
	Repositorio nVarChar(50) NULL, 
	Version nVarChar(50) NULL, 
	Resumen nVarChar(350) NULL, 
	IdentificadorPaq nVarChar(350) NULL,
	ObservacionPaq nVarChar(350) NULL, 
	CONSTRAINT AK_DatosPaquete  UNIQUE (Paquete))
GO
CREATE TABLE DatosRDL(
	nIdRDL INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	nIdPaquete INT NOT NULL REFERENCES DatosPaquete(nIdPaquete), 
	RDL INT NOT NULL, 
	Paquete nVarChar(12) NULL, 
	FechaInicio DateTime NULL, 
	FechaFin DateTime NULL,
	Id_Estado Int NULL, 
	Id_Responsable Int NULL)
GO
CREATE TABLE Observacioines(
	nIdObservaciones INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	nIdRequerimiento INT NULL REFERENCES DatosReqs(nIdRequerimiento),
	nIdPaquete INT NULL REFERENCES DatosPaquete(nIdPaquete),
	Observacion nVarChar(350) NULL)
GO
CREATE VIEW catSensillo
AS
	SELECT TDetalle.nIdTabla, Tab.Tabla, Tab.Estado AS Est_Tabla, TDetalle.nIdTablaDetalle, 
		TDetalle.Acronimo, TDetalle.Orden, TDetalle.Observaciones, TDetalle.TablaDetalle, 
		TDetalle.Estado AS Est_Detalle
	FROM Tablas AS Tab INNER JOIN
		 TablasDetalles AS TDetalle ON Tab.nIdTabla = TDetalle.nIdTabla
GO