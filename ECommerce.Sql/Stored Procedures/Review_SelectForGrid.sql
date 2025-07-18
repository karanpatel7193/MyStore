CREATE PROCEDURE [dbo].[Review_SelectForGrid]
    @UserId    BIGINT = NULL,
    @ProductId INT = NULL,
    @PageIndex INT,
    @PageSize  INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Fetch paginated reviews with Date
    SELECT 
        R.[Id], 
        R.[UserId], 
        R.[ProductId], 
        R.[Rating], 
        R.[Comments],
        R.[Date]   -- ✅ Added Date
    FROM [dbo].[Review] R WITH (NOLOCK)
    WHERE 
        (@UserId IS NULL OR R.[UserId] = @UserId)
    AND 
        (@ProductId IS NULL OR R.[ProductId] = @ProductId)
    ORDER BY R.[Id] DESC
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Fetch media list for the selected reviews
    SELECT 
        RM.[Id], 
        RM.[ReviewId], 
        RM.[MediaType], 
        RM.[MediaURL]
    FROM [dbo].[ReviewMedia] RM WITH (NOLOCK)
    WHERE EXISTS (
        SELECT 1 
        FROM [dbo].[Review] R
        WHERE RM.[ReviewId] = R.[Id]
        AND (@UserId IS NULL OR R.[UserId] = @UserId)
        AND (@ProductId IS NULL OR R.[ProductId] = @ProductId)
    );

    -- Get total number of records
    SELECT COUNT(1) AS TotalRecords
    FROM [dbo].[Review] R WITH (NOLOCK)
    WHERE 
        (@UserId IS NULL OR R.[UserId] = @UserId)
    AND 
        (@ProductId IS NULL OR R.[ProductId] = @ProductId);
END;