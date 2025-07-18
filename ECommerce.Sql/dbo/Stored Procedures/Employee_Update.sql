
CREATE PROCEDURE [dbo].[Employee_Update]
	@Id int,	@FirstName nvarchar(50), 	@MiddleName nvarchar(50) = NULL, 	@LastName nvarchar(50), 	@Gender nvarchar(10), 	@Email nvarchar(100), 	@PhoneNumber nvarchar(20) = NULL, 	@DOB date, 	@DateOfJoin date, 	@Education nvarchar(50) = NULL, 	@CityId int = NULL, 	@StateId int = NULL, 	@CountryId smallint = NULL
AS
/***********************************************************************************************
	 NAME     :  Employee_Update
	 PURPOSE  :  This SP update record in table Employee.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	BEGIN TRANSACTION EmployeeUpdate
	BEGIN TRY 
		IF NOT EXISTS(SELECT [Id] FROM [Employee] WHERE [Email] = @Email AND [Id] != @Id)
		BEGIN
			UPDATE [Employee] 
			SET [FirstName] = @FirstName, 				[MiddleName] = @MiddleName, 				[LastName] = @LastName, 				[Gender] = @Gender, 				[Email] = @Email, 				[PhoneNumber] = @PhoneNumber, 				[DOB] = @DOB, 				[DateOfJoin] = @DateOfJoin, 				[Education] = @Education, 				[CityId] = @CityId, 				[StateId] = @StateId, 				[CountryId] = @CountryId
			WHERE [Id] = @Id;
		END
		ELSE
			SET @Id = 0;
	
		SELECT @Id;
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION EmployeeUpdate
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION EmployeeUpdate
		
		DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT
		SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(), @ErrorNumber = ERROR_NUMBER()
		RAISERROR  (@ErrorMessage,@ErrorSeverity,@ErrorState,@ErrorNumber)
	END CATCH
END