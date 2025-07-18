CREATE PROCEDURE [dbo].[Product_SelectForLOV]
AS
BEGIN
    SELECT 
        P.[Id], 
        P.[Name]
    FROM 
        [Product] P WITH (NOLOCK)
END
