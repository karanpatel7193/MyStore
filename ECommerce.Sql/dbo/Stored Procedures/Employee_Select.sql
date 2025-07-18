
CREATE PROCEDURE [dbo].[Employee_Select]
	@Id int = NULL, @FirstName nvarchar(50) = NULL, @MiddleName nvarchar(50) = NULL, @LastName nvarchar(50) = NULL, @Gender nvarchar(10) = NULL, @Email nvarchar(100) = NULL, @PhoneNumber nvarchar(20) = NULL, @DOB date = NULL, @DateOfJoin date = NULL, @Education nvarchar(50) = NULL, @CityId int = NULL, @StateId int = NULL, @CountryId smallint = NULL 
AS
/***********************************************************************************************
	 NAME     :  Employee_Select
	 PURPOSE  :  This SP select records from table Employee
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT E.[Id], E.[FirstName], E.[MiddleName], E.[LastName], E.[Gender], E.[Email], E.[PhoneNumber], E.[DOB], E.[DateOfJoin], E.[Education], E.[CityId], E.[StateId], E.[CountryId] 
	FROM [Employee] E
	WHERE E.[Id] = COALESCE(@Id, E.[Id]) AND E.[FirstName] = COALESCE(@FirstName, E.[FirstName]) AND E.[MiddleName] = COALESCE(@MiddleName, E.[MiddleName]) AND E.[LastName] = COALESCE(@LastName, E.[LastName]) AND E.[Gender] = COALESCE(@Gender, E.[Gender]) AND E.[Email] = COALESCE(@Email, E.[Email]) AND E.[PhoneNumber] = COALESCE(@PhoneNumber, E.[PhoneNumber]) AND E.[DOB] = COALESCE(@DOB, E.[DOB]) AND E.[DateOfJoin] = COALESCE(@DateOfJoin, E.[DateOfJoin]) AND E.[Education] = COALESCE(@Education, E.[Education]) AND E.[CityId] = COALESCE(@CityId, E.[CityId]) AND E.[StateId] = COALESCE(@StateId, E.[StateId]) AND E.[CountryId] = COALESCE(@CountryId, E.[CountryId]) 
END