CREATE PROCEDURE [dbo].[Wishlist_Delete]
    @UserId    BIGINT,
    @ProductId INT
AS
BEGIN
    DECLARE @RowsAffected INT;

    DELETE FROM [dbo].[Wishlist] 
    WHERE [UserId] = @UserId AND [ProductId] = @ProductId;

    SET @RowsAffected = @@ROWCOUNT;

    SELECT @RowsAffected;
END;
