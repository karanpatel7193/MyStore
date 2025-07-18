CREATE PROCEDURE [dbo].[City_Update]
	@Id int,
	@Name varchar(50), 
	@StateId int
AS
/***********************************************************************************************
	 NAME     :  City_Update
	 PURPOSE  :  This SP update record in table City.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
		IF NOT EXISTS(SELECT [Id] FROM [City] WHERE [Name] = @Name AND [StateId] = @StateId AND [Id] != @Id)
		BEGIN
			UPDATE [City] 
			SET [Name] = @Name, 
				[StateId] = @StateId
			WHERE [Id] = @Id;
		END
		ELSE
			SET @Id = 0;
	
		SELECT @Id;
END