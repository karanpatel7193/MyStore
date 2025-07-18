
CREATE PROCEDURE [dbo].[Product_Insert]
    @Name                   VARCHAR(200),
    @Description            VARCHAR(500)	= '',
    @LongDescription        VARCHAR(5000)	= '',
    @CategoryId             INT				= 0,
    @AllowReturn            BIT				= 0,
    @ReturnPolicy           VARCHAR(500)	= '',
    @IsExpiry               BIT				= 0,
    @CreatedBy              INT,
    @LastUpdatedBy          INT,
    @LastUpdatedOn          DATETIME,
    @CreatedOn				DATETIME,
    @SKU                    VARCHAR(20),
    @UPC                    INT,
    @OpeningQty             INT,
    @BuyQty                 INT,
    @LockQty                INT,
    @OrderQty               INT,
    @SellQty                INT,
    @ClosingQty             INT,
    @CostPrice              FLOAT,
    @SellPrice              FLOAT,
    @DiscountPercentage     FLOAT,
    @DiscountAmount         FLOAT,
    @FinalSellPrice         FLOAT,
    @ParentProductId        INT			        = NULL,
    @ProductVariantIds      VARCHAR(300)		= NULL,
    @ProductMediaXML        XML			        = NULL,
    @VariantCombinationXML  XML			        = NULL
AS
BEGIN
    BEGIN TRANSACTION ProductInsert;
    DECLARE @DocHandle INT;
    DECLARE @InsertedId INT;

    BEGIN TRY
        -- Insert into Product table
        INSERT INTO [dbo].[Product]
        (
            [Name], [Description], [LongDescription], [CategoryId], [AllowReturn], 
            [ReturnPolicy], [IsExpiry], [CreatedBy], [LastUpdatedBy], [LastUpdatedOn],[CreatedOn],
            [SKU], [UPC], [ParentProductId], [ProductVariantIds]
        )
        VALUES
        (
            @Name, @Description, @LongDescription, @CategoryId, @AllowReturn,
            @ReturnPolicy, @IsExpiry, @CreatedBy, @LastUpdatedBy, @LastUpdatedOn,@CreatedOn,
            @SKU, @UPC, @ParentProductId, @ProductVariantIds
        );

        SET @InsertedId = SCOPE_IDENTITY();

        -- Insert into ProductInventory
        INSERT INTO [dbo].[ProductInventory]
        (
            ProductId, OpeningQty, BuyQty, LockQty, OrderQty, SellQty, 
            ClosingQty, CostPrice, SellPrice, DiscountPercentage, 
            DiscountAmount, FinalSellPrice
        )
        VALUES
        (
            @InsertedId, @OpeningQty, @BuyQty, @LockQty, @OrderQty, @SellQty, 
            @ClosingQty, @CostPrice, @SellPrice, @DiscountPercentage, 
            @DiscountAmount, @FinalSellPrice
        );

        -- Insert ProductMedia if XML is present
        IF @ProductMediaXML IS NOT NULL
        BEGIN
            EXEC sp_xml_preparedocument @DocHandle OUTPUT, @ProductMediaXML;

            INSERT INTO [dbo].[ProductMedia] (ProductId, [Type], [Url], [ThumbUrl], [Description])
            SELECT @InsertedId,
                   [Type],
                   REPLACE([Url], '{Id}', CONVERT(VARCHAR, @InsertedId)),
                   REPLACE([ThumbUrl], '{Id}', CONVERT(VARCHAR, @InsertedId)),
                   [Description]
            FROM OPENXML(@DocHandle, '/ArrayOfProductMediaEntity/ProductMediaEntity', 2)
            WITH ([Type] INT, [Url] VARCHAR(500), [ThumbUrl] VARCHAR(500), [Description] VARCHAR(500));

            EXEC sp_xml_removedocument @DocHandle;
        END

        -- Insert Variant Combinations if XML is present
        IF @VariantCombinationXML IS NOT NULL
        BEGIN
            EXEC sp_xml_preparedocument @DocHandle OUTPUT, @VariantCombinationXML;

            INSERT INTO [dbo].[ProductVariant] (ProductId, VariantPropertyId, VariantPropertyValue)
            SELECT @InsertedId, VariantPropertyId, VariantPropertyValue
            FROM OPENXML(@DocHandle, '/ArrayOfProductVariantEntity/ProductVariantEntity', 2)
            WITH (
                VariantPropertyId INT,
                VariantPropertyValue VARCHAR(100)
            )AS Sources
			WHERE NOT EXISTS (
				SELECT 1
				FROM [dbo].[ProductVariant] PV
				WHERE PV.ProductId = @InsertedId
				  AND PV.VariantPropertyId = Sources.VariantPropertyId
				  AND PV.VariantPropertyValue = Sources.VariantPropertyValue
			);

            EXEC sp_xml_removedocument @DocHandle;
        END

        COMMIT TRANSACTION ProductInsert;
        SELECT @InsertedId AS ProductId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION ProductInsert;

        IF @DocHandle IS NOT NULL
            EXEC sp_xml_removedocument @DocHandle;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(), 
               @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
