CREATE PROCEDURE [dbo].[City_Delete]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  City_Delete
	 PURPOSE  :  This SP delete record from table City
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
		DELETE FROM [City] 
		WHERE [Id] = @Id;
END