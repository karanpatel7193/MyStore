CREATE PROCEDURE [dbo].[CategoryProperty_SelectForRecord]
    @Id INT
AS
BEGIN
    SELECT 
        [Id],
        [CategoryId],
        [PropertyId],
        [Unit]
    FROM [dbo].[CategoryProperty]
    WHERE [Id] = @Id;
END