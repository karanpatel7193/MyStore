
CREATE PROCEDURE [dbo].[Employee_SelectForList]
	@StateId int = NULL, 	@CountryId smallint = NULL,
	@SortExpression varchar(50),
	@SortDirection varchar(5),
	@PageIndex int,
	@PageSize int
AS
/***********************************************************************************************
	 NAME     :  Employee_SelectForList
	 PURPOSE  :  This SP use for fill all LOV and list grid in Employee list page
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Country_SelectForLOV 	EXEC State_SelectForLOV  @CountryId	EXEC City_SelectForLOV @StateId

	EXEC Employee_SelectForGrid @StateId, @CountryId, @SortExpression, @SortDirection, @PageIndex, @PageSize
END