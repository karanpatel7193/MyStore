CREATE PROCEDURE [dbo].[Country_SelectForList]
	@Name varchar(50) = NULL, 
	@SortName varchar(10) = NULL, 
	@SortExpression varchar(50),
	@SortDirection varchar(5),
	@PageIndex int,
	@PageSize int
AS
/***********************************************************************************************
	 NAME     :  Country_SelectForList
	 PURPOSE  :  This SP use for fill all LOV and list grid in Country list page
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Country_SelectForGrid @Name, @SortName, @SortExpression, @SortDirection, @PageIndex, @PageSize
END