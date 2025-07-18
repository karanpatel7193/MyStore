CREATE PROCEDURE [dbo].[Product_SelectForGrid]
    @Name               NVARCHAR(50)    = NULL, 
    @Description        NVARCHAR(500)   = NULL, 
    @CategoryId         INT             = NULL, 
    @SortExpression     VARCHAR(50),
    @SortDirection      VARCHAR(5),
    @PageIndex          INT,
    @PageSize           INT
AS
BEGIN
    DECLARE @SqlQuery NVARCHAR(MAX);

    SET @SqlQuery = N'
    SELECT 
        P.[Id], 
        P.[Name], 
        P.[Description], 
        P.[CategoryId],
        C.[Name] AS CategoryName,
        P.[SKU], 
        P.[UPC], 
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
        (
            SELECT TOP 1 PMI.[ThumbUrl]
            FROM [ProductMedia] PMI
            WHERE PMI.[ProductId] = P.[Id]
        ) AS MediaThumbUrl
    FROM 
        [Product] P WITH (NOLOCK) 
        INNER JOIN [Category] C ON P.[CategoryId] = C.[Id]
        INNER JOIN [ProductInventory] PI ON P.[Id] = PI.[ProductId]
    WHERE 
        (P.[ParentProductId] IS NULL OR P.[ParentProductId] = 0)
        AND P.[Name] LIKE COALESCE(@Name, P.[Name]) + ''%''
        AND (@Description IS NULL OR P.[Description] LIKE @Description + ''%'')
        AND (@CategoryId IS NULL OR P.[CategoryId] = @CategoryId)

    ORDER BY ' + QUOTENAME(@SortExpression) + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY
    ';

    EXEC sp_executesql @SqlQuery, 
        N'@Name NVARCHAR(50), @Description NVARCHAR(500), @CategoryId INT, @PageIndex INT, @PageSize INT, @SortExpression NVARCHAR(50), @SortDirection NVARCHAR(5)', 
        @Name, @Description, @CategoryId, @PageIndex, @PageSize, @SortExpression, @SortDirection;

    SELECT COUNT(1) AS TotalRecords
    FROM [Product] P WITH (NOLOCK)
    WHERE 
        (P.[ParentProductId] IS NULL OR P.[ParentProductId] = 0)
        AND P.[Name] LIKE COALESCE(@Name, P.[Name]) + '%'
        AND (@Description IS NULL OR P.[Description] LIKE @Description + '%')
        AND (@CategoryId IS NULL OR P.[CategoryId] = @CategoryId);
END
