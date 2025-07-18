
CREATE PROCEDURE [dbo].[Employee_Delete]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  Employee_Delete
	 PURPOSE  :  This SP delete record from table Employee
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	BEGIN TRANSACTION EmployeeDelete
	BEGIN TRY 
		DELETE FROM [Employee] 
		WHERE [Id] = @Id;
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION EmployeeDelete
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION EmployeeDelete
		
		DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT
		SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(), @ErrorNumber = ERROR_NUMBER()
		RAISERROR  (@ErrorMessage,@ErrorSeverity,@ErrorState,@ErrorNumber)
	END CATCH
END