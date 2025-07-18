CREATE PROCEDURE [dbo].[Product_Delete]
    @Id INT
AS
BEGIN
    WHILE (1 = 1)
    BEGIN
        DECLARE @ChildId INT = NULL

        SELECT  TOP 1 @ChildId = Id  
        FROM    dbo.[Product] P 
        WHERE   P.ParentProductId = @Id

        IF @ChildId IS NOT NULL
            EXEC [dbo].[Product_Delete] @ChildId
        ELSE
            BREAK
    END

    BEGIN TRANSACTION ProductDelete
    BEGIN TRY
        DELETE FROM [dbo].[ProductInventory]
        WHERE [ProductId] = @Id;

        DELETE FROM [dbo].[ProductMedia]
        WHERE [ProductId] = @Id;

        DELETE FROM [dbo].[ProductProperty]
        WHERE [ProductId] = @Id;

        DELETE FROM [dbo].[Cart]
        WHERE [ProductId] = @Id;

        DELETE FROM [dbo].[Wishlist]
        WHERE [ProductId] = @Id;

        DELETE FROM [dbo].[ProductVariant]
        WHERE [ProductId] = @Id;

        DELETE FROM [dbo].[BlockProduct]
        WHERE [ProductId] = @Id

        DELETE FROM [dbo].[Product]
        WHERE [Id] = @Id;

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION ProductDelete;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION ProductDelete;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(), 
            @ErrorSeverity = ERROR_SEVERITY(), 
            @ErrorState = ERROR_STATE(), 
            @ErrorNumber = ERROR_NUMBER();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState, @ErrorNumber);
    END CATCH
END;
