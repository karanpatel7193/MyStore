
CREATE PROCEDURE [dbo].[Employee_SelectForAdd]
	@StateId int = NULL, 	@CountryId smallint = NULL
AS
/***********************************************************************************************
	 NAME     :  Employee_SelectForAdd
	 PURPOSE  :  This SP use for fill all LOV in Employee page for add mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Country_SelectForLOV 	EXEC State_SelectForLOV @CountryId	EXEC City_SelectForLOV @StateId
END