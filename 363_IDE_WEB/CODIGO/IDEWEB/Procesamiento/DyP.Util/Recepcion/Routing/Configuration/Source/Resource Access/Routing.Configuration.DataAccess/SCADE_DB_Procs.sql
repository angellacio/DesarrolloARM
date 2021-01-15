
IF NOT EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'RethrowError')
BEGIN
	EXEC('CREATE PROCEDURE [dbo].RethrowError AS RETURN')
END
GO

ALTER PROCEDURE RethrowError AS
    /* Return if there is no error information to retrieve. */
    IF ERROR_NUMBER() IS NULL
        RETURN;

    DECLARE
        @ErrorMessage    NVARCHAR(4000),
        @ErrorNumber     INT,
        @ErrorSeverity   INT,
        @ErrorState      INT,
        @ErrorLine       INT,
        @ErrorProcedure  NVARCHAR(200); 

    /* Assign variables to error-handling functions that
       capture information for RAISERROR. */

    SELECT
        @ErrorNumber = ERROR_NUMBER(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE(),
        @ErrorLine = ERROR_LINE(),
        @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-'); 

    /* Building the message string that will contain original
       error information. */

    SELECT @ErrorMessage = 
        N'Error %d, Level %d, State %d, Procedure %s, Line %d, ' + 
         'Message: '+ ERROR_MESSAGE(); 

    /* Raise an error: msg_str parameter of RAISERROR will contain
	   the original error information. */

    RAISERROR(@ErrorMessage, @ErrorSeverity, 1,
        @ErrorNumber,    /* parameter: original error number. */
        @ErrorSeverity,  /* parameter: original error severity. */
        @ErrorState,     /* parameter: original error state. */
        @ErrorProcedure, /* parameter: original error procedure name. */
        @ErrorLine       /* parameter: original error line number. */
        );

GO

----------------------------------------------------------------
-- [dbo].[SCADERoute] Table
--
IF NOT EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'InsertSCADERoute')
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[InsertSCADERoute] AS RETURN')
END

GO

ALTER PROCEDURE [dbo].[InsertSCADERoute]
    @description text = NULL,
	@destination nvarchar(255),
	@id_route int OUT,
	@soap_action nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON
	
	BEGIN TRY
    INSERT INTO [dbo].[SCADERoute] ([description], [destination], [soap_action])
	VALUES (@description, @destination, @soap_action)
    SET @id_route = SCOPE_IDENTITY()
    END TRY

    BEGIN CATCH
		EXEC RethrowError;
	END CATCH
    
    SET NOCOUNT OFF
END    

GO

IF NOT EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'UpdateSCADERoute')
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[UpdateSCADERoute] AS RETURN')
END

GO

ALTER PROCEDURE [dbo].[UpdateSCADERoute]
    @description text = NULL,
	@destination nvarchar(255),
	@id_route int,
	@soap_action nvarchar(255)
AS
BEGIN

	--The [dbo].[SCADERoute] table doesn't have a timestamp column. Optimistic concurrency logic cannot be generated
	SET NOCOUNT ON

	BEGIN TRY
	UPDATE [dbo].[SCADERoute] 
	SET [description] = @description, [destination] = @destination, [soap_action] = @soap_action
	WHERE [id_route]=@id_route

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END
    END TRY

    BEGIN CATCH
		EXEC RethrowError;
	END CATCH	

	SET NOCOUNT OFF
END

GO

IF NOT EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'DeleteSCADERoute')
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[DeleteSCADERoute] AS RETURN')
END

GO

ALTER PROCEDURE [dbo].[DeleteSCADERoute]
	 @id_route int
AS
BEGIN
	SET NOCOUNT ON
	
    DELETE FROM [dbo].[SCADERoute]
	WHERE [id_route]=@id_route
    
    SET NOCOUNT OFF
END

GO

IF NOT EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'GetAllFromSCADERoute')
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[GetAllFromSCADERoute] AS RETURN')
END

GO

ALTER PROCEDURE [dbo].[GetAllFromSCADERoute]    
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
	[SCADERoute].[description] AS 'description',
	[SCADERoute].[destination] AS 'destination',
	[SCADERoute].[id_route] AS 'id_route',
	[SCADERoute].[soap_action] AS 'soap_action'
FROM [dbo].[SCADERoute] [SCADERoute]

	SET NOCOUNT OFF
END

GO

IF NOT EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'GetSCADERouteByid_route')
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[GetSCADERouteByid_route] AS RETURN')
END

GO

ALTER PROCEDURE [dbo].[GetSCADERouteByid_route] 
	@id_route int
AS
BEGIN

	SET NOCOUNT ON
	
	SELECT
	[SCADERoute].[description] AS 'description',
	[SCADERoute].[destination] AS 'destination',
	[SCADERoute].[id_route] AS 'id_route',
	[SCADERoute].[soap_action] AS 'soap_action'
	FROM [dbo].[SCADERoute] [SCADERoute]
	WHERE [id_route]=@id_route

	SET NOCOUNT OFF
END

GO


