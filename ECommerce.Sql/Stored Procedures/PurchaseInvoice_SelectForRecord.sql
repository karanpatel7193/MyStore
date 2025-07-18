CREATE PROCEDURE [dbo].[PurchaseInvoice_SelectForRecord]
    @Id INT
AS
BEGIN
    -- Select PurchaseInvoice data and its associated items as JSON
    SELECT 
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
        PI.[TotalFinalAmount]
    FROM 
        [PurchaseInvoice] PI WITH (NOLOCK)
    INNER JOIN [Vendor] V ON PI.[VendorId] = V.[Id]
    WHERE 
        PI.[Id] = @Id;

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
    FROM [PurchaseInvoiceItem] PII
    WHERE PII.[PurchaseInvoiceId] =  @Id

END;