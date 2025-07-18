CREATE PROCEDURE [dbo].[State_Delete]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  State_Delete
	 PURPOSE  :  This SP delete record from table State
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
		DELETE FROM [State] 
		WHERE [Id] = @Id;
END