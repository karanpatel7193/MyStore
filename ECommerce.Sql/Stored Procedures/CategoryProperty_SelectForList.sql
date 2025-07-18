CREATE PROCEDURE [dbo].[CategoryProperty_SelectForList]
    @CategoryId     INT = NULL,
    @PropertyId     INT = NULL,
    @Unit           VARCHAR(50) = NULL,
    @SortExpression VARCHAR(50),
    @SortDirection  VARCHAR(5),
    @PageIndex      INT,
    @PageSize       INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Fetch LOV (List of Values) for related entities
    EXEC Property_SelectForLov;

    -- Fetch paginated data for CategoryProperty
    SELECT 
        [Id], 
        [CategoryId], 
        [PropertyId], 
        [Unit]
    FROM 
        [CategoryProperty]
    WHERE 
        (@CategoryId IS NULL OR [CategoryId] = @CategoryId) AND
        (@PropertyId IS NULL OR [PropertyId] = @PropertyId) AND
        (@Unit IS NULL OR [Unit] = @Unit)
    ORDER BY 
        CASE 
            WHEN @SortExpression = 'CategoryId' AND @SortDirection = 'ASC' THEN [CategoryId]
            WHEN @SortExpression = 'CategoryId' AND @SortDirection = 'DESC' THEN -[CategoryId]
            WHEN @SortExpression = 'PropertyId' AND @SortDirection = 'ASC' THEN [PropertyId]
            WHEN @SortExpression = 'PropertyId' AND @SortDirection = 'DESC' THEN -[PropertyId]
            ELSE [Id]
        END
    OFFSET @PageIndex * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;