CREATE PROCEDURE [dbo].[Country_SelectForLOV]
	@API_URL varchar(250) = ''
AS
/***********************************************************************************************
	 NAME     :  Country_SelectForLOV
	 PURPOSE  :  This SP select records from table Country for fill LOV
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT C.[Id], C.[Name], REPLACE(C.[FlagImagePath], '##API_URL##', @API_URL) AS FlagImagePath
	FROM [Country] C
END