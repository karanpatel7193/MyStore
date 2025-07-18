
CREATE PROCEDURE [dbo].[CategoryProperty_SelectForGrid]
    @CategoryId     INT             = NULL, 
    @PropertyId     INT             = NULL,
	@Unit           VARCHAR(50) = NULL,
    @SortExpression VARCHAR(50),
    @SortDirection  VARCHAR(5),
    @PageIndex      INT,
    @PageSize       INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Get paginated data with property names and category name
    SELECT 
        CP.[Id], 
        CP.[CategoryId], 
        CP.[PropertyId], 
        P.[Name] AS Name,    -- Property Name
        C.[Name] AS CategoryName,    -- Category Name
        CP.[Unit]
    FROM 
        [CategoryProperty] CP WITH (NOLOCK)
    INNER JOIN 
        [Property] P WITH (NOLOCK) ON CP.[PropertyId] = P.[Id]
    INNER JOIN 
        [Category] C WITH (NOLOCK) ON CP.[CategoryId] = C.[Id]  -- Join with Category table to get the Category Name
    WHERE 
        (@CategoryId IS NULL OR CP.[CategoryId] = @CategoryId) 
        AND (@PropertyId IS NULL OR CP.[PropertyId] = @PropertyId)
    ORDER BY 
        CASE WHEN @SortExpression = 'PropertyName' AND @SortDirection = 'ASC' THEN P.[Name] END ASC,
        CASE WHEN @SortExpression = 'PropertyName' AND @SortDirection = 'DESC' THEN P.[Name] END DESC,
        CASE WHEN @SortExpression = 'Unit' AND @SortDirection = 'ASC' THEN CP.[Unit] END ASC,
        CASE WHEN @SortExpression = 'Unit' AND @SortDirection = 'DESC' THEN CP.[Unit] END DESC
    OFFSET 
        ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Get total record count
    SELECT 
        COUNT(1) AS TotalRecords
    FROM 
        [CategoryProperty] CP WITH (NOLOCK)
    INNER JOIN 
        [Property] P WITH (NOLOCK) ON CP.[PropertyId] = P.[Id]
    INNER JOIN 
        [Category] C WITH (NOLOCK) ON CP.[CategoryId] = C.[Id]  -- Include Category table for total count
    WHERE 
        (@CategoryId IS NULL OR CP.[CategoryId] = @CategoryId) 
        AND (@PropertyId IS NULL OR CP.[PropertyId] = @PropertyId);
END;