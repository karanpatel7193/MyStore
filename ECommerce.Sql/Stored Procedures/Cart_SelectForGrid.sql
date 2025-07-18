CREATE PROCEDURE [dbo].[Cart_SelectForGrid]
    @UserId BIGINT ,
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
        C.[Id], 
        C.[UserId], 
        P.[Name] AS ProductName, 
        P.[SKU], 
        P.[ID] AS ProductId,
        PI.[DiscountAmount],
        C.[Quantity], 
        C.[Price], 
        C.[OfferPrice],
        (
            SELECT TOP 1 PM.[ThumbUrl] 
            FROM [ProductMedia] PM 
            WHERE PM.[ProductId] = P.[Id] 
            ORDER BY PM.[Id] ASC
        ) AS MediaThumbUrl
    FROM [dbo].[Cart] C WITH (NOLOCK)
    INNER JOIN [dbo].[Product]          P   ON  C.[ProductId]   = P.[Id]
	INNER JOIN [dbo].[ProductInventory]	PI  ON	C.[ProductId]	=	PI.[ProductId]
    WHERE
        C.[UserId] = COALESCE(@UserId, C.[UserId])
        AND P.[Name] LIKE COALESCE(@ProductName, P.[Name]) + ''%''
    ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY
    ';

    EXEC sp_executesql @SqlQuery, 
        N'@UserId BIGINT, @ProductName NVARCHAR(50), @PageIndex INT, @PageSize INT', 
        @UserId, @ProductName, @PageIndex, @PageSize;

    -- Total count for pagination
    SELECT COUNT(1) AS TotalRecords
    FROM [dbo].[Cart] C WITH (NOLOCK)
    INNER JOIN [dbo].[Product] P ON C.[ProductId] = P.[Id]
    WHERE 
        C.[UserId] = COALESCE(@UserId, C.[UserId])
        AND P.[Name] LIKE COALESCE(@ProductName, P.[Name]) + '%';
END;
