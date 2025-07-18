CREATE PROCEDURE [dbo].[Block_SelectForGrid]
    @Name           NVARCHAR(50)    = NULL,
    @Description    NVARCHAR(500)   = NULL,
    @IsActive       BIT             = NULL,
    @SortExpression VARCHAR(50),
    @SortDirection  VARCHAR(5),
    @PageIndex      INT,
    @PageSize       INT
AS
BEGIN
    DECLARE @SqlQuery NVARCHAR(MAX);

    SET @SqlQuery = N'
    SELECT 
        B.[Id], 
        B.[Name], 
        B.[Description], 
        B.[IsActive]
    FROM 
        [Block] B WITH (NOLOCK)
    WHERE 
        B.[Name] LIKE COALESCE(@Name, B.[Name]) + ''%''
        AND (@Description IS NULL OR B.[Description] LIKE @Description + ''%'')
        AND (@IsActive IS NULL OR B.[IsActive] = @IsActive)
    ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY
    ';

    EXEC sp_executesql @SqlQuery, 
        N'@Name NVARCHAR(50), @Description NVARCHAR(500), @IsActive BIT, @PageIndex INT, @PageSize INT, @SortExpression NVARCHAR(50), @SortDirection NVARCHAR(5)', 
        @Name, @Description, @IsActive, @PageIndex, @PageSize, @SortExpression, @SortDirection;

    SELECT COUNT(1) AS TotalRecords
    FROM [Block] B WITH (NOLOCK)
    WHERE 
        B.[Name] LIKE COALESCE(@Name, B.[Name]) + '%'
        AND (@Description IS NULL OR B.[Description] LIKE @Description + '%')
        AND (@IsActive IS NULL OR B.[IsActive] = @IsActive);
END
