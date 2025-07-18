CREATE PROCEDURE [dbo].[Vendor_SelectForRecord]
    @Id BIGINT
AS
BEGIN
    SELECT 
        V.[Id], 
        V.[Name], 
        V.[Email], 
        V.[Phone], 
        V.[Address], 
        V.[CountryId], 
        V.[StateId], 
        V.[PostalCode], 
        V.[BankAccountNumber], 
        V.[BankName], 
        V.[IFSCCode], 
        V.[ContactPersonName], 
        V.[ContactPersonPhone], 
        V.[TaxNumber], 
        V.[TotalOutstanding], 
        V.[TotalPaid], 
        V.[TotalInvoices], 
        V.[Status], 
        V.[CreatedOn]
    FROM [Vendor] V WITH (NOLOCK)
    WHERE V.[Id] = @Id;
END
GO
