CREATE PROCEDURE [dbo].[Product_SelectForDetails]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Get Product Basic Details
    SELECT 
        P.[Id], 
        P.[Name], 
        P.[Description], 
        P.[SKU], 
        PI.[SellPrice], 
        PI.[DiscountPercentage], 
        PI.[FinalSellPrice], 
        C.[Name] AS CategoryName,
        PM.[Url],
        PM.[ThumbUrl]
    FROM 
        [Product] P WITH (NOLOCK)
        INNER JOIN [Category] C ON P.[CategoryId] = C.[Id]
        INNER JOIN [ProductInventory] PI ON P.Id = PI.ProductId
        LEFT JOIN [ProductMedia] PM ON P.Id = PM.ProductId
    WHERE 
        P.[Id] = @Id;

    -- 2. Get Product Properties
    SELECT
        PRP.[Name] AS PropertyName,
        CP.[Unit],
        PP.[Value]
    FROM 
        ProductProperty PP
        INNER JOIN CategoryProperty CP ON CP.[PropertyId] = PP.[PropertyId]
        INNER JOIN Property PRP ON CP.[PropertyId] = PRP.[Id]
        INNER JOIN Product PR ON PR.[Id] = PP.[ProductId] AND PR.[CategoryId] = CP.[CategoryId]
    WHERE 
        PP.[ProductId] = @Id;

    -- 3. Get Variant Property Values using ParentProductId
    DECLARE @ParentId INT;
    SELECT @ParentId = ParentProductId FROM Product WHERE Id = @Id;

    SELECT 
        PV.[Id],
        PV.[VariantPropertyId],
        PR.[Name] AS VariantPropertyName,
        PV.[VariantPropertyValue],
        PV.[ProductId]
    FROM 
        ProductVariant PV
        INNER JOIN Property PR ON PR.[Id] = PV.[VariantPropertyId]
    WHERE 
        PV.[ProductId] = @ParentId;


    -- 4. Get All Variant Combinations for This Parent Product
    DECLARE @ParentProductId INT;

    SELECT @ParentProductId = ParentProductId 
    FROM Product 
    WHERE Id = @Id;

    SELECT 
        P.Id,
        P.Name,
        P.SKU,
        P.ProductVariantIds,
        PI.FinalSellPrice,
        PM.Url AS ImageUrl
    FROM 
        Product P
        LEFT JOIN ProductInventory PI ON P.Id = PI.ProductId
        LEFT JOIN ProductMedia PM ON P.Id = PM.ProductId
    WHERE 
        P.ParentProductId = @ParentProductId;
END;
