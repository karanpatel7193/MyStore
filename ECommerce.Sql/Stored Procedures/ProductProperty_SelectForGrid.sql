CREATE PROCEDURE [dbo].[ProductProperty_SelectForGrid]
    @ProductId		INT, 
    @CategoryId		INT

AS
BEGIN
   SELECT			PP.[Id],	
					P.[Name] AS PropertyName,   
					CP.[PropertyId],    
					CP.[Unit],  
					PP.[Value] 
   FROM				CategoryProperty CP

    LEFT  JOIN		ProductProperty PP
			ON		CP.[PropertyId] =   PP.[PropertyId]
			AND		PP.[ProductId]  =   @ProductId
    INNER JOIN		Property p
			ON		CP.[PropertyId] = P.Id

			WHERE	CP.[CategoryId] = @CategoryId

	SELECT DISTINCT VariantPropertyId 
    FROM			ProductVariant 
    WHERE			ProductId = @ProductId
END;
