CREATE PROCEDURE [dbo].[RecentItem_Update]
    @Id INT,
    @ProductId INT,
    @UserId BIGINT
AS
BEGIN
    UPDATE [dbo].[RecentItem]
    SET [ProductId] = @ProductId, [UserId] = @UserId
    WHERE [Id] = @Id;
END;