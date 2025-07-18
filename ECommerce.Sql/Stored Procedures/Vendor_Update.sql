CREATE PROCEDURE [dbo].[Vendor_Update]
    @Id                   BIGINT,
    @Name                 NVARCHAR(100),
    @Email                NVARCHAR(100),
    @Phone                BIGINT,
    @Address              NVARCHAR(255),
    @CountryId            SMALLINT,
    @StateId              INT,
    @PostalCode           NVARCHAR(10),
    @BankAccountNumber    NVARCHAR(50)    = NULL,
    @BankName             NVARCHAR(100)   = NULL,
    @IFSCCode             NVARCHAR(20)    = NULL,
    @ContactPersonName    NVARCHAR(100)   = NULL,
    @ContactPersonPhone   NVARCHAR(15)    = NULL,
    @CreatedOn                DATETIME        = NULL,
    @TaxNumber            NVARCHAR(50)    = NULL,
    @TotalOutstanding     DECIMAL         = NULL,
    @TotalPaid            DECIMAL         = NULL,
    @TotalInvoices        DECIMAL         = NULL,
    @Status               BIT
AS
BEGIN
    IF NOT EXISTS(SELECT [Id] 
                  FROM [Vendor] 
                  WHERE ([Email] = @Email OR [Phone] = @Phone) 
                  AND [Id] != @Id)
    BEGIN
        UPDATE [Vendor]
        SET [Name]              = @Name,
            [Email]             = @Email,
            [Phone]             = @Phone,
            [Address]           = @Address,
            [CountryId]         = @CountryId,
            [StateId]           = @StateId,
            [PostalCode]        = @PostalCode,
            [BankAccountNumber] = @BankAccountNumber,
            [BankName]          = @BankName,
            [IFSCCode]          = @IFSCCode,
            [ContactPersonName] = @ContactPersonName,
            [ContactPersonPhone] = @ContactPersonPhone,
            [TaxNumber]         = @TaxNumber,
            [TotalOutstanding]  = @TotalOutstanding,
            [TotalPaid]         = @TotalPaid,
            [TotalInvoices]     = @TotalInvoices,
            [Status]            = @Status
        WHERE [Id] = @Id;
    END
    ELSE
        SET @Id = 0;

    SELECT @Id;
END
GO
