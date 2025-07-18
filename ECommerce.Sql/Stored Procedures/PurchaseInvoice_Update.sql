create PROCEDURE [dbo].[PurchaseInvoice_Update]
    @Id INT,
    @InvoiceNumber INT,
    @CreatedBy BIGINT,
    @CreatedOn DATETIME,
    @LastUpdatedBy INT,
    @LastUpdatedOn DATETIME,
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
        -- Update the main PurchaseInvoice table
        UPDATE [dbo].[PurchaseInvoice]
        SET 
            [InvoiceNumber] = @InvoiceNumber,
            [CreatedBy] = @CreatedBy,
            [CreatedOn] = @CreatedOn,
            [LastUpdatedBy] = @LastUpdatedBy,
            [LastUpdatedOn] = @LastUpdatedOn,
            [VendorId] = @VendorId,
            [Description] = @Description,
            [TotalQuantity] = @TotalQuantity,
            [TotalAmount] = @TotalAmount,
            [TotalDiscount] = @TotalDiscount,
            [TotalTax] = @TotalTax,
            [TotalFinalAmount] = @TotalFinalAmount
        WHERE [Id] = @Id;

        IF @InvoiceItemsXML IS NOT NULL
        BEGIN
            DECLARE @DocumentHandle INT;
            EXEC sp_xml_preparedocument @DocumentHandle OUTPUT, @InvoiceItemsXML;

            -- Handle inventory  old and new quantities
            DECLARE @OldItems TABLE (
                ProductId INT,
                OldQuantity INT
            );

            -- Save  items into a temporary table
            INSERT INTO @OldItems (ProductId, OldQuantity)
            SELECT [ProductId], [Quantity]
            FROM [dbo].[PurchaseInvoiceItem]
            WHERE [PurchaseInvoiceId] = @Id;

            DELETE FROM [dbo].[PurchaseInvoiceItem]
            WHERE [PurchaseInvoiceId] = @Id;

            --  PurchaseInvoiceItem table
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
                @Id,
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

            -- Compare old and new quantities and update inventory 
            UPDATE PI
            SET 
                PI.[BuyQty] = PI.[BuyQty] + 
                    CASE 
                        WHEN NI.[Quantity] > OI.[OldQuantity] THEN NI.[Quantity] - OI.[OldQuantity]  -- Increase
                        WHEN NI.[Quantity] < OI.[OldQuantity] THEN -(OI.[OldQuantity] - NI.[Quantity]) -- Decrease
                        ELSE 0
                    END,
                PI.[ClosingQty] = PI.[ClosingQty] + 
                    CASE 
                        WHEN NI.[Quantity] > OI.[OldQuantity] THEN NI.[Quantity] - OI.[OldQuantity]  -- Increase
                        WHEN NI.[Quantity] < OI.[OldQuantity] THEN -(OI.[OldQuantity] - NI.[Quantity]) -- Decrease
                        ELSE 0
                    END,
                PI.[CostPrice] = 
                    CASE 
                        WHEN NI.[Quantity] > OI.[OldQuantity] THEN 
                            ((PI.[ClosingQty] * PI.[CostPrice]) + ((NI.[Quantity] - OI.[OldQuantity]) * NI.[Price]))
                            / (PI.[ClosingQty] + (NI.[Quantity] - OI.[OldQuantity]))
                        WHEN NI.[Quantity] < OI.[OldQuantity] THEN 
                            ((PI.[ClosingQty] * PI.[CostPrice]) - ((OI.[OldQuantity] - NI.[Quantity]) * NI.[Price]))
                            / (PI.[ClosingQty] - (OI.[OldQuantity] - NI.[Quantity]))
                        ELSE PI.[CostPrice]
                    END
            FROM [dbo].[ProductInventory] PI
            INNER JOIN @OldItems OI ON PI.[ProductId] = OI.[ProductId]
            INNER JOIN OPENXML (@DocumentHandle, '/ArrayOfPurchaseInvoiceItemEntity/PurchaseInvoiceItemEntity', 2)
            WITH (
                [ProductId] INT,
                [Quantity] INT,
                [Price] FLOAT
            ) NI
            ON PI.[ProductId] = NI.[ProductId];

            EXEC sp_xml_removedocument @DocumentHandle;
        END

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION PurchaseInvoiceTransaction;

        SELECT @Id AS PurchaseInvoiceId;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION PurchaseInvoiceTransaction;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(), 
               @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
