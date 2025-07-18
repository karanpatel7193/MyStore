CREATE PROCEDURE [dbo].[Vendor_SelectForList]
    @Name               NVARCHAR(100)    = NULL, 
    @Email              NVARCHAR(250)    = NULL, 
    @Phone              BIGINT           = NULL, 
    @PostalCode         NVARCHAR(10)     = NULL,
    @CountryId          SMALLINT         = NULL, 
    @StateId            INT              = NULL, 
    @SortExpression     VARCHAR(50),
    @SortDirection      VARCHAR(5),
    @PageIndex          INT,
    @PageSize           INT
AS
BEGIN
    --EXEC Country_SelectForLOV
    --EXEC State_SelectForLOV

    EXEC Vendor_SelectForGrid 
        @Name, 
        @Email, 
        @Phone,
        @PostalCode,
        @CountryId, 
        @StateId, 
        @SortExpression, 
        @SortDirection, 
        @PageIndex, 
        @PageSize
END
GO
