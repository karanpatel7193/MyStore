CREATE PROCEDURE [dbo].[Block_Update]
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(200) = NULL,
    @IsActive BIT,
    @Content NVARCHAR(MAX),
    @BlockProductsXML XML = NULL 
AS
BEGIN
    BEGIN TRANSACTION BlockUpdate;
    BEGIN TRY
        UPDATE [dbo].[Block]
        SET 
            [Name] = @Name,
            [Description] = @Description,
            [IsActive] = @IsActive,
            [Content] = @Content
        WHERE 
            [Id] = @Id;

        IF @BlockProductsXML IS NOT NULL
        BEGIN
            DECLARE @DocHandle INT;
            EXEC sp_xml_preparedocument @DocHandle OUTPUT, @BlockProductsXML;

            DELETE FROM [dbo].[BlockProduct]
            WHERE [BlockId] = @Id
              AND (
                  @BlockProductsXML IS NULL
                  OR [Id] NOT IN (
                      SELECT [Id] 
                      FROM OPENXML(@DocHandle, '/ArrayOfBlockProductEntity/BlockProductEntity', 2) 
                      WITH ([Id] INT)
                  )
              );

            INSERT INTO [dbo].[BlockProduct] ([BlockId], [ProductId])
            SELECT 
                @Id, 
                [ProductId]
            FROM OPENXML(@DocHandle, '/ArrayOfBlockProductEntity/BlockProductEntity', 2)
                WITH ([Id] INT, [ProductId] INT)
            WHERE [Id] = 0;  -- This indicates a new entry to be inserted

            EXEC sp_xml_removedocument @DocHandle;
        END
        ELSE
        BEGIN
            -- Delete all BlockProducts if no XML is provided
            DELETE FROM [dbo].[BlockProduct]
            WHERE [BlockId] = @Id;
        END

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION BlockUpdate;

        SELECT @Id AS BlockId;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION BlockUpdate;

        EXEC sp_xml_removedocument @DocHandle;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(), 
               @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
