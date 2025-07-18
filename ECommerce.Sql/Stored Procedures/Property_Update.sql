/*
This SP update record in table Property
Created By :: Rekansh Patel
Created On :: 04/23/2017
*/	
CREATE PROCEDURE [dbo].[Property_Update]
	@Id int,
	@Name varchar(50),
	@Description varchar(550)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [Property] WHERE [Name] = @Name AND [Id] != @Id)
	BEGIN
		UPDATE [Property] 
		SET [Name] = @Name,
			[Description] = @Description
		WHERE [Id] = @Id;
	END
	ELSE
		SET @Id = 0;
	
	SELECT @Id;
END

