CREATE PROCEDURE [dbo].[City_SelectForEdit]
	@Id int 
AS
/***********************************************************************************************
	 NAME     :  City_SelectForEdit
	 PURPOSE  :  This SP use for fill all LOV in City page for edit mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	EXEC City_SelectForRecord @Id

	EXEC Country_SelectForLOV

	DECLARE @CountryId smallint
	SELECT	@CountryId = S.CountryId
	FROM	dbo.[State] S 
	WHERE	S.Id = (
		SELECT	C.StateId
		FROM	dbo.City C
		WHERE	C.Id = @Id
	)
	EXEC State_SelectForLOV @CountryId
END
