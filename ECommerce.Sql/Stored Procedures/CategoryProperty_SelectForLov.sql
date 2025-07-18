CREATE PROCEDURE [dbo].[CategoryProperty_SelectForLov]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id], 
        CONVERT(VARCHAR, [CategoryId]) + ' ' + CONVERT(VARCHAR, [PropertyId]) as [Name]
    FROM 
        [dbo].[CategoryProperty]
END