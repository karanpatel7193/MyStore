CREATE PROCEDURE [dbo].[PurchaseOrder_SelectForList]
    @ProductId          INT          = NULL, 
    @SortExpression     VARCHAR(50), 
    @SortDirection      VARCHAR(5), 
    @PageIndex          INT, 
    @PageSize           INT,
    @VendorName         NVARCHAR(100) = NULL,-- New parameter for Vendor Name
    @VendorId         INT = NULL

AS
BEGIN
    SET NOCOUNT ON;

    -- Optionally include related LOV procedures for other entities
    EXEC Vendor_SelectForLOV;
    EXEC Product_SelectForLOV;

    -- Main data retrieval for PurchaseOrder
    EXEC PurchaseOrder_SelectForGrid 
        @Description = NULL,
        @SortExpression = @SortExpression, 
        @SortDirection = @SortDirection, 
        @PageIndex = @PageIndex, 
        @PageSize = @PageSize,
        @VendorName = @VendorName,
		@VendorId = @VendorId;
END