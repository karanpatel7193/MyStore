
CREATE PROCEDURE [dbo].[Employee_SelectForEdit]
	@Id int,
	@StateId int = NULL, 	@CountryId smallint = NULL
AS
/***********************************************************************************************
	 NAME     :  Employee_SelectForEdit
	 PURPOSE  :  This SP use for fill all LOV in Employee page for edit mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Employee_SelectForRecord @Id

	EXEC Employee_SelectForAdd @StateId, @CountryId
END