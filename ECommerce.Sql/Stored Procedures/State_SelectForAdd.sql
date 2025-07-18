CREATE PROCEDURE [dbo].[State_SelectForAdd]
AS
/***********************************************************************************************
	 NAME     :  State_SelectForAdd
	 PURPOSE  :  This SP use for fill all LOV in State page for add mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Country_SelectForLOV 
END