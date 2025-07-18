CREATE PROCEDURE [dbo].[Category_Delete]
	@Id INT
AS
BEGIN
	DELETE FROM [Category] 
	WHERE [Id] = @Id;
END