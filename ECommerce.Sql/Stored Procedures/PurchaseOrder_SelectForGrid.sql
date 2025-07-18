CREATE PROCEDURE [dbo].[PurchaseOrder_SelectForGrid]
    @Description      NVARCHAR(100)   = NULL, 
    @TotalQuantity    DECIMAL(18, 2) = NULL,
    @TotalAmount      DECIMAL(18, 2) = NULL,
    @TotalDiscount    DECIMAL(18, 2) = NULL,
    @TotalTax         DECIMAL(18, 2) = NULL,
    @TotalFinalAmount DECIMAL(18, 2) = NULL,
    @SortExpression   VARCHAR(50),
    @SortDirection    VARCHAR(5),
    @PageIndex        INT,
    @PageSize         INT,
    @VendorName       NVARCHAR(100) = NULL, 
    @VendorId         INT = NULL  -- Added VendorId parameter
AS
BEGIN
    DECLARE @SqlQuery NVARCHAR(MAX)

    SET @SqlQuery = N'
    SELECT 
        PO.[Id], 
        PO.[OrderNumber], 
        PO.[CreatedBy], 
        PO.[CreatedOn], 
        PO.[LastUpdatedBy], 
        PO.[LastUpdatedOn], 
        PO.[VendorId], 
        V.[Name] AS VendorName,
        PO.[Description], 
        PO.[TotalQuantity], 
        PO.[TotalAmount], 
        PO.[TotalDiscount], 
        PO.[TotalTax], 
        PO.[TotalFinalAmount]
    FROM [PurchaseOrder] PO WITH (NOLOCK)
    LEFT JOIN [Vendor] V ON PO.[VendorId] = V.[Id]
    WHERE   PO.[Description] LIKE COALESCE(@Description, PO.[Description]) + ''%''
        AND (@TotalQuantity IS NULL OR PO.[TotalQuantity] = @TotalQuantity)
        AND (@TotalAmount IS NULL OR PO.[TotalAmount] = @TotalAmount)
        AND (@TotalDiscount IS NULL OR PO.[TotalDiscount] = @TotalDiscount)
        AND (@TotalTax IS NULL OR PO.[TotalTax] = @TotalTax)
        AND (@TotalFinalAmount IS NULL OR PO.[TotalFinalAmount] = @TotalFinalAmount)
        AND (@VendorName IS NULL OR V.[Name] LIKE @VendorName + ''%'')
        AND (@VendorId IS NULL OR PO.[VendorId] = @VendorId) -- Added VendorId filter
    ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY
    '

    EXEC sp_executesql @SqlQuery, 
        N'@Description NVARCHAR(100), @TotalQuantity DECIMAL(18,2), 
          @TotalAmount DECIMAL(18,2), @TotalDiscount DECIMAL(18,2), @TotalTax DECIMAL(18,2), 
          @TotalFinalAmount DECIMAL(18,2), @PageIndex INT, @PageSize INT, @SortExpression NVARCHAR(50), 
          @SortDirection NVARCHAR(5), @VendorName NVARCHAR(100), @VendorId INT',
        @Description, @TotalQuantity, @TotalAmount, @TotalDiscount, 
        @TotalTax, @TotalFinalAmount, @PageIndex, @PageSize, @SortExpression, 
        @SortDirection, @VendorName, @VendorId

    -- Fetch total records
    SELECT COUNT(1) AS TotalRecords
    FROM [PurchaseOrder] PO WITH (NOLOCK)
    LEFT JOIN [Vendor] V ON PO.[VendorId] = V.[Id]
    WHERE   PO.[Description] LIKE COALESCE(@Description, PO.[Description]) + '%'
        AND (@TotalQuantity IS NULL OR PO.[TotalQuantity] = @TotalQuantity)
        AND (@TotalAmount IS NULL OR PO.[TotalAmount] = @TotalAmount)
        AND (@TotalDiscount IS NULL OR PO.[TotalDiscount] = @TotalDiscount)
        AND (@TotalTax IS NULL OR PO.[TotalTax] = @TotalTax)
        AND (@TotalFinalAmount IS NULL OR PO.[TotalFinalAmount] = @TotalFinalAmount)
        AND (@VendorName IS NULL OR V.[Name] LIKE @VendorName + '%')
        AND (@VendorId IS NULL OR PO.[VendorId] = @VendorId) -- Added VendorId filter for total count
END