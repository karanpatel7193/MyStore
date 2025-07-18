CREATE PROCEDURE [dbo].[PurchaseInvoice_SelectForEdit]
    @Id INT
AS
BEGIN
    EXEC [dbo].[PurchaseInvoice_SelectForRecord] @Id;

    EXEC [dbo].[PurchaseInvoice_SelectForAdd];
END;
