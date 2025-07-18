CREATE PROCEDURE [dbo].[PurchaseOrder_SelectForLOV]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PO.[Id], 
        CAST(PO.[OrderNumber] AS NVARCHAR) + ' - ' + ISNULL(V.[Name], 'Unknown Vendor') AS [Name]
    FROM 
        [dbo].[PurchaseOrder] PO WITH (NOLOCK)
    LEFT JOIN 
        [dbo].[Vendor] V WITH (NOLOCK) ON PO.[VendorId] = V.[Id];
END
