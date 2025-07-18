CREATE PROCEDURE [dbo].[Vendor_SelectForGrid]
    @Name NVARCHAR(100)                = NULL,
    @Email NVARCHAR(100)               = NULL,
    @Phone BIGINT                      = NULL,
    @CountryId SMALLINT                = NULL,
    @StateId INT                       = NULL,
    @PostalCode VARCHAR(10)           = NULL,
    @SortExpression VARCHAR(50),
    @SortDirection VARCHAR(5),
    @PageIndex INT,
    @PageSize INT
AS
BEGIN
    DECLARE @SqlQuery NVARCHAR(MAX)

    SET @SqlQuery = N'
    SELECT V.[Id], V.[Name], V.[Email], V.[Phone], V.[Address], 
           C.[Name] AS CountryName, S.[Name] AS StateName, 
           V.[PostalCode], V.[Status], V.[CreatedOn]
    FROM [Vendor] V WITH (NOLOCK)
    LEFT JOIN [Country] C ON V.[CountryId] = C.[Id]
    LEFT JOIN [State] S ON V.[StateId] = S.[Id]
    WHERE   V.[Name] LIKE COALESCE(@Name, V.[Name]) + ''%''
        AND (@Email IS NULL OR V.[Email] LIKE @Email + ''%'')
        AND (@Phone IS NULL OR V.[Phone] = @Phone)
        AND V.[CountryId] = COALESCE(@CountryId, V.[CountryId])
        AND V.[StateId] = COALESCE(@StateId, V.[StateId])
        AND (@PostalCode IS NULL OR V.[PostalCode] LIKE @PostalCode + ''%'')
    ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY
    '

    EXEC sp_executesql @SqlQuery, 
        N'@Name NVARCHAR(100), @Email NVARCHAR(100), @Phone BIGINT, 
          @CountryId SMALLINT, @StateId INT, @PostalCode NVARCHAR(10), 
          @PageIndex INT, @PageSize INT',
        @Name, @Email, @Phone, @CountryId, @StateId, @PostalCode, @PageIndex, @PageSize

    SELECT COUNT(1) AS TotalRecords
    FROM [Vendor] V WITH (NOLOCK)
    WHERE   V.[Name] LIKE COALESCE(@Name, V.[Name]) + '%'
        AND (@Email IS NULL OR V.[Email] LIKE @Email + '%')
        AND (@Phone IS NULL OR V.[Phone] = @Phone)
        AND V.[CountryId] = COALESCE(@CountryId, V.[CountryId])
        AND V.[StateId] = COALESCE(@StateId, V.[StateId])
        AND (@PostalCode IS NULL OR V.[PostalCode] LIKE @PostalCode + '%')
END
GO
