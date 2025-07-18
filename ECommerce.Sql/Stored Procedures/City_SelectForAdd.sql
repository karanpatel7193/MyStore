CREATE PROCEDURE [dbo].[City_SelectForAdd]
AS
/***********************************************************************************************
	 NAME     :  City_SelectForAdd
	 PURPOSE  :  This SP use for fill all LOV in City page for add mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Country_SelectForLOV
END