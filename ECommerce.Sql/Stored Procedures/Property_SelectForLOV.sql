/*
This SP select records from table Property for bind LOV
Created By :: Rekansh Patel
Created On :: 04/23/2017
*/	
CREATE PROCEDURE [dbo].[Property_SelectForLOV]
AS
BEGIN
	SELECT [Id], [Name]
	FROM [Property]  
END

