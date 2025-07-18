CREATE PROCEDURE [dbo].[Category_SelectForLOV]
AS
BEGIN
    SELECT 
        [Id], 
        [Name]
    FROM [Category]
    ORDER BY [Id]; 
END