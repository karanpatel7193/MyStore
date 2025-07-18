CREATE PROCEDURE [dbo].[State_SelectForEdit]
	@Id int 
AS
/***********************************************************************************************
	 NAME     :  State_SelectForEdit
	 PURPOSE  :  This SP use for fill all LOV in State page for edit mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC State_SelectForRecord @Id

	EXEC State_SelectForAdd
END