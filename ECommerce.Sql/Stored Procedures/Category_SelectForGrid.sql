CREATE PROCEDURE [dbo].[Category_SelectForGrid]
    @Name			VARCHAR(255) = NULL, 
    @ParentId		INT = NULL, 
    @SortExpression VARCHAR(50),
    @SortDirection	VARCHAR(5),
    @PageIndex		INT,
    @PageSize		INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SqlQuery NVARCHAR(MAX);

    SET @SqlQuery = N'
    SELECT 
        C.[Id], 
        C.[Name], 
        C.[Description],
        C.[ParentId], 
        C.[ImageUrl], 
        C.[IsVisible],
        P.[Name] AS ParentName
    FROM 
        [Category] C
    LEFT JOIN 
        [Category] P ON C.[ParentId] = P.[Id]
    WHERE 
        C.[Name] LIKE COALESCE(@Name, C.[Name]) + ''%''
        AND (@ParentId IS NULL OR C.[ParentId] = @ParentId)
    ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    ';

    -- Execute the dynamic SQL query
    EXEC sp_executesql @SqlQuery, 
        N'@Name VARCHAR(255), @ParentId INT = NULL, @PageIndex INT, @PageSize INT', 
        @Name, @ParentId, @PageIndex, @PageSize;

    -- Get the total count of records
    SELECT COUNT(1) AS TotalRecords
    FROM [Category] C
    WHERE 
        C.[Name] LIKE COALESCE(@Name, C.[Name]) + '%'
        AND (@ParentId IS NULL OR C.[ParentId] = @ParentId)
END;
