CREATE PROCEDURE [dbo].[City_SelectForList]
AS
/***********************************************************************************************
	 NAME     :  City_SelectForList
	 PURPOSE  :  This SP use for fill all LOV and list grid in City list page
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Country_SelectForLOV
END