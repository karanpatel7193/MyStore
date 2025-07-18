CREATE PROCEDURE [dbo].[Cart_InsertBulk]
    @UserId BIGINT,
    @CartXML XML
AS
BEGIN
    BEGIN TRANSACTION CartInsertBulk;
    BEGIN TRY
        DECLARE @DocHandle INT;
        EXEC sp_xml_preparedocument @DocHandle OUTPUT, @CartXML;

        -- Temporary table to hold XML data
        DECLARE @TempCart TABLE (
            ProductId INT,
            Quantity INT,
            Price DECIMAL(18,2),
            OfferPrice DECIMAL(18,2),
            IsActive BIT,
            AddedDate DATE
        );

        -- Extract XML data into the temp table
        INSERT INTO @TempCart (ProductId, Quantity, Price, OfferPrice, IsActive, AddedDate)
        SELECT 
            ProductId, Quantity, Price, OfferPrice, IsActive, AddedDate
        FROM OPENXML(@DocHandle, 'ArrayOfCartEntity/CartEntity', 2)
        WITH (
            ProductId INT, 
            Quantity INT, 
            Price DECIMAL(18,2), 
            OfferPrice DECIMAL(18,2), 
            IsActive BIT, 
            AddedDate DATE
        );

        -- Remove XML document reference
        EXEC sp_xml_removedocument @DocHandle;

        -- Ensure price and offer price are set correctly if NULL
        UPDATE T
        SET 
            T.Price = P.SellPrice,
            T.OfferPrice = P.FinalSellPrice
        FROM @TempCart T
        INNER JOIN [dbo].[ProductInventory] P ON T.ProductId = P.ProductId
        WHERE T.Price IS NULL OR T.OfferPrice IS NULL;

        -- Update existing cart items (increase quantity)
        UPDATE C
        SET 
            C.Quantity = C.Quantity + T.Quantity,
            C.Price = T.Price,
            C.OfferPrice = T.OfferPrice,
            C.IsActive = T.IsActive,
            C.AddedDate = T.AddedDate
        FROM [dbo].[Cart] C
        INNER JOIN @TempCart t ON C.ProductId = T.ProductId AND C.UserId = @UserId;

        -- Insert new cart items that do not exist
        INSERT INTO [dbo].[Cart] ([UserId], [ProductId], [Quantity], [Price], [OfferPrice], [IsActive], [AddedDate])
        SELECT 
            @UserId, T.ProductId, T.Quantity, T.Price, T.OfferPrice, T.IsActive, T.AddedDate
        FROM @TempCart T
        WHERE NOT EXISTS (
            SELECT 1 FROM [dbo].[Cart] C WHERE C.ProductId = T.ProductId AND C.UserId = @UserId
        );

        -- Commit transaction
        COMMIT TRANSACTION CartInsertBulk;
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of an error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION CartInsertBulk;

        -- Capture error details
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
