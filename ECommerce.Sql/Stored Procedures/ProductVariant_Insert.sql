CREATE PROCEDURE [dbo].[ProductVariant_Insert]
    @ProductId             INT,
    @VariantPropertyId     INT,
    @VariantPropertyValue  VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id INT;

    -- Check if an exact match already exists
    SELECT @Id = [Id]
    FROM [ProductVariant]
    WHERE [ProductId] = @ProductId
      AND [VariantPropertyId] = @VariantPropertyId
      AND [VariantPropertyValue] = @VariantPropertyValue;

    -- 1. If exact match exists, return existing ID (do nothing)
    IF @Id IS NOT NULL
    BEGIN
        SELECT @Id; -- Already exists
        RETURN;
    END

    -- 2. If another value exists for the same ProductId + PropertyId, update it
    SELECT @Id = [Id]
    FROM [ProductVariant]
    WHERE [ProductId] = @ProductId
      AND [VariantPropertyId] = @VariantPropertyId;

    IF @Id IS NOT NULL
    BEGIN
        UPDATE [ProductVariant]
        SET [VariantPropertyValue] = @VariantPropertyValue
        WHERE [Id] = @Id;

        SELECT @Id; -- Return updated ID
        RETURN;
    END

    -- 3. Otherwise, insert new record
    INSERT INTO [dbo].[ProductVariant] (
        [ProductId],
        [VariantPropertyId],
        [VariantPropertyValue]
    )
    VALUES (
        @ProductId,
        @VariantPropertyId,
        @VariantPropertyValue
    );

    SELECT SCOPE_IDENTITY(); -- Return newly inserted ID
END
GO
