CREATE PROCEDURE [dbo].[City_SelectForLOV]
	@StateId int
AS
/***********************************************************************************************
	 NAME     :  City_SelectForLOV
	 PURPOSE  :  This SP select records from table city for fill LOV
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT C.[Id], C.[Name]
	FROM [City] C
	WHERE	C.StateId = @StateId
END