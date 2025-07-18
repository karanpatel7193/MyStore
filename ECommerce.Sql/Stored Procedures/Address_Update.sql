CREATE PROCEDURE [dbo].[Address_Update]
    @Id              INT,
    @UserId          BIGINT,
    @FullName        NVARCHAR(255),
    @MobileNumber    VARCHAR(15),
    @AlternateNumber VARCHAR(15) = NULL,
    @AddressLine     NVARCHAR(500),
    @Landmark        NVARCHAR(255) = NULL,
    @CityId          INT,
    @StateId         INT,
    @PinCode         VARCHAR(10),
    @AddressType     VARCHAR(50),
    @IsDefault       BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Ensure the address exists for the given UserId and Id
    IF EXISTS (SELECT 1 FROM [dbo].[Address] WHERE Id = @Id AND UserId = @UserId)
    BEGIN
        -- If setting as default, remove default from other addresses of this user
        IF @IsDefault = 1
        BEGIN
            UPDATE [dbo].[Address]
            SET IsDefault = 0
            WHERE UserId = @UserId;
        END

        -- Update the specific address
        UPDATE [dbo].[Address]
        SET FullName = @FullName,
            MobileNumber = @MobileNumber,
            AlternateNumber = @AlternateNumber,
            AddressLine = @AddressLine,
            Landmark = @Landmark,
            CityId = @CityId,
            StateId = @StateId,
            PinCode = @PinCode,
            AddressType = @AddressType,
            IsDefault = @IsDefault
        WHERE Id = @Id AND UserId = @UserId;

        SELECT @Id AS UpdatedId;  -- Return updated Id
    END
    ELSE
    BEGIN
        SET @Id = 0;  -- Address not found
        SELECT @Id AS UpdatedId;
    END
END;
