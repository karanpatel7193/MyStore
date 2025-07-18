CREATE PROCEDURE [dbo].[Wishlist_SelectForGrid]
    @UserId BIGINT,
    @ProductName NVARCHAR(50) = NULL,
    @SortExpression VARCHAR(50),
    @SortDirection VARCHAR(5),
    @PageIndex INT,
    @PageSize INT
AS
BEGIN
    DECLARE @SqlQuery NVARCHAR(MAX);

    SET @SqlQuery = N'
    SELECT 
        P.[Id], 
        P.[Name], 
        P.[CategoryId], 
        C.[Name] AS CategoryName,
        P.[AllowReturn], 
        P.[ReturnPolicy], 
        P.[IsExpiry], 
        P.[CreatedOn], 
        PI.[OpeningQty],
        PI.[BuyQty],
        PI.[LockQty],
        PI.[OrderQty],
        PI.[CostPrice],
        PI.[SellPrice],
        PI.[DiscountPercentage],
        PI.[DiscountAmount],
        PI.[FinalSellPrice],
        (
            SELECT TOP 1 PMI.[ThumbUrl]
            FROM [ProductMedia] PMI
            WHERE PMI.[ProductId] = P.[Id]
            ORDER BY PMI.[Id] ASC
        ) AS MediaThumbUrl
    FROM 
        [dbo].[Wishlist] W WITH (NOLOCK)
        INNER JOIN [dbo].[Product] P ON W.[ProductId] = P.[Id]
        INNER JOIN [dbo].[Category] C ON P.[CategoryId] = C.[Id]
        INNER JOIN [dbo].[ProductInventory] PI ON P.[Id] = PI.[ProductId]
    WHERE   
        W.[UserId] = COALESCE(@UserId, W.[UserId])
        AND P.[Name] LIKE COALESCE(@ProductName, P.[Name]) + ''%''
    
    ORDER BY ' + QUOTENAME(@SortExpression) + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY
    ';

    EXEC sp_executesql @SqlQuery, 
        N'@UserId BIGINT, @ProductName NVARCHAR(50), @PageIndex INT, @PageSize INT', 
        @UserId, @ProductName, @PageIndex, @PageSize;

    -- Total count for pagination
    SELECT COUNT(1) AS TotalRecords
    FROM [dbo].[Wishlist] W WITH (NOLOCK)
    INNER JOIN [dbo].[Product] P ON W.[ProductId] = P.[Id]
    WHERE   
        W.[UserId] = COALESCE(@UserId, W.[UserId])
        AND P.[Name] LIKE COALESCE(@ProductName, P.[Name]) + '%';
END;
