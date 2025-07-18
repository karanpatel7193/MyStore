CREATE PROCEDURE [dbo].[PurchaseOrder_SelectForRecord]
    @Id INT
AS
BEGIN
    -- Fetch main Purchase Order details
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
    FROM 
        [PurchaseOrder] PO WITH (NOLOCK)
    INNER JOIN [Vendor] V ON PO.[VendorId] = V.[Id]
    WHERE 
        PO.[Id] = @Id;

    -- Fetch associated Purchase Order Items
    SELECT 
        PII.[ProductId],
        PII.[ProductName],
        PII.[Quantity],
        PII.[Price],
        PII.[Amount],
        PII.[DiscountPercentage],
        PII.[DiscountedAmount],
        PII.[Tax],
        PII.[FinalAmount],
        PII.[ExpiryDate]
    FROM 
        [PurchaseOrderItem] PII
    WHERE 
        PII.[PurchaseOrderId] = @Id;
END;
