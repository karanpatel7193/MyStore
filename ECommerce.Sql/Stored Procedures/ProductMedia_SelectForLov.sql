CREATE PROCEDURE [dbo].[ProductMedia_SelectForLov]
AS
BEGIN

    SELECT 
        [Value],
        [ValueText]
    FROM 
        [dbo].[MasterValue]
    WHERE 
        [MasterId] = 4;
END;

