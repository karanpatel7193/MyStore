CREATE PROCEDURE [dbo].[State_SelectForRecord]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  State_SelectForRecord
	 PURPOSE  :  This SP select perticular record from table State for open page in edit / view mode.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT S.[Id], S.[Name], S.[SortName], S.[CountryId]
	FROM [State] S
	WHERE S.[Id] = @Id;
END