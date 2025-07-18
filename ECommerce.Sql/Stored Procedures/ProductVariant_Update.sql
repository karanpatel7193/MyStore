CREATE PROCEDURE [dbo].[ProductVariant_Update]
    @Id                    INT,
    @ProductId             INT,
    @VariantPropertyId     INT,
    @VariantPropertyValue  VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Avoid updating to a value that already exists for another record
    IF NOT EXISTS (
        SELECT [Id]
        FROM [ProductVariant]
        WHERE [ProductId] = @ProductId
          AND [VariantPropertyId] = @VariantPropertyId
          AND [VariantPropertyValue] = @VariantPropertyValue
          AND [Id] != @Id
    )
    BEGIN
        UPDATE [ProductVariant]
        SET [ProductId] = @ProductId,
            [VariantPropertyId] = @VariantPropertyId,
            [VariantPropertyValue] = @VariantPropertyValue
        WHERE [Id] = @Id;
    END
    ELSE
        SET @Id = 0;

    SELECT @Id;
END
GO
