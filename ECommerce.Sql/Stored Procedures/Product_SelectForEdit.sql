CREATE PROCEDURE [dbo].[Product_SelectForEdit]
    @Id INT
AS
BEGIN
    EXEC [dbo].[Product_SelectForRecord] @Id;

    SELECT  PM.Id, PM.[Type], PM.[Url], PM.[ThumbUrl], PM.[Description]
    FROM    [dbo].[ProductMedia] PM
    WHERE   PM.ProductId = @Id;

    EXEC [dbo].[Product_SelectForAdd];

    EXEC [dbo].[ProductVariant_SelectForGrid] @Id;

    SELECT 
        P.[Id],
        P.[Name],
        P.[SKU],
        P.[UPC],
        P.[ProductVariantIds],
        PI.[FinalSellPrice]
    FROM 
        [Product] P
        LEFT JOIN [ProductInventory] PI ON P.Id = PI.ProductId
    WHERE 
        P.[ParentProductId] = @Id;
    
END
