
CREATE PROCEDURE [dbo].[PurchaseInvoice_SelectForList]
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
    @VendorId         INT = NULL, 
    @ProductName      NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Include related LOV procedures for Vendor and Product if necessary
    EXEC Vendor_SelectForLOV;
    EXEC Product_SelectForLOV;

    -- Main data retrieval for PurchaseInvoice
    EXEC PurchaseInvoice_SelectForGrid
        @Description,
        @TotalQuantity,
        @TotalAmount,
        @TotalDiscount,
        @TotalTax,
        @TotalFinalAmount,
        @SortExpression,
        @SortDirection ,
        @PageIndex, 
		@PageSize, 
        @VendorName,
        @ProductName,
		@VendorId;
END