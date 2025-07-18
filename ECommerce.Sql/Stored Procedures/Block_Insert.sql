CREATE PROCEDURE [dbo].[Block_Insert]
    @Name NVARCHAR(100),
    @Description NVARCHAR(200) = NULL,
    @IsActive BIT,
    @Content NVARCHAR(MAX),
    @BlockProductsXML XML = NULL 
AS
BEGIN
    BEGIN TRANSACTION BlockInsert
    BEGIN TRY
        DECLARE @BlockId INT;

        INSERT INTO [dbo].[Block] ([Name], [Description], [IsActive], [Content])
        VALUES (@Name, @Description, @IsActive, @Content);

        SET @BlockId = SCOPE_IDENTITY();

        IF @BlockProductsXML IS NOT NULL
        BEGIN
            DECLARE @DocHandle INT;
            EXEC sp_xml_preparedocument @DocHandle OUTPUT, @BlockProductsXML;

            INSERT INTO [dbo].[BlockProduct] ([BlockId], [ProductId])
            SELECT 
                @BlockId, 
                BlockProduct.[ProductId]
            FROM OPENXML (@DocHandle, '/ArrayOfBlockProductEntity/BlockProductEntity', 2)
            WITH (
                [ProductId] INT
            ) AS BlockProduct;

            EXEC sp_xml_removedocument @DocHandle;
        END

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION BlockInsert;

        SELECT @BlockId AS BlockId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION BlockInsert;

        EXEC sp_xml_removedocument @DocHandle;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(), 
               @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
