CREATE PROCEDURE [dbo].[PurchaseOrder_Insert]
    @OrderNumber INT,
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
    @InvoiceItemsXML XML = Null
AS
BEGIN
    BEGIN TRANSACTION PurchaseOrderTransaction;
    BEGIN TRY
        DECLARE @PurchaseOrderId INT;

        -- Insert into PurchaseOrder table
        INSERT INTO [dbo].[PurchaseOrder] (
            [OrderNumber],
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
            @OrderNumber,
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

        SET @PurchaseOrderId = SCOPE_IDENTITY();

        -- Insert into PurchaseOrderInvoiceItem if XML data is provided
        IF @InvoiceItemsXML IS NOT NULL
        BEGIN
            DECLARE @DocumentHandle INT;
            EXEC sp_xml_preparedocument @DocumentHandle OUTPUT, @InvoiceItemsXML;

            INSERT INTO [dbo].[PurchaseOrderItem] (
                [PurchaseOrderId],
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
                @PurchaseOrderId,
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

        -- Return the PurchaseOrderId
        SELECT @PurchaseOrderId AS PurchaseOrderId;
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