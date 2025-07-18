
CREATE PROCEDURE [dbo].[PurchaseOrder_Update]
    @Id INT, 
    @OrderNumber INT,
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
    BEGIN TRANSACTION PurchaseOrderTransaction;
    BEGIN TRY
        -- Update PurchaseOrder table
        UPDATE [dbo].[PurchaseOrder]
        SET 
            [OrderNumber] = @OrderNumber,
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
        WHERE [Id] = @Id;  -- Using Id as the primary key

        -- Delete existing PurchaseOrderItems for the given Id
        DELETE FROM [dbo].[PurchaseOrderItem]
        WHERE [PurchaseOrderId] = @Id;  -- Use PurchaseOrderId here if the column exists

        -- Insert updated PurchaseOrderInvoiceItems if XML data is provided
        IF @InvoiceItemsXML IS NOT NULL
        BEGIN
            DECLARE @DocumentHandle INT;
            EXEC sp_xml_preparedocument @DocumentHandle OUTPUT, @InvoiceItemsXML;

            INSERT INTO [dbo].[PurchaseOrderItem] (
                [PurchaseOrderId],  -- Make sure this column exists
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
                @Id,  -- Using @Id here to link items to the correct purchase order
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
            FROM OPENXML (@DocumentHandle, '/ArrayOfPurchaseOrderItemEntity/PurchaseOrderItemEntity', 2)
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

            EXEC sp_xml_removedocument @DocumentHandle;
        END

        -- Commit the transaction if no errors
        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION PurchaseOrderTransaction;

        -- Return the updated PurchaseOrder data and related PurchaseOrderItems as JSON
        SELECT 
            P.[Id],
            P.[OrderNumber],
            P.[CreatedBy],
            P.[CreatedOn],
            P.[LastUpdatedBy],
            P.[LastUpdatedOn],
            P.[VendorId],
            P.[Description],
            P.[TotalQuantity],
            P.[TotalAmount],
            P.[TotalDiscount],
            P.[TotalTax],
            P.[TotalFinalAmount],
            -- Return related PurchaseOrderItems as JSON
            (SELECT 
                POI.[ProductId],
                POI.[ProductName],
                POI.[Quantity],
                POI.[Price],
                POI.[Amount],
                POI.[DiscountPercentage],
                POI.[DiscountedAmount],
                POI.[Tax],
                POI.[FinalAmount],
                POI.[ExpiryDate]
            FROM [dbo].[PurchaseOrderItem] POI
            WHERE POI.[PurchaseOrderId] = P.[Id]
            FOR JSON PATH) AS PurchaseOrderItems
        FROM [dbo].[PurchaseOrder] P
        WHERE P.[Id] = @Id;

    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of errors
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION PurchaseOrderTransaction;

        -- Rethrow the error with details
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
