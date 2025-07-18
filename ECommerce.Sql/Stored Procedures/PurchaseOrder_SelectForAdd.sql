CREATE PROCEDURE [dbo].[PurchaseOrder_SelectForAdd]
AS

BEGIN
	EXEC Vendor_SelectForLOV

	EXEC Product_SelectForLOV
END