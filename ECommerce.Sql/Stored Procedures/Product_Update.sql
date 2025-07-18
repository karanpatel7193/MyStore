CREATE PROCEDURE [dbo].[Product_Update]
    @Id                     INT,
    @Name                   VARCHAR(200),
    @Description            VARCHAR(500) = '',
    @LongDescription        VARCHAR(5000) = '',
    @CategoryId             INT = 0,
    @AllowReturn            BIT = 0,
    @ReturnPolicy           VARCHAR(500) = '',
    @IsExpiry               BIT = 0,
    @LastUpdatedBy          INT,
    @CreatedBy              INT,
    @LastUpdatedOn          DATETIME,
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
    @ProductVariantIds		VARCHAR(300) = NULL,
    @ProductVariantIndexs   VARCHAR(300) = NULL,
    @ProductMediaXML        XML     = NULL,
    @VariantCombinationXML  XML     = NULL
AS
BEGIN
    BEGIN TRANSACTION ProductUpdate;

    DECLARE @DocHandle INT;

    -- 🆕 Table variable to store variant-specific property IDs
    DECLARE @VariantPropertyIdTable TABLE (PropertyId INT);

    BEGIN TRY
        -- Update Product
        UPDATE [dbo].[Product]
        SET 
            [Name]              = @Name,
            [Description]       = @Description,
            [LongDescription]   = @LongDescription,
            [CategoryId]        = @CategoryId,
            [AllowReturn]       = @AllowReturn,
            [ReturnPolicy]      = @ReturnPolicy,
            [IsExpiry]          = @IsExpiry,
            [CreatedBy]         = @CreatedBy,
            [LastUpdatedBy]     = @LastUpdatedBy,
            [LastUpdatedOn]     = @LastUpdatedOn,
            [SKU]               = @SKU,
            [UPC]               = @UPC,
            [ProductVariantIds] = @ProductVariantIds
        WHERE 
            [Id]                = @Id;

        -- Update Inventory
       IF EXISTS (SELECT 1 FROM [dbo].[ProductInventory] WHERE ProductId = @Id)
        BEGIN
            UPDATE [dbo].[ProductInventory]
            SET 
                [OpeningQty] = @OpeningQty,
                [BuyQty] = @BuyQty,
                [LockQty] = @LockQty,
                [OrderQty] = @OrderQty,
                [SellQty] = @SellQty,
                [ClosingQty] = @ClosingQty,
                [CostPrice] = @CostPrice,
                [SellPrice] = @SellPrice,
                [DiscountPercentage] = @DiscountPercentage,
                [DiscountAmount] = @DiscountAmount,
                [FinalSellPrice] = @FinalSellPrice
            WHERE [ProductId] = @Id;
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[ProductInventory]
            (
                ProductId, OpeningQty, BuyQty, LockQty, OrderQty, SellQty, 
                ClosingQty, CostPrice, SellPrice, DiscountPercentage, 
                DiscountAmount, FinalSellPrice
            )
            VALUES
            (
                @Id, @OpeningQty, @BuyQty, @LockQty, @OrderQty, @SellQty, 
                @ClosingQty, @CostPrice, @SellPrice, @DiscountPercentage, 
                @DiscountAmount, @FinalSellPrice
            );
        END


        -- Handle ProductMedia
        IF @ProductMediaXML IS NOT NULL
        BEGIN
            EXEC sp_xml_preparedocument @DocHandle OUTPUT, @ProductMediaXML;

            DELETE FROM [dbo].[ProductMedia]
            WHERE [ProductId] = @Id
              AND [Id] NOT IN (
                  SELECT [Id] 
                  FROM OPENXML(@DocHandle, '/ArrayOfProductMediaEntity/ProductMediaEntity', 2) 
                  WITH ([Id] INT)
              );

            UPDATE PM
            SET 
                [Type] = PMXML.[Type], 
                [Url] = REPLACE(PMXML.[Url], '{Id}', CONVERT(VARCHAR, @Id)),
                [ThumbUrl] = REPLACE(PMXML.[ThumbUrl], '{Id}', CONVERT(VARCHAR, @Id)),
                [Description] = PMXML.[Description]
            FROM [dbo].[ProductMedia] PM
            INNER JOIN OPENXML(@DocHandle, '/ArrayOfProductMediaEntity/ProductMediaEntity', 2) 
                WITH ([Id] INT, [Type] INT, [Url] VARCHAR(500), [ThumbUrl] VARCHAR(500), [Description] VARCHAR(500)) PMXML
                ON PM.[Id] = PMXML.[Id] AND PM.[ProductId] = @Id;

            INSERT INTO [dbo].[ProductMedia] ([ProductId], [Type], [Url], [ThumbUrl], [Description])
            SELECT @Id, 
                   [Type], 
                   REPLACE([Url], '{Id}', CONVERT(VARCHAR, @Id)),
                   REPLACE([ThumbUrl], '{Id}', CONVERT(VARCHAR, @Id)),
                   [Description]
            FROM OPENXML(@DocHandle, '/ArrayOfProductMediaEntity/ProductMediaEntity', 2)
                WITH ([Id] INT, [Type] INT, [Url] VARCHAR(500), [ThumbUrl] VARCHAR(500), [Description] VARCHAR(500))
            WHERE [Id] = 0;

            EXEC sp_xml_removedocument @DocHandle;
        END

        -- Handle VariantCombination insert/update/delete
		IF @VariantCombinationXML IS NOT NULL
		BEGIN
			EXEC sp_xml_preparedocument @DocHandle OUTPUT, @VariantCombinationXML;

			-- Delete old variants that are not in the XML
			DELETE FROM [dbo].[ProductVariant]
			WHERE [ProductId] = @Id
			  AND [Id] NOT IN (
				  SELECT [Id]
				  FROM OPENXML(@DocHandle, '/ArrayOfProductVariantEntity/ProductVariantEntity', 2)
				  WITH ([Id] INT)
			  );

			-- Update existing variants
			UPDATE PV
			SET 
				PV.[VariantPropertyId] = PX.[VariantPropertyId],
				PV.[VariantPropertyValue] = PX.[VariantPropertyValue]
			FROM [dbo].[ProductVariant] PV
			INNER JOIN OPENXML(@DocHandle, '/ArrayOfProductVariantEntity/ProductVariantEntity', 2)
				WITH (
					[Id] INT,
					[VariantPropertyId] INT,
					[VariantPropertyValue] VARCHAR(100)
				) PX
				ON PV.[Id] = PX.[Id] AND PV.[ProductId] = @Id;

			-- Insert new variants
			INSERT INTO [dbo].[ProductVariant] ([ProductId], [VariantPropertyId], [VariantPropertyValue])
			SELECT 
				@Id,
				PX.[VariantPropertyId],
				PX.[VariantPropertyValue]
			FROM OPENXML(@DocHandle, '/ArrayOfProductVariantEntity/ProductVariantEntity', 2)
			WITH (
				[Id] INT,
				[VariantPropertyId] INT,
				[VariantPropertyValue] VARCHAR(100)
			) PX
			WHERE [Id] = 0;

			EXEC sp_xml_removedocument @DocHandle;
		END

        COMMIT TRANSACTION ProductUpdate;
        SELECT @Id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION ProductUpdate;

        IF @DocHandle IS NOT NULL
            EXEC sp_xml_removedocument @DocHandle;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(), 
               @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
