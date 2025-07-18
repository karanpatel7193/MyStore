CREATE PROCEDURE [dbo].[Category_SelectChild]
	@ParentId int
AS
BEGIN
	SELECT A.[Id], A.[Name]
	FROM [Category] A
	WHERE	A.ParentId = @ParentId AND A.ParentId IS NOT NULL
	ORDER BY [Name]
END

