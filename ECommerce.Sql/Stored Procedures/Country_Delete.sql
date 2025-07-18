CREATE PROCEDURE [dbo].[Country_Delete]
	@Id smallint
AS
/***********************************************************************************************
	 NAME     :  Country_Delete
	 PURPOSE  :  This SP delete record from table Country
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	BEGIN TRANSACTION CountryDelete
	BEGIN TRY 
		DELETE FROM [CountryLanguage] 
		WHERE [CountryId] = @Id;

		DELETE FROM [Country] 
		WHERE [Id] = @Id;

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION CountryDelete
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION CountryDelete
		
		DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT
		SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(), @ErrorNumber = ERROR_NUMBER()
		RAISERROR  (@ErrorMessage,@ErrorSeverity,@ErrorState,@ErrorNumber)
	END CATCH
END