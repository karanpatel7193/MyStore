CREATE PROCEDURE [dbo].[Wishlist_Insert]
    @UserId    BIGINT,
    @ProductId INT
AS
BEGIN
    DECLARE @Id INT;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Wishlist] WHERE [UserId] = @UserId AND [ProductId] = @ProductId)
    BEGIN
        INSERT INTO [dbo].[Wishlist] ([UserId], [ProductId],[CreatedTime])
        VALUES (@UserId, @ProductId ,GETDATE());

        SET @Id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SET @Id = 0;
    END

    SELECT @Id;
END;
