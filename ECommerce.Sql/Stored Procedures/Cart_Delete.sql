CREATE PROCEDURE [dbo].[Cart_Delete]
    @UserId    BIGINT,
    @ProductId INT
AS
BEGIN
    DECLARE @RowsAffected INT;

    DELETE FROM [dbo].[Cart] 
    WHERE [UserId] = @UserId AND [ProductId] = @ProductId;

    SET @RowsAffected = @@ROWCOUNT;

    SELECT @RowsAffected;
END;
