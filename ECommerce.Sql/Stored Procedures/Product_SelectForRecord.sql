CREATE PROCEDURE [dbo].[Product_SelectForRecord]
    @Id INT
AS
BEGIN
    SELECT 
        P.[Id], 
        P.[Name], 
        P.[Description], 
        P.[LongDescription], 
        P.[CategoryId],
        P.[IsExpiry],
        P.CreatedBy,
        P.LastUpdatedBy,
        P.[SKU],
        P.[UPC],
        P.[ParentProductId],
        C.[Name] AS CategoryName,
        PI.[OpeningQty],
        PI.[BuyQty],
        PI.[LockQty],
        PI.[OrderQty],
        PI.[SellQty],
        PI.[ClosingQty],
        PI.[CostPrice],
        PI.[SellPrice],
        PI.[DiscountPercentage],
        PI.[DiscountAmount],
        PI.[FinalSellPrice]
    FROM 
        [Product] P WITH (NOLOCK) 
        INNER JOIN [Category] C ON P.[CategoryId] = C.[Id]
        LEFT JOIN [ProductInventory] PI ON P.[Id] = PI.[ProductId]
    WHERE 
        P.[Id] = @Id;
END
