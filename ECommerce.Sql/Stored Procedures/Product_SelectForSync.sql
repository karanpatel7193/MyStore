CREATE PROCEDURE [dbo].[Product_SelectForSync]
    @Id INT
AS
BEGIN
    SELECT 
        P.[Id], 
        P.[Name], 
        P.[Description], 
        P.[LongDescription], 
        P.[CategoryId],
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
        PI.[FinalSellPrice],
        PM.[Url],           
        PM.[ThumbUrl]
    FROM 
        [Product] P WITH (NOLOCK) 
        INNER   JOIN    [Category]          C   ON P.[CategoryId]       = C.[Id]
        LEFT    JOIN    [ProductInventory]  PI  ON P.[Id]               = PI.[ProductId]
        LEFT    JOIN    [ProductMedia]      PM  ON P.[Id]               = PM.[ProductId] 
    WHERE 
        P.[Id] = @Id;
END
