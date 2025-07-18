CREATE PROCEDURE [dbo].[Home_SelectForCategoryList]
AS
BEGIN
    SELECT 
        [Id], 
        [Name],
        [ParentId],
        [ImageUrl]
    FROM [Category]
    WHERE [IsVisible] = 1 
    ORDER BY [Id]; 
END
