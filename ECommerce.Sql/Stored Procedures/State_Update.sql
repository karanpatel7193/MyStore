CREATE PROCEDURE [dbo].[State_Update]
	@Id int,
	@Name varchar(50), 
	@SortName varchar(10), 
	@CountryId smallint
AS
/***********************************************************************************************
	 NAME     :  State_Update
	 PURPOSE  :  This SP update record in table State.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
		IF NOT EXISTS(SELECT [Id] FROM [State] WHERE [Name] = @Name AND [CountryId] = @CountryId AND [Id] != @Id)
		BEGIN
			UPDATE [State] 
			SET [Name] = @Name, 
				[SortName] = @SortName, 
				[CountryId] = @CountryId
			WHERE [Id] = @Id;
		END
		ELSE
			SET @Id = 0;
	
		SELECT @Id;
END