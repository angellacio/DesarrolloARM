USE Templates
GO

IF (SELECT COUNT(Descripcion) FROM dbo.CatDocumento where Descripcion='Imagen')>0

BEGIN
DELETE dbo.CatDocumento 
where [Descripcion]='Imagen'
END 

SELECT [IdDocumento]
      ,[Descripcion]
      ,[Configuracion]
      ,[XsdValidacion]
FROM [Templates].[dbo].[CatDocumento]