CREATE PROCEDURE [dbo].[City_Insert]
	@Name varchar(50), 
	@StateId int
AS
/***********************************************************************************************
	 NAME     :  City_Insert
	 PURPOSE  :  This SP insert record in table City.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
		DECLARE @Id int
		IF NOT EXISTS(SELECT [Id] FROM [City] WHERE [Name] = @Name AND [StateId] = @StateId)
		BEGIN
			INSERT INTO [City] ([Name], [StateId]) 
			VALUES (@Name, @StateId)
			SET @Id = SCOPE_IDENTITY();
		END
		ELSE
			SET @Id = 0;
	
		SELECT @Id;
END