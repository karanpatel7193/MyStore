
CREATE PROCEDURE [dbo].[Employee_SelectForLOV]
AS
/***********************************************************************************************
	 NAME     :  Employee_SelectForLOV
	 PURPOSE  :  This SP select records from table Employee for fill LOV
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT E.[Id]
	FROM [Employee] E
END