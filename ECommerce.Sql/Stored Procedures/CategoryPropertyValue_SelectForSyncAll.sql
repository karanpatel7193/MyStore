CREATE PROCEDURE [dbo].[CategoryPropertyValue_SelectForSyncAll]
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM [CategoryPropertyValue];

        INSERT INTO [CategoryPropertyValue] ([CategoryId], [PropertyId], [Value], [Unit])
        SELECT 
            P.[CategoryId], 
            PP.[PropertyId], 
            PP.[Value], 
            PP.[Unit]
        FROM 
            [Product] P
        INNER JOIN 
            [ProductProperty] PP ON P.[Id] = PP.[ProductId]
        GROUP BY 
            P.[CategoryId], 
            PP.[PropertyId], 
            PP.[Value], 
            PP.[Unit];

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
