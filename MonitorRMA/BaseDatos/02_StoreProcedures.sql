USE ManejoRMA
GO
DROP PROCEDURE IF EXISTS catEmpleadoConsulta
GO
CREATE PROCEDURE catEmpleadoConsulta(
	@nTipoConsulta INT,
	@nIdEmpleado INT,
	@sNombre nVarChar(100) = NULL,
	@sApellido1 nVarChar(100) = NULL,
	@sApellido2 nVarChar(100) = NULL,
	@sUsuario nVarChar(100) = NULL,
	@bEstado BIT = NULL)
AS
 BEGIN
	IF @nTipoConsulta = 1
	 BEGIN
		DECLARE @sQuery nVarChar(MAX)
		SET @sQuery = 'SELECT nIdEmpleado, nIdArea, nIdAplicativo, sNombre, sApellido1, sApellido2, sUsuario, sContra, Orden, Root, Estado FROM Empleados WHERE'
		IF @nIdEmpleado IS NOT NULL SET @sQuery = @sQuery + ' nIdEmpleado = ' + CONVERT(nVarChar, @nIdEmpleado) + ' AND1'
		IF @sNombre IS NOT NULL SET @sQuery = @sQuery + ' sNombre LIKE ''%' + @sNombre + '%'' AND1'
		IF @sApellido1 IS NOT NULL SET @sQuery = @sQuery + ' sApellido1 LIKE ''%' + @sApellido1 + '%'' AND1'
		IF @sApellido2 IS NOT NULL SET @sQuery = @sQuery + ' sApellido2 LIKE ''%' + @sApellido2 + '%'' AND1'
		IF @sUsuario IS NOT NULL SET @sQuery = @sQuery + ' sUsuario LIKE ''%' + @sUsuario + '%'' AND1'
		IF @bEstado IS NOT NULL SET @sQuery = @sQuery + ' Estado = ' + CONVERT(nVarChar, @bEstado) + ' AND1'

		SET @sQuery = LEFT(@sQuery, LEN(@sQuery) - 5)
		SET @sQuery = REPLACE(@sQuery, 'AND1', 'AND') + ' ORDER BY Orden'

		--select @sQuery
		EXEC sp_executesql @sQuery
	 END
	ELSE IF @nTipoConsulta = 2
	 BEGIN
		DECLARE @nRoot BIT

		SELECT @nRoot = Root FROM Empleados WHERE nIdEmpleado = @nIdEmpleado

		SELECT nIdEmpleado, nIdArea, nIdAplicativo, sNombre, sApellido1, sApellido2, sUsuario, sContra, Orden, Root, Estado
		FROM Empleados
		WHERE nIdEmpleado = @nIdEmpleado
		ORDER BY Orden

		IF (@nRoot = 1)
		 BEGIN
			SELECT null AS 'nIdArea', nIdTabla, Tabla, Est_Tabla, nIdTablaDetalle, Acronimo, Orden, Observaciones, TablaDetalle, Est_Detalle
			FROM catSensillo
			WHERE nIdTabla = 3
			ORDER BY Orden

			SELECT aA.nIdArea, cS.nIdTabla, cS.Tabla, cS.Est_Tabla, aA.nIdAplicativo AS nIdTablaDetalle, cS.Acronimo, cS.Orden, cS.Observaciones, cS.TablaDetalle, cS.Est_Detalle
			FROM AreaAplicativo AS aA INNER JOIN
				 catSensillo AS cS ON aA.nIdAplicativo = cS.nIdTablaDetalle
			ORDER BY aA.nIdArea, cS.Orden
		 END
		ELSE
		 BEGIN
			SELECT null AS 'nIdArea', cS.nIdTabla, cS.Tabla, cS.Est_Tabla, eA.nIdArea AS nIdTablaDetalle, cS.Acronimo, cS.Orden, cS.Observaciones, cS.TablaDetalle, cS.Est_Detalle
			FROM EmpleadoArea AS eA INNER JOIN
				 catSensillo AS cS ON eA.nIdArea = cS.nIdTablaDetalle
			WHERE eA.nIdEmpleado = @nIdEmpleado
			ORDER BY Orden

			SELECT aA.nIdArea, cS.nIdTabla, cS.Tabla, cS.Est_Tabla, aA.nIdAplicativo AS nIdTablaDetalle, cS.Acronimo, cS.Orden, cS.Observaciones, cS.TablaDetalle, cS.Est_Detalle
			FROM AreaAplicativo AS aA INNER JOIN
				 catSensillo AS cS ON aA.nIdAplicativo = cS.nIdTablaDetalle
			WHERE aA.nIdArea IN(SELECT nIdArea FROM EmpleadoArea WHERE nIdEmpleado = @nIdEmpleado)
			ORDER BY aA.nIdArea, cS.Orden
		 END
	 END
 END
GO
DROP PROCEDURE IF EXISTS catEmpleadoManejo
GO
CREATE PROCEDURE catEmpleadoManejo(
	@nTipoManejo INT,
	@nIdEmpleado INT = NULL,
	@sNombre nVarChar(100) = NULL,
	@sApellido1 nVarChar(100) = NULL,
	@sApellido2 nVarChar(100) = NULL,
	@sUsuario nVarChar(100) = NULL,
	@nOrden INT = NULL,
	@bRoot BIT = 0,
	@bEstado BIT = NULL,
	@nIdArea INT = NULL,
	@nIdAplicativo INT = NULL)
AS
 BEGIN
	IF @nTipoManejo = 1 -- Alta empleado
	 BEGIN
		INSERT INTO Empleados(sNombre, sApellido1, sApellido2, sUsuario, sContra, Orden, Root, Estado)
					VALUES(@sNombre, @sApellido1, @sApellido2, @sUsuario, '12345678a', @nOrden, @bRoot, @bEstado)
		SELECT @nIdEmpleado = SCOPE_IDENTITY()
	 END
	ELSE IF @nTipoManejo = 2 -- Actualiza empleado
	 BEGIN
		UPDATE Empleados 
		   SET sNombre = @sNombre, sApellido1 = @sApellido1, sApellido2 = @sApellido2, 
			   sUsuario = @sUsuario, Orden = @nOrden, Root = @bRoot, Estado = @bEstado
		WHERE nIdEmpleado = @nIdEmpleado
	 END
	ELSE IF @nTipoManejo = 3 -- Alta Empleado Area
	 BEGIN
		INSERT INTO EmpleadoArea(nIdEmpleado, nIdArea) VALUES(@nIdEmpleado, @nIdArea)
	 END
	ELSE IF @nTipoManejo = 4 -- Elimina Empleado Area
	 BEGIN
		DELETE FROM EmpleadoArea WHERE nIdEmpleado = @nIdEmpleado AND nIdArea = @nIdArea
	 END
	ELSE IF @nTipoManejo = 5 -- Alta Area Aplicativo
	 BEGIN
		INSERT INTO AreaAplicativo(nIdArea, nIdAplicativo) VALUES(@nIdArea, @nIdAplicativo)
	 END
	ELSE IF @nTipoManejo = 6 -- Elimina Area Aplicativo
	 BEGIN
		DELETE FROM AreaAplicativo WHERE nIdArea = @nIdArea AND nIdAplicativo = @nIdAplicativo
	 END
	EXEC catEmpleadoConsulta 2, @nIdEmpleado, NULL, NULL, NULL, NULL, NULL
 END
GO
DROP PROCEDURE IF EXISTS catSensilloConsulta
GO
CREATE PROCEDURE catSensilloConsulta(
	@nTipoConsulta INT,
	@nIdTbla INT,
	@sTabla nVarChar(100) = NULL,
	@bEstadoT BIT = NULL,
	@nIdDetalle INT = NULL,
	@sAcronimo nVarChar(150) = NULL,
	@sObservaciones nVarChar(350) = NULL,
	@sTablaDetalle nVarChar(250) = NULL,
	@bEstadoD BIT = NULL)
AS
 BEGIN
	DECLARE @sQuery nVarChar(MAX)
	IF @nTipoConsulta = 1 -- Consulta Tablas
	 BEGIN
		SET @sQuery = 'SELECT nIdTabla, Tabla, Estado FROM Tablas WHERE'
		IF @nIdTbla IS NOT NULL SET @sQuery = @sQuery + ' nIdTabla = ' + CONVERT(nVarChar, @nIdTbla) + ' AND1 '
		IF @sTabla IS NOT NULL SET @sQuery = @sQuery + ' Tabla LIKE ''%' + @sTabla + '%'' AND1'
		IF @bEstadoT IS NOT NULL SET @sQuery = @sQuery + ' Estado = ' + CONVERT(nVarChar, @bEstadoT) + ' AND1'

		SET @sQuery = LEFT(@sQuery, LEN(@sQuery) - 5)
		SET @sQuery = REPLACE(@sQuery, 'AND1', 'AND') + ' ORDER BY nIdTabla'

		--select @sQuery
		EXEC sp_executesql @sQuery
	 END
	ELSE IF @nTipoConsulta = 2 -- Consulta Detalle
	 BEGIN
		SET @sQuery = 'SELECT nIdTabla, Tabla, Est_Tabla, nIdTablaDetalle, Acronimo, Orden, Observaciones, TablaDetalle, Est_Detalle FROM catSensillo WHERE nIdTabla = ' + CONVERT(nVarChar, @nIdTbla) + ' AND1 '
		IF @sTabla IS NOT NULL SET @sQuery = @sQuery + ' Tabla LIKE ''%' + @sTabla + '%'' AND1'
		IF @bEstadoT IS NOT NULL SET @sQuery = @sQuery + ' Est_Tabla = ' + CONVERT(nVarChar, @bEstadoT) + ' AND1'
		IF @nIdDetalle IS NOT NULL SET @sQuery = @sQuery + ' nIdTablaDetalle = ' + CONVERT(nVarChar, @nIdDetalle) + ' AND1'
		IF @sAcronimo IS NOT NULL SET @sQuery = @sQuery + ' Acronimo LIKE ''%' + @sAcronimo + '%'' AND1'
		IF @sObservaciones IS NOT NULL SET @sQuery = @sQuery + ' Observaciones LIKE ''%' + @sObservaciones + '%'' AND1'
		IF @sTablaDetalle IS NOT NULL SET @sQuery = @sQuery + ' TablaDetalle LIKE ''%' + @sTablaDetalle + '%'' AND1'

		SET @sQuery = LEFT(@sQuery, LEN(@sQuery) - 5)
		SET @sQuery = REPLACE(@sQuery, 'AND1', 'AND') + ' ORDER BY nIdTabla, Orden'

		--select @sQuery
		EXEC sp_executesql @sQuery
	 END
 END
GO
DROP PROCEDURE IF EXISTS catSensilloManejo
GO
CREATE PROCEDURE catSensilloManejo(
	@nTipoManejo INT,
	@nIdTbla INT,
	@sTabla nVarChar(100) = NULL,
	@bEstadoT BIT = NULL,
	@nIdDetalle INT = NULL,
	@sAcronimo nVarChar(150) = NULL,
	@nOrden INT = NULL,
	@sObservaciones nVarChar(350) = NULL,
	@sTablaDetalle nVarChar(250) = NULL,
	@bEstadoD BIT = NULL)
AS
 BEGIN
	IF @nTipoManejo = 1 -- Alta Tablas
	 BEGIN
		INSERT INTO Tablas (Tabla, Estado) VALUES (@sTabla, @bEstadoT)

		EXEC catSensilloConsulta 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
	 END
	ELSE IF @nTipoManejo = 2 -- Actualiza Tablas
	 BEGIN
		UPDATE Tablas SET Tabla = @sTabla, Estado = @bEstadoT WHERE nIdTabla = @nIdTbla

		EXEC catSensilloConsulta 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
	 END
	ELSE IF @nTipoManejo = 3 -- Alta Detalle
	 BEGIN
		INSERT INTO TablasDetalles(nIdTabla, Acronimo, Orden, TablaDetalle, Observaciones, Estado)
						VALUES (@nIdTbla, @sAcronimo, @nOrden, @sTablaDetalle, @sObservaciones, @bEstadoD)
		
		EXEC catSensilloConsulta 2, @nIdTbla, NULL, NULL, NULL, NULL, NULL, NULL, NULL
	 END
	ELSE IF @nTipoManejo = 4 -- Actualiza Detalle
	 BEGIN
		UPDATE TablasDetalles 
			SET nIdTabla = @nIdTbla, Acronimo = @sAcronimo, Orden = @nOrden, 
				TablaDetalle = @sTablaDetalle, Observaciones = @sObservaciones, Estado = @bEstadoD
		WHERE nIdTablaDetalle = @nIdDetalle

		EXEC catSensilloConsulta 2, @nIdTbla, NULL, NULL, NULL, NULL, NULL, NULL, NULL
	 END
 END
GO
DROP PROCEDURE IF EXISTS catSeguridadUsuario
GO
CREATE PROCEDURE catSeguridadUsuario(
	@nTipoConsulta INT,
	@nIdUsuario INT,
	@sUsuario nVarChar(50),
	@sContrasenia nVarChar(150))
AS
 BEGIN
	IF @nTipoConsulta = 1 -- Valida credenciales
	 BEGIN
		DECLARE @nIdEmpleado INT

		SELECT @nIdEmpleado = nIdEmpleado FROM Empleados WHERE sUsuario = @sUsuario AND sContra = @sContrasenia AND Estado = 1

		EXEC catEmpleadoConsulta 2, @nIdEmpleado, NULL, NULL, NULL, NULL, NULL
	 END
	ELSE IF @nTipoConsulta = 2 -- Actualiza credenciales
	 BEGIN
		UPDATE Empleados SET sContra = @sContrasenia WHERE nIdEmpleado = @nIdUsuario
	 END
 END
GO
DROP PROCEDURE IF EXISTS manRequerimiento
GO
CREATE PROCEDURE manRequerimiento(
	@nTipoConsulta INT,
	@nIdRequerimiento INT,
	@nIdTipoRequerimiento INT,
	@nIdEstado INT,
	@nIdResponsable INT,
	@nIdAplicativo INT,
	@nRequerimiento nVarChar(20),
	@sREQS nVarChar(20),
	@sTitulo nVarChar(250),
	@sResumen nVarChar(350),
	@sRFC nChar(15),
	@sIdentificadorReq nVarChar(350),
	@FechaRecepcion DateTime,
	@FechaEntrega DateTime,
	@sObservacionReq nVarChar(350))
AS
 BEGIN
	IF @nTipoConsulta = 1 -- Inserta Dato de Requerimiento
	 BEGIN
		INSERT INTO DatosReqs(nIdTipoRequerimiento, nIdEstado, nIdResponsable, nIdAplicativo, 
							  Requerimiento, REQS, Titulo, Resumen, RFC, IdentificadorReq, 
							  FechaRecepcion, FechaEntrega, ObservacionReq) 
		VALUES (@nIdTipoRequerimiento, @nIdEstado, @nIdResponsable, @nIdAplicativo,
				@nRequerimiento, @sREQS, @sTitulo, @sResumen, @sRFC, @sIdentificadorReq,
				@FechaRecepcion, @FechaEntrega, @sObservacionReq)

		SELECT @nIdRequerimiento = SCOPE_IDENTITY();
	  END
	 ELSE IF @nTipoConsulta = 2
	  BEGIN
		UPDATE DatosReqs
		   SET nIdTipoRequerimiento = @nIdTipoRequerimiento, nIdEstado = @nIdEstado, 
			   nIdResponsable = @nIdResponsable, nIdAplicativo = @nIdAplicativo, 
			   Requerimiento = @nRequerimiento,REQS = @sREQS, Titulo = @sTitulo, 
			   Resumen = @sResumen, RFC = @sRFC, IdentificadorReq = @sIdentificadorReq, 
			   FechaRecepcion = @FechaRecepcion, FechaEntrega = @FechaEntrega, 
			   ObservacionReq = @sObservacionReq
		WHERE nIdRequerimiento = @nIdRequerimiento
	  END

 END
GO

/*
EXEC catEmpleadoConsulta 1, null, null, 'a', 'c', 'r', 1
EXEC catEmpleadoConsulta 2, 1, null, null, null, null, null

DECLARE @nTipoConsulta INT, @nIdTbla INT, @sTabla nVarChar(100), @bEstadoT BIT, @nIdDetalle INT, @sAcronimo nVarChar(150), @sObservaciones nVarChar(350), @sTablaDetalle nVarChar(250), @bEstadoD BIT
SET @nTipoConsulta = 2
SET @nIdTbla = 4
SET @sTabla = NULL
SET @bEstadoT = NULL
SET @nIdDetalle = NULL
SET @sAcronimo = NULL
SET @sObservaciones = 'RAPE'
SET @sTablaDetalle = 'A'
SET @bEstadoD = NULL
EXEC catSensilloConsulta @nTipoConsulta, @nIdTbla, @sTabla, @bEstadoT, @nIdDetalle, @sAcronimo, @sObservaciones, @sTablaDetalle, @bEstadoD

EXEC catSeguridadUsuario 1, NULL, 'aramirez', 'oEFPCa1Z1k+qV2t65a98yA=='
EXEC catSeguridadUsuario 1, NULL, 'idias', 'oEFPCa1Z1k+qV2t65a98yA=='
EXEC catSeguridadUsuario 2, 5, NULL, 'oEFPCa1Z1k+qV2t65a98yA=='


*/