
CREATE PROCEDURE [dbo].[CategoryProperty_SelectForEdit]
	@Id int
AS

BEGIN
	EXEC CategoryProperty_SelectForRecord @Id

	EXEC CategoryProperty_SelectForAdd
END