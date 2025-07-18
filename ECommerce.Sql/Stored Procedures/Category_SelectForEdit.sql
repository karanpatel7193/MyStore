CREATE PROCEDURE [dbo].[Category_SelectForEdit]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  Category_SelectForEdit
	 PURPOSE  :  This SP use for fill all LOV in Category page for edit mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        03/10/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Category_SelectForRecord @Id

	EXEC Category_SelectForAdd
END

