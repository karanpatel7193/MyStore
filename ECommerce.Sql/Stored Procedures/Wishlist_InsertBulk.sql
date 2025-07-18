
CREATE PROCEDURE [dbo].[Wishlist_InsertBulk]
    @UserId BIGINT,
    @WishlistXML XML
AS
BEGIN
    BEGIN TRANSACTION WishlistInsertBulk;
    BEGIN TRY
        DECLARE @DocHandle INT;
        EXEC sp_xml_preparedocument @DocHandle OUTPUT, @WishlistXML;

        -- Temporary table to hold XML data
        DECLARE @TempWishlist TABLE (
            ProductId INT,
            CreatedTime DATETIME
        );

        -- Extract XML data into the temp table
        INSERT INTO @TempWishlist (ProductId, CreatedTime)
        SELECT ProductId, CreatedTime
        FROM OPENXML(@DocHandle, 'ArrayOfWishlistEntity/WishlistEntity', 2)
        WITH (
            ProductId INT,
            CreatedTime DATETIME
        );

        -- Remove XML document reference
        EXEC sp_xml_removedocument @DocHandle;

        -- Insert new wishlist items that do not exist
        INSERT INTO [dbo].[Wishlist] ([UserId], [ProductId], [CreatedTime])
        SELECT @UserId, T.ProductId, ISNULL(T.CreatedTime, GETDATE())
        FROM @TempWishlist T
        WHERE NOT EXISTS (
            SELECT 1 FROM [dbo].[Wishlist] W WHERE W.ProductId = T.ProductId AND W.UserId = @UserId
        );

        -- Commit transaction
        COMMIT TRANSACTION WishlistInsertBulk;
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of an error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION WishlistInsertBulk;

        -- Capture error details
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;