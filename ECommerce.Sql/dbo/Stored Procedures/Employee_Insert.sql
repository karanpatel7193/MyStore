	
CREATE PROCEDURE [dbo].[Employee_Insert]
	@FirstName nvarchar(50), 	@MiddleName nvarchar(50) = NULL, 	@LastName nvarchar(50), 	@Gender nvarchar(10), 	@Email nvarchar(100), 	@PhoneNumber nvarchar(20) = NULL, 	@DOB date, 	@DateOfJoin date, 	@Education nvarchar(50) = NULL, 	@CityId int = NULL, 	@StateId int = NULL, 	@CountryId smallint = NULL
AS
/***********************************************************************************************
	 NAME     :  Employee_Insert
	 PURPOSE  :  This SP insert record in table Employee.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	BEGIN TRANSACTION EmployeeInsert
	BEGIN TRY 
		DECLARE @Id int
		IF NOT EXISTS(SELECT [Id] FROM [Employee] WHERE [Email] = @Email)
		BEGIN
			INSERT INTO [Employee] ([FirstName], [MiddleName], [LastName], [Gender], [Email], [PhoneNumber], [DOB], [DateOfJoin], [Education], [CityId], [StateId], [CountryId]) 
			VALUES (@FirstName, @MiddleName, @LastName, @Gender, @Email, @PhoneNumber, @DOB, @DateOfJoin, @Education, @CityId, @StateId, @CountryId)
			SET @Id = SCOPE_IDENTITY();
		END
		ELSE
			SET @Id = 0;
	
		SELECT @Id;
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION EmployeeInsert
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION EmployeeInsert
		
		DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT
		SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(), @ErrorNumber = ERROR_NUMBER()
		RAISERROR  (@ErrorMessage,@ErrorSeverity,@ErrorState,@ErrorNumber)
	END CATCH
END