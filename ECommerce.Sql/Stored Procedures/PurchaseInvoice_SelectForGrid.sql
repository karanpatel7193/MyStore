
CREATE PROCEDURE [dbo].[PurchaseInvoice_SelectForGrid]
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
    @ProductName      NVARCHAR(100) = NULL,
    @VendorId         INT = NULL  
AS
BEGIN
    DECLARE @SqlQuery NVARCHAR(MAX)

    SET @SqlQuery = N'
    SELECT DISTINCT
        PI.[Id], 
        PI.[InvoiceNumber], 
        PI.[CreatedBy], 
        PI.[CreatedOn], 
        PI.[LastUpdatedBy], 
        PI.[LastUpdatedOn], 
        PI.[VendorId], 
        V.[Name] AS VendorName,
        PI.[Description], 
        PI.[TotalQuantity], 
        PI.[TotalAmount], 
        PI.[TotalDiscount], 
        PI.[TotalTax], 
        PI.[TotalFinalAmount],
        P.[Name] AS ProductName
    FROM [PurchaseInvoice] PI WITH (NOLOCK)
    LEFT JOIN [Vendor] V ON PI.[VendorId] = V.[Id]
    LEFT JOIN [PurchaseInvoiceItem] PII ON PI.[Id] = PII.[PurchaseInvoiceId]
    LEFT JOIN [Product] P ON PII.[ProductId] = P.[Id]
    WHERE   PI.[Description] LIKE COALESCE(@Description, PI.[Description]) + ''%''
        AND (@TotalQuantity IS NULL OR PI.[TotalQuantity] = @TotalQuantity)
        AND (@TotalAmount IS NULL OR PI.[TotalAmount] = @TotalAmount)
        AND (@TotalDiscount IS NULL OR PI.[TotalDiscount] = @TotalDiscount)
        AND (@TotalTax IS NULL OR PI.[TotalTax] = @TotalTax)
        AND (@TotalFinalAmount IS NULL OR PI.[TotalFinalAmount] = @TotalFinalAmount)
        AND (@VendorName IS NULL OR V.[Name] LIKE @VendorName + ''%'')
        AND (@ProductName IS NULL OR P.[Name] LIKE @ProductName + ''%'')
        AND (@VendorId IS NULL OR PI.[VendorId] = @VendorId) -- Added VendorId filter
    ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY
    '

    EXEC sp_executesql @SqlQuery, 
        N'@Description NVARCHAR(100), @TotalQuantity DECIMAL(18,2), 
          @TotalAmount DECIMAL(18,2), @TotalDiscount DECIMAL(18,2), @TotalTax DECIMAL(18,2), 
          @TotalFinalAmount DECIMAL(18,2), @PageIndex INT, @PageSize INT, @SortExpression NVARCHAR(50), 
          @SortDirection NVARCHAR(5), @VendorName NVARCHAR(100), @ProductName NVARCHAR(100), 
          @VendorId INT',
        @Description, @TotalQuantity, @TotalAmount, @TotalDiscount, 
        @TotalTax, @TotalFinalAmount, @PageIndex, @PageSize, @SortExpression, 
        @SortDirection, @VendorName, @ProductName, @VendorId

    -- Fetch total records
    SELECT COUNT(1) AS TotalRecords
    FROM [PurchaseInvoice] PI WITH (NOLOCK)
    LEFT JOIN [Vendor] V ON PI.[VendorId] = V.[Id]
    LEFT JOIN [PurchaseInvoiceItem] PII ON PI.[Id] = PII.[PurchaseInvoiceId]
    LEFT JOIN [Product] P ON PII.[ProductId] = P.[Id]
    WHERE   PI.[Description] LIKE COALESCE(@Description, PI.[Description]) + '%'
        AND (@TotalQuantity IS NULL OR PI.[TotalQuantity] = @TotalQuantity)
        AND (@TotalAmount IS NULL OR PI.[TotalAmount] = @TotalAmount)
        AND (@TotalDiscount IS NULL OR PI.[TotalDiscount] = @TotalDiscount)
        AND (@TotalTax IS NULL OR PI.[TotalTax] = @TotalTax)
        AND (@TotalFinalAmount IS NULL OR PI.[TotalFinalAmount] = @TotalFinalAmount)
        AND (@VendorName IS NULL OR V.[Name] LIKE @VendorName + '%')
        AND (@ProductName IS NULL OR P.[Name] LIKE @ProductName + '%')
        AND (@VendorId IS NULL OR PI.[VendorId] = @VendorId) -- Added VendorId filter for total count
END