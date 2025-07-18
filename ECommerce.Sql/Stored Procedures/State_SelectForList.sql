CREATE PROCEDURE [dbo].[State_SelectForList]
AS
/***********************************************************************************************
	 NAME     :  State_SelectForList
	 PURPOSE  :  This SP use for fill all LOV and list grid in State list page
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Country_SelectForLOV
END