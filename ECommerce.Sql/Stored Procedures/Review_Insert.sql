CREATE PROCEDURE [dbo].[Review_Insert]
    @UserId BIGINT,
    @ProductId INT,
    @Rating DECIMAL(2,1),
    @Comments NVARCHAR(MAX),
    @Date DATETIME = NULL,  -- New parameter
    @MediaList XML  
AS
BEGIN
    BEGIN TRANSACTION ReviewInsert;
    BEGIN TRY 
        DECLARE @ReviewId INT;

        -- Insert the review into the Review table
        INSERT INTO [Review] ([UserId], [ProductId], [Rating], [Comments], [Date])
        VALUES (@UserId, @ProductId, @Rating, @Comments, ISNULL(@Date, GETDATE()));

        SET @ReviewId = SCOPE_IDENTITY();

        -- Process the MediaList XML
        IF @MediaList IS NOT NULL
        BEGIN
            DECLARE @DocHandle INT;
            EXEC sp_xml_preparedocument @DocHandle OUTPUT, @MediaList;

            -- Insert data from XML into ReviewMedia table
            INSERT INTO [ReviewMedia] ([ReviewId], [MediaType], [MediaURL])
            SELECT 
                @ReviewId,
                TMP.[MediaType],
                TMP.[MediaURL]
            FROM OPENXML (@DocHandle, '/ArrayOfReviewMediaEntity/ReviewMediaEntity', 2)
            WITH ([MediaType] NVARCHAR(250), [MediaURL] NVARCHAR(MAX)) AS TMP;

            EXEC sp_xml_removedocument @DocHandle;
        END;

        -- Return the newly created ReviewId
        SELECT @ReviewId AS ReviewId;

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION ReviewInsert;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION ReviewInsert;

        IF @DocHandle IS NOT NULL
            EXEC sp_xml_removedocument @DocHandle;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;