
CREATE PROCEDURE [dbo].[PurchaseOrder_Delete]
    @Id INT
AS
BEGIN
    BEGIN TRANSACTION PurchaseOrderDelete;
    BEGIN TRY
        -- Delete from PurchaseOrderInvoiceItem table
        DELETE FROM [dbo].[PurchaseOrderItem]
        WHERE [PurchaseOrderId] = @Id;

        -- Delete from PurchaseOrder table
        DELETE FROM [dbo].[PurchaseOrder]
        WHERE [Id] = @Id;

        -- Commit transaction if no errors
        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION PurchaseOrderDelete;
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of errors
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION PurchaseOrderDelete;

        -- Capture and raise the error details
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(), 
            @ErrorSeverity = ERROR_SEVERITY(), 
            @ErrorState = ERROR_STATE(), 
            @ErrorNumber = ERROR_NUMBER();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState, @ErrorNumber);
    END CATCH
END;
