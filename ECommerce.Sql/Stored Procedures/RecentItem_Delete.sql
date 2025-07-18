CREATE PROCEDURE [dbo].[RecentItem_Delete]
    @Id INT
AS
BEGIN
    DELETE FROM [dbo].[RecentItem]
    WHERE [Id] = @Id;
END;