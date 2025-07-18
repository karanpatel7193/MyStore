CREATE PROCEDURE [dbo].[Review_Update]
    @ReviewId INT,
    @UserId BIGINT,
    @ProductId INT,
    @Rating DECIMAL(2,1),
    @Comments NVARCHAR(MAX),
    @Date DATETIME = NULL,       -- ✅ Added parameter
    @MediaList XML  
AS
BEGIN
    BEGIN TRANSACTION ReviewUpdate;
    BEGIN TRY 
        -- Update Review table including Date
        UPDATE [Review]
        SET 
            [UserId] = @UserId,
            [ProductId] = @ProductId,
            [Rating] = @Rating,
            [Comments] = @Comments,
            [Date] = ISNULL(@Date, GETDATE())   -- ✅ Set Date
        WHERE [Id] = @ReviewId;

        -- Process the MediaList XML
        IF @MediaList IS NOT NULL
        BEGIN
            DECLARE @DocHandle INT;
            EXEC sp_xml_preparedocument @DocHandle OUTPUT, @MediaList;

            -- Delete existing media for the review
            DELETE FROM [ReviewMedia] WHERE [ReviewId] = @ReviewId;

            -- Insert new media files
            INSERT INTO [ReviewMedia] ([ReviewId], [MediaType], [MediaURL])
            SELECT 
                @ReviewId,
                TMP.[MediaType],
                TMP.[MediaURL]
            FROM OPENXML (@DocHandle, '/ArrayOfMedia/ArrayOfFile', 2)
            WITH ([MediaType] NVARCHAR(250), [MediaURL] NVARCHAR(MAX)) AS TMP;

            EXEC sp_xml_removedocument @DocHandle;
        END;

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION ReviewUpdate;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION ReviewUpdate;

        IF @DocHandle IS NOT NULL
            EXEC sp_xml_removedocument @DocHandle;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;