CREATE PROCEDURE [dbo].[Cart_Update]
    @UserId BIGINT,
    @ProductId INT,
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Cart]
    SET [Quantity] = @Quantity
    WHERE [UserId] = @UserId AND [ProductId] = @ProductId;

    SELECT @@ROWCOUNT AS RowsAffected;
END;
