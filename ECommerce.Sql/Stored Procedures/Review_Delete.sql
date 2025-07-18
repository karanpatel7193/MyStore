
CREATE PROCEDURE [dbo].[Review_Delete]
    @Id INT
AS
BEGIN
    BEGIN TRANSACTION ReviewDelete;
    BEGIN TRY 
        -- Delete Review (Cascade Delete will remove related media)
        DELETE FROM [Review] WHERE [Id] = @Id;

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION ReviewDelete;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION ReviewDelete;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;