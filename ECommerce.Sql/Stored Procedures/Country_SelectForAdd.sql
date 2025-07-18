CREATE PROCEDURE [dbo].[Country_SelectForAdd]
AS
/***********************************************************************************************
	 NAME     :  Country_SelectForAdd
	 PURPOSE  :  This SP use for fill all LOV in Country page for add mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT L.Id AS LanguageId, L.EnglishName, L.LocalName
	FROM dbo.[Language] L 
END