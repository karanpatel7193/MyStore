CREATE PROCEDURE [dbo].[Block_Delete]
    @Id INT
AS
BEGIN
    BEGIN TRANSACTION BlockDelete;
    BEGIN TRY
        DELETE FROM [dbo].[BlockProduct]
        WHERE [BlockId] = @Id;

        DELETE FROM [dbo].[Block]
        WHERE [Id] = @Id;

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION BlockDelete;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION BlockDelete;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(), 
            @ErrorSeverity = ERROR_SEVERITY(), 
            @ErrorState = ERROR_STATE(), 
            @ErrorNumber = ERROR_NUMBER();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState, @ErrorNumber);
    END CATCH
END;
