CREATE PROCEDURE [dbo].[State_SelectForLOV]
	@CountryId smallint
AS
/***********************************************************************************************
	 NAME     :  State_SelectForLOV
	 PURPOSE  :  This SP select records from table State for fill LOV
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT S.[Id], S.[Name]
	FROM [State] S
	WHERE	S.CountryId = @CountryId
END