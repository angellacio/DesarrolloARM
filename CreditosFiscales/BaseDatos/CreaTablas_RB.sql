--- Plan de retorno creaci�n de tablas

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblControlPagosDet]') AND type in (N'U'))
DROP TABLE [dbo].tblControlPagosDet
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblControlPagosHead]') AND type in (N'U'))
DROP TABLE [dbo].tblControlPagosHead
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblArchivosZIP]') AND type in (N'U'))
DROP TABLE [dbo].[tblArchivosZIP]
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CatEstadoProceso]') AND type in (N'U'))
DROP TABLE [dbo].[CatEstadoProceso]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CatErrorProceso]') AND type in (N'U'))
DROP TABLE [dbo].[CatErrorProceso]
GO