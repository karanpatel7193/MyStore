CREATE PROCEDURE [dbo].[Category_SelectForRecord]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  Category_SelectForRecord
	 PURPOSE  :  This SP select perticular record from table Category for open page in edit / view mode.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/03/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT R.[Id], R.[Name], R.[ParentId], R.[ImageUrl], R.[Description], R.[IsVisible]
	FROM [Category] R
	WHERE R.[Id] = @Id;
END

