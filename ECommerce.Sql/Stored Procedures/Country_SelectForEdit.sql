CREATE PROCEDURE [dbo].[Country_SelectForEdit]
	@Id smallint 
AS
/***********************************************************************************************
	 NAME     :  Country_SelectForEdit
	 PURPOSE  :  This SP use for fill all LOV in Country page for edit mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC Country_SelectForRecord @Id

	SELECT CL.CountryId, L.Id AS LanguageId, L.EnglishName, L.LocalName, CONVERT(BIT, CASE WHEN CL.Id IS NULL THEN 0 ELSE 1 END) AS IsSelect
	FROM dbo.CountryLanguage CL 
		RIGHT JOIN dbo.[Language] L ON CL.LanguageId = L.Id AND CL.CountryId = @Id
END