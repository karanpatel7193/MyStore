CREATE PROCEDURE [dbo].[CategoryProperty_SelectByCategory]  
    @CategoryId INT  
AS  
BEGIN  
    SET NOCOUNT ON;  

    -- Get distinct properties for the selected category  
    SELECT DISTINCT  
        CP.PropertyId,  
        P.[Name] AS PropertyName  
    FROM  
        CategoryProperty CP  
    INNER JOIN  
        Property P ON CP.PropertyId = P.Id  
    WHERE  
        CP.CategoryId = @CategoryId  
    ORDER BY  
        P.[Name];  
END;
