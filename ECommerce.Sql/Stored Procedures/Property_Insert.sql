/*
This SP insert record in table Property
Created By :: Rekansh Patel
Created On :: 04/23/2017
*/	
CREATE PROCEDURE [dbo].[Property_Insert]
	@Name varchar(50),
	@Description varchar(550)
AS
BEGIN
	DECLARE @Id int
	IF NOT EXISTS(SELECT [Id] FROM [Property] WHERE [Name] = @Name)
	BEGIN
		INSERT INTO [Property] ([Name], [Description]) 
		VALUES (@Name, @Description)
		SET @Id = SCOPE_IDENTITY();
	END
	ELSE
		SET @Id = 0;
	
	SELECT @Id;
END

