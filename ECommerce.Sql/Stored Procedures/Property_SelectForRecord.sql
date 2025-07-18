CREATE PROCEDURE [dbo].[Property_SelectForRecord]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  Property_SelectForRecord
	 PURPOSE  :  This SP select perticular record from table Property for open page in edit / view mode.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/03/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT R.[Id], R.[Name], R.[Description]
	FROM [Property] R
	WHERE R.[Id] = @Id;
END

