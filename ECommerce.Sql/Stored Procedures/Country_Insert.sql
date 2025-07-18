CREATE PROCEDURE [dbo].[Country_Insert]
	@Name varchar(50), 
	@SortName varchar(10), 
	@CurrencySign nvarchar(1), 
	@CurrencyCode nvarchar(3), 
	@CurrencyName varchar(20), 
	@FlagImagePath varchar(500),
	@CountryLanguages XML
AS
/***********************************************************************************************
	 NAME     :  Country_Insert
	 PURPOSE  :  This SP insert record in table Country.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	DECLARE @DocHandle int
	EXEC sp_xml_preparedocument @DocHandle OUTPUT, @CountryLanguages

	BEGIN TRANSACTION CountryInsert
	BEGIN TRY 
		DECLARE @Id smallint
		IF NOT EXISTS(SELECT [Id] FROM [Country] WHERE [Name] = @Name)
		BEGIN
			INSERT INTO [Country] ([Name], [SortName], [CurrencySign], [CurrencyCode], [CurrencyName], [FlagImagePath]) 
			VALUES (@Name, @SortName, @CurrencySign, @CurrencyCode, @CurrencyName, @FlagImagePath)

			SET @Id = SCOPE_IDENTITY();

			INSERT dbo.[CountryLanguage]([CountryId], [LanguageId])
			SELECT @Id, [LanguageId]
			FROM OPENXML (@DocHandle, 'ArrayOfClsCountryLanguageEAL/clsCountryLanguageEAL',2) WITH ([LanguageId] SMALLINT, [IsSelect] BIT)
			WHERE [IsSelect] = 1

		END
		ELSE
			SET @Id = 0;
	
		SELECT @Id;
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION CountryInsert
		
		EXEC sp_xml_removedocument @DocHandle

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION CountryInsert
		
		EXEC sp_xml_removedocument @DocHandle

		DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT
		SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(), @ErrorNumber = ERROR_NUMBER()
		RAISERROR  (@ErrorMessage,@ErrorSeverity,@ErrorState,@ErrorNumber)
	END CATCH
END