CREATE PROCEDURE [dbo].[Vendor_Insert]
    @Name                     NVARCHAR(100),
    @Email                    NVARCHAR(100),
    @Phone                    BIGINT,
    @Address                  NVARCHAR(255),
    @CountryId                SMALLINT,
    @StateId                  INT,
    @PostalCode               NVARCHAR(10),
    @BankAccountNumber        NVARCHAR(50)    = NULL,
    @BankName                 NVARCHAR(100)   = NULL,
    @IFSCCode                 NVARCHAR(20)    = NULL,
    @ContactPersonName        NVARCHAR(100)   = NULL,
    @ContactPersonPhone       NVARCHAR(15)    = NULL,
    @CreatedOn                DATETIME        = NULL,
    @TaxNumber                NVARCHAR(50)    = NULL,
    @TotalOutstanding         DECIMAL         = NULL,
    @TotalPaid                DECIMAL         = NULL,
    @TotalInvoices            DECIMAL         = NULL,
    @Status                   BIT
AS
BEGIN
    DECLARE @Id BIGINT;

    IF NOT EXISTS(SELECT [Id] FROM [Vendor] WHERE ([Email] = @Email AND [Phone] = @Phone))
    BEGIN
        INSERT INTO [Vendor] (
            [Name],
            [Email],
            [Phone],
            [Address],
            [CountryId],
            [StateId],
            [PostalCode],
            [BankAccountNumber],
            [BankName],
            [IFSCCode],
            [ContactPersonName],
            [ContactPersonPhone],
            [CreatedOn],
            [TaxNumber],
            [TotalOutstanding],
            [TotalPaid],
            [TotalInvoices],
            [Status]
        )
        VALUES (
            @Name,
            @Email,
            @Phone,
            @Address,
            @CountryId,
            @StateId,
            @PostalCode,
            @BankAccountNumber,
            @BankName,
            @IFSCCode,
            @ContactPersonName,
            @ContactPersonPhone,
            @CreatedOn,
            @TaxNumber,
            @TotalOutstanding,
            @TotalPaid,
            @TotalInvoices,
            @Status
        );
        
        SET @Id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SET @Id = 0;
    END

    SELECT @Id;
END
GO
