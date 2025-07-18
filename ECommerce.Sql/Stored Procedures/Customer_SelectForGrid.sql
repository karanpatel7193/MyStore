CREATE PROCEDURE [dbo].[Customer_SelectForGrid]
    @Name           VARCHAR(50)     = NULL, 
    @Status         INT             = NULL, 
    @UserId         BIGINT, -- UserId is mandatory
    @SortExpression VARCHAR(50),
    @SortDirection  VARCHAR(5),
    @PageIndex      INT,
    @PageSize       INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Get paginated data with customer details, status description, and username
    SELECT 
        C.[Id], 
        C.[Name], 
        C.[TotalBuy], 
        C.[TotalInvoices], 
        MV.[Value] AS [Status],  
        U.[Username] AS [UserName],  
        C.[UserId] -- Include UserId in the output
    FROM 
        [dbo].[Customer] C WITH (NOLOCK)
    INNER JOIN 
        [dbo].[MasterValue] MV WITH (NOLOCK) ON C.[Status] = MV.[Value]
    INNER JOIN 
        [dbo].[User] U WITH (NOLOCK) ON C.[UserId] = U.[Id]
    WHERE 
        (@Name IS NULL OR C.[Name] LIKE '%' + @Name + '%') 
        AND (@Status IS NULL OR C.[Status] = @Status)
        AND C.[UserId] = @UserId -- Match the specific UserId
    ORDER BY 
        CASE WHEN @SortExpression = 'Name' AND @SortDirection = 'ASC' THEN C.[Name] END ASC,
        CASE WHEN @SortExpression = 'Name' AND @SortDirection = 'DESC' THEN C.[Name] END DESC,
        CASE WHEN @SortExpression = 'TotalBuy' AND @SortDirection = 'ASC' THEN C.[TotalBuy] END ASC,
        CASE WHEN @SortExpression = 'TotalBuy' AND @SortDirection = 'DESC' THEN C.[TotalBuy] END DESC
    OFFSET 
        ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Get total record count
    SELECT 
        COUNT(1) AS TotalRecords
    FROM 
        [dbo].[Customer] C WITH (NOLOCK)
    INNER JOIN 
        [dbo].[MasterValue] MV WITH (NOLOCK) ON C.[Status] = MV.[Value]
    INNER JOIN 
        [dbo].[User] U WITH (NOLOCK) ON C.[UserId] = U.[Id]
    WHERE 
        (@Name IS NULL OR C.[Name] LIKE '%' + @Name + '%') 
        AND (@Status IS NULL OR C.[Status] = @Status)
        AND C.[UserId] = @UserId; -- Match the specific UserId
END;