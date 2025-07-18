CREATE PROCEDURE [dbo].[City_SelectForRecord]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  City_SelectForRecord
	 PURPOSE  :  This SP select perticular record from table City for open page in edit / view mode.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT C.[Id], C.[Name], C.[StateId], S.CountryId
	FROM [City] C
		INNER JOIN [State] S ON C.StateId = S.Id
	WHERE C.[Id] = @Id;
END