USE [master]
GO

--CREA LOGIN IDEWEB
IF NOT EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE name = N'XXDominio\UsrIDEWEB')
	BEGIN
		--Generar el usuario en el manejador de Base de Datos
		CREATE LOGIN [XXDominio\UsrIDEWEB] FROM WINDOWS WITH 
        DEFAULT_DATABASE=[IDEWEB]
	END

GO
ALTER LOGIN [UsrIDEWEB] ENABLE

IF IS_SRVROLEMEMBER ( 'bulkadmin' , N'XXDominio\UsrIDEWEB' ) = 0
	BEGIN
		EXEC master..sp_addsrvrolemember @loginame = N'XXDominio\UsrIDEWEB', @rolename = N'bulkadmin'
	END
GO

USE [IDEWEB]
GO
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = N'XXDominio\UsrIDEWEB')
	BEGIN
		CREATE USER [XXDominio\UsrIDEWEB] FOR LOGIN [XXDominio\UsrIDEWEB]
		EXEC sp_addrolemember N'db_owner', N'XXDominio\UsrIDEWEB'
	END

ALTER AUTHORIZATION ON SCHEMA::[db_owner] TO [XXDominio\UsrIDEWEB]
GO

