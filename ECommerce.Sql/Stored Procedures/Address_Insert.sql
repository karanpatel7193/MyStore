CREATE PROCEDURE [dbo].[Address_Insert]
    @UserId           BIGINT,
    @FullName         NVARCHAR(255),
    @MobileNumber     VARCHAR(15),
    @AlternateNumber  VARCHAR(15) = NULL,
    @AddressLine      NVARCHAR(500),
    @Landmark         NVARCHAR(255) = NULL,
    @CityId           INT,
    @StateId          INT,
    @PinCode          VARCHAR(10),
    @AddressType      VARCHAR(50),
    @IsDefault        BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id INT;

    INSERT INTO [dbo].[Address] (
        UserId, FullName, MobileNumber, AlternateNumber, AddressLine, 
        Landmark, CityId, StateId, PinCode, AddressType, IsDefault
    ) 
    VALUES (
        @UserId, @FullName, @MobileNumber, @AlternateNumber, @AddressLine, 
        @Landmark, @CityId, @StateId, @PinCode, @AddressType, @IsDefault
    );

    SET @Id = SCOPE_IDENTITY();

    SELECT @Id AS InsertedId;
END;
