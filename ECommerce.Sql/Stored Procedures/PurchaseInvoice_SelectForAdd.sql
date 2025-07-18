CREATE PROCEDURE [dbo].[PurchaseInvoice_SelectForAdd]
AS

BEGIN
	EXEC Vendor_SelectForLOV

	EXEC Product_SelectForLOV
END