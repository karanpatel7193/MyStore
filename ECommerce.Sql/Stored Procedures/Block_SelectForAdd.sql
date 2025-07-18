CREATE PROCEDURE [dbo].[Block_SelectForAdd]
AS
/***********************************************************************************************
	 NAME     :  Block_SelectForAdd
	 PURPOSE  :  This SP use for fill all LOV in Block page for add mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        03/10/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Product_SelectForLOV
END

