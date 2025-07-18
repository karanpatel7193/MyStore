CREATE PROCEDURE [dbo].[SearchProduct_SelectForCriteriea]
    @CategoryId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
		CPV.PropertyId,
        P.[Name] AS PropertyName 
    FROM 
        CategoryPropertyValue CPV
    INNER JOIN 
        Property P ON CPV.PropertyId = P.Id
    WHERE 
        CPV.CategoryId = @CategoryId
    ORDER BY 
        P.[Name]

    SELECT  
		CPV.PropertyId,
        CPV.[Value], 
        CPV.Unit
    FROM 
        CategoryPropertyValue CPV
    WHERE 
        CPV.CategoryId = @CategoryId
    ORDER BY 
        CPV.Value;
END;