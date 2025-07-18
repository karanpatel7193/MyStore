CREATE PROCEDURE [dbo].[Employee_SelectForRecord]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  Employee_SelectForRecord
	 PURPOSE  :  This SP select perticular record from table Employee for open page in edit / view mode.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT E.[Id], E.[FirstName], E.[MiddleName], E.[LastName], E.[Gender], E.[Email], E.[PhoneNumber], E.[DOB], E.[DateOfJoin], E.[Education], E.[CityId], E.[StateId], E.[CountryId]
	FROM [Employee] E
	WHERE E.[Id] = @Id;
END