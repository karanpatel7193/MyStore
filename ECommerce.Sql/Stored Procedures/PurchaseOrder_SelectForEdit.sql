CREATE PROCEDURE [dbo].[PurchaseOrder_SelectForEdit]
    @Id INT
AS
BEGIN
    EXEC [dbo].[PurchaseOrder_SelectForRecord] @Id;

    EXEC [dbo].[PurchaseOrder_SelectForAdd];

END