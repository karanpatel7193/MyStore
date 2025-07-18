CREATE PROCEDURE [dbo].[RecentItem_Insert]
    @ProductId INT,
    @UserId BIGINT
AS
BEGIN
    INSERT INTO [dbo].[RecentItem] ([ProductId], [UserId])
    VALUES (@ProductId, @UserId);
END;