CREATE PROCEDURE [dbo].[PurchaseInvoice_Insert]
    @InvoiceNumber INT,
    @CreatedBy BIGINT,
    @CreatedOn DATETIME,
    @LastUpdatedBy INT = NULL,
    @LastUpdatedOn DATETIME = NULL,
    @VendorId INT,
    @Description NVARCHAR(MAX) = NULL,
    @TotalQuantity FLOAT,
    @TotalAmount FLOAT,
    @TotalDiscount FLOAT,
    @TotalTax FLOAT,
    @TotalFinalAmount FLOAT,
    @InvoiceItemsXML XML = NULL
AS
BEGIN
    BEGIN TRANSACTION PurchaseInvoiceTransaction;
    BEGIN TRY
        DECLARE @PurchaseInvoiceId INT;

        -- Insert into PurchaseInvoice table
        INSERT INTO [dbo].[PurchaseInvoice] (
            [InvoiceNumber],
            [CreatedBy],
            [CreatedOn],
            [LastUpdatedBy],
            [LastUpdatedOn],
            [VendorId],
            [Description],
            [TotalQuantity],
            [TotalAmount],
            [TotalDiscount],
            [TotalTax],
            [TotalFinalAmount]
        )
        VALUES (
            @InvoiceNumber,
            @CreatedBy,
            @CreatedOn,
            @LastUpdatedBy,
            @LastUpdatedOn,
            @VendorId,
            @Description,
            @TotalQuantity,
            @TotalAmount,
            @TotalDiscount,
            @TotalTax,
            @TotalFinalAmount
        );

        SET @PurchaseInvoiceId = SCOPE_IDENTITY();

        -- Insert into PurchaseInvoiceItem if XML data is provided
        IF @InvoiceItemsXML IS NOT NULL
        BEGIN
            DECLARE @DocumentHandle INT;
            EXEC sp_xml_preparedocument @DocumentHandle OUTPUT, @InvoiceItemsXML;

            -- Insert PurchaseInvoiceItem records
            INSERT INTO [dbo].[PurchaseInvoiceItem] (
                [PurchaseInvoiceId],
                [ProductId],
                [ProductName],
                [Quantity],
                [Price],
                [Amount],
                [DiscountPercentage],
                [DiscountedAmount],
                [Tax],
                [FinalAmount],
                [ExpiryDate]
            )
            SELECT
                @PurchaseInvoiceId,
                [ProductId],
                [ProductName],
                [Quantity],
                [Price],
                [Amount],
                [DiscountPercentage],
                [DiscountedAmount],
                [Tax],
                [FinalAmount],
                [ExpiryDate]
            FROM OPENXML (@DocumentHandle, '/ArrayOfPurchaseInvoiceItemEntity/PurchaseInvoiceItemEntity', 2)
            WITH (
                [ProductId] INT,
                [ProductName] NVARCHAR(MAX),
                [Quantity] INT,
                [Price] FLOAT,
                [Amount] FLOAT,
                [DiscountPercentage] FLOAT,
                [DiscountedAmount] FLOAT,
                [Tax] FLOAT,
                [FinalAmount] FLOAT,
                [ExpiryDate] DATETIME
            );

            -- Update ProductInventory table
            DECLARE @InventoryUpdates TABLE (
                ProductId INT,
                Quantity INT,
                Price FLOAT
            );

            INSERT INTO @InventoryUpdates (ProductId, Quantity, Price)
            SELECT
                [ProductId],
                [Quantity],
                [Price]
            FROM OPENXML (@DocumentHandle, '/ArrayOfPurchaseInvoiceItemEntity/PurchaseInvoiceItemEntity', 2)
            WITH (
                [ProductId] INT,
                [Quantity] INT,
                [Price] FLOAT
            );

            -- Update inventory for each product
            UPDATE PI
            SET 
                PI.[BuyQty] = PI.[BuyQty] + IU.[Quantity],
                PI.[ClosingQty] = PI.[ClosingQty] + IU.[Quantity],
                PI.[CostPrice] = ((PI.[ClosingQty] * PI.[CostPrice]) + (IU.[Quantity] * IU.[Price])) / (PI.[ClosingQty] + IU.[Quantity])
            FROM [dbo].[ProductInventory] PI
            INNER JOIN @InventoryUpdates IU ON PI.[ProductId] = IU.[ProductId];

            EXEC sp_xml_removedocument @DocumentHandle;
        END

        -- Commit the transaction if no errors
        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION PurchaseInvoiceTransaction;

        -- Return the PurchaseInvoiceId
        SELECT @PurchaseInvoiceId AS PurchaseInvoiceId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION PurchaseInvoiceTransaction;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
