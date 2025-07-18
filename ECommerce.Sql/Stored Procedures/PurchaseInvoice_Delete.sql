CREATE PROCEDURE [dbo].[PurchaseInvoice_Delete]
    @Id INT
AS
BEGIN
    BEGIN TRANSACTION PurchaseInvoiceDelete;
    BEGIN TRY
        -- Delete from PurchaseInvoiceItem table
        DELETE FROM [dbo].[PurchaseInvoiceItem]
        WHERE [PurchaseInvoiceId] = @Id;

        -- Delete from PurchaseInvoice table
        DELETE FROM [dbo].[PurchaseInvoice]
        WHERE [Id] = @Id;

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION PurchaseInvoiceDelete;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION PurchaseInvoiceDelete;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;