CREATE PROCEDURE [dbo].[Category_SelectParent]
AS
BEGIN
    SELECT 
        [Id], 
        [Name],
        [ImageUrl]
    FROM [Category]
    WHERE [ParentId] IS NULL 
      AND [IsVisible] = 1 
    ORDER BY [Id]; 
END
