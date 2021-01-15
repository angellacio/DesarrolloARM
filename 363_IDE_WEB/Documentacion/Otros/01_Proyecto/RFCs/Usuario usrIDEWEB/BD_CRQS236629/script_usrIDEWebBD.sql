USE [master]
GO
--CREA LOGIN IDEWEB
IF NOT EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE name = N'usrIDEWebBD')
	BEGIN
		--Generar el usuario en el manejador de Base de Datos
		CREATE LOGIN usrIDEWebBD WITH PASSWORD = '12345678a'
	END
GO
ALTER LOGIN [usrIDEWebBD] ENABLE
IF IS_SRVROLEMEMBER ( 'public' , N'usrIDEWebBD' ) = 0
	BEGIN
		EXEC master..sp_addsrvrolemember @loginame = N'usrIDEWebBD', @rolename = N'public'
	END
GO
USE [DyPUtil]
GO
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = N'usrIDEWebBD')
	BEGIN
		CREATE USER [usrIDEWebBD] FOR LOGIN usrIDEWebBD
		EXEC sp_addrolemember N'db_owner', N'usrIDEWebBD'
	END

ALTER AUTHORIZATION ON SCHEMA::[db_owner] TO [usrIDEWebBD]
GO
USE [IdeWeb]
GO
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = N'usrIDEWebBD')
	BEGIN
		CREATE USER [usrIDEWebBD] FOR LOGIN usrIDEWebBD
		EXEC sp_addrolemember N'db_owner', N'usrIDEWebBD'
	END

ALTER AUTHORIZATION ON SCHEMA::[db_owner] TO [usrIDEWebBD]
GO
