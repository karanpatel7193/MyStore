CREATE PROCEDURE [dbo].[Category_SelectForAdd]
AS
/***********************************************************************************************
	 NAME     :  Category_SelectForAdd
	 PURPOSE  :  This SP use for fill all LOV in Category page for add mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        03/10/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Category_SelectForLOV
END

