CREATE PROCEDURE [dbo].[Cart_Insert]
    @UserId      BIGINT,
    @ProductId   INT,
    @Quantity    INT,
    @Price       DECIMAL(18,2) = NULL,
    @OfferPrice  DECIMAL(18,2) = NULL,
    @IsActive    BIT = 1,
    @AddedDate   DATE
AS
BEGIN
    BEGIN TRANSACTION CartInsert;
    BEGIN TRY
        DECLARE @CartId INT;

        -- Get price and offer price if not provided
        IF @Price IS NULL OR @OfferPrice IS NULL
        BEGIN
            SELECT @Price = SellPrice, 
                   @OfferPrice = FinalSellPrice
            FROM [dbo].[ProductInventory]
            WHERE ProductId = @ProductId;
        END

        -- Check if the product already exists in the cart
        IF EXISTS (SELECT 1 FROM [dbo].[Cart] WHERE UserId = @UserId AND ProductId = @ProductId)
        BEGIN
            -- Update quantity if the product already exists
            UPDATE [dbo].[Cart]
            SET Quantity = Quantity + @Quantity, 
                Price = @Price, 
                OfferPrice = @OfferPrice, 
                IsActive = @IsActive, 
                AddedDate = @AddedDate
            WHERE UserId = @UserId AND ProductId = @ProductId;

            -- Get the existing CartId
            SELECT @CartId = Id FROM [dbo].[Cart] WHERE UserId = @UserId AND ProductId = @ProductId;
        END
        ELSE
        BEGIN
            -- Insert new entry if product is not in the cart
            INSERT INTO [dbo].[Cart] (
                [UserId], 
                [ProductId], 
                [Quantity], 
                [Price], 
                [OfferPrice], 
                [IsActive], 
                [AddedDate]
            )
            VALUES (
                @UserId, 
                @ProductId, 
                @Quantity, 
                @Price, 
                @OfferPrice, 
                @IsActive, 
                @AddedDate
            );

            -- Get the inserted Cart ID
            SET @CartId = SCOPE_IDENTITY();
        END

        -- Commit transaction
        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION CartInsert;

        -- Return the inserted or updated Cart ID
        SELECT @CartId AS CartId;
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of an error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION CartInsert;

        -- Capture error details
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(), 
               @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
