CREATE PROCEDURE [dbo].[State_Insert]
	@Name varchar(50), 
	@SortName varchar(10), 
	@CountryId smallint
AS
/***********************************************************************************************
	 NAME     :  State_Insert
	 PURPOSE  :  This SP insert record in table State.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
		DECLARE @Id int
		IF NOT EXISTS(SELECT [Id] FROM [State] WHERE [Name] = @Name AND [CountryId] = @CountryId)
		BEGIN
			INSERT INTO [State] ([Name], [SortName], [CountryId]) 
			VALUES (@Name, @SortName, @CountryId)
			SET @Id = SCOPE_IDENTITY();

			INSERT INTO [City]([StateId], [Name]) 
			VALUES (@Id, 'Other');
		END
		ELSE
			SET @Id = 0;
	
		SELECT @Id;
END