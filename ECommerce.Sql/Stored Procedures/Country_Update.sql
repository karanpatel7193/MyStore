CREATE PROCEDURE [dbo].[Country_Update]
	@Id smallint,
	@Name varchar(50), 
	@SortName varchar(10), 
	@CurrencySign nvarchar(1), 
	@CurrencyCode nvarchar(3), 
	@CurrencyName varchar(20), 
	@FlagImagePath varchar(500),
	@CountryLanguages XML
AS
/***********************************************************************************************
	 NAME     :  Country_Update
	 PURPOSE  :  This SP update record in table Country.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	DECLARE @DocHandle int
	EXEC sp_xml_preparedocument @DocHandle OUTPUT, @CountryLanguages

	BEGIN TRANSACTION CountryUpdate
	BEGIN TRY 
		IF NOT EXISTS(SELECT [Id] FROM [Country] WHERE [Name] = @Name AND [Id] != @Id)
		BEGIN
			UPDATE [Country] 
			SET [Name] = @Name, 
				[SortName] = @SortName, 
				[CurrencySign] = @CurrencySign, 
				[CurrencyCode] = @CurrencyCode, 
				[CurrencyName] = @CurrencyName, 
				[FlagImagePath] = @FlagImagePath
			WHERE [Id] = @Id;

			DELETE FROM dbo.[CountryLanguage] 
			WHERE [CountryId] = @Id
				AND LanguageId IN (
						SELECT [LanguageId]
						FROM OPENXML (@DocHandle, 'ArrayOfClsCountryLanguageEAL/clsCountryLanguageEAL',2) WITH ([LanguageId] SMALLINT, [IsSelect] BIT) CLX
						WHERE [IsSelect] = 0 
								  )

			INSERT dbo.[CountryLanguage]([CountryId], [LanguageId])
			SELECT @Id, CLX.[LanguageId]
			FROM OPENXML (@DocHandle, 'ArrayOfClsCountryLanguageEAL/clsCountryLanguageEAL',2) WITH ([LanguageId] SMALLINT, [IsSelect] BIT) CLX
				LEFT JOIN dbo.CountryLanguage CL ON CLX.[LanguageId] = CL.[LanguageId] AND CL.[CountryId] = @Id
			WHERE [IsSelect] = 1 AND CL.[Id] IS NULL
		END
		ELSE
			SET @Id = 0;
	
		SELECT @Id;
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION CountryUpdate

		EXEC sp_xml_removedocument @DocHandle

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION CountryUpdate
		
		EXEC sp_xml_removedocument @DocHandle

		DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT
		SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(), @ErrorNumber = ERROR_NUMBER()
		RAISERROR  (@ErrorMessage,@ErrorSeverity,@ErrorState,@ErrorNumber)
	END CATCH
END