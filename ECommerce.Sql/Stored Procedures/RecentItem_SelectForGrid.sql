CREATE PROCEDURE [dbo].[RecentItem_SelectForGrid]
    @UserId BIGINT = NULL, 
    @ProductId INT = NULL, 
    @SortExpression VARCHAR(50),
    @SortDirection VARCHAR(5),
    @PageIndex INT,
    @PageSize INT
AS
/***********************************************************************************************
     NAME     :  RecentItem_SelectForGrid
     PURPOSE  :  This SP selects records from the RecentItem table for binding to the RecentItem page grid.
     REVISIONS:
     Ver        Date                      Author               Description
     ---------  ----------                 ---------------      -------------------------------
     1.0        21/01/2025                 [Your Name]           1. Initial Version.    
************************************************************************************************/
BEGIN
    DECLARE @SqlQuery NVARCHAR(MAX)

    SET @SqlQuery = N'
    SELECT		
				RI.[Id], 
				RI.[ProductId], 
				P.[Name] AS ProductName,  
				RI.[UserId],
				U.[FirstName] AS UserName  
    FROM [dbo].[RecentItem] RI
    INNER JOIN [dbo].[Product] P ON RI.[ProductId] = P.[Id]
    INNER JOIN [dbo].[User] U ON RI.[UserId] = U.[Id]
    WHERE (@UserId IS NULL OR RI.[UserId] = @UserId)
      AND (@ProductId IS NULL OR RI.[ProductId] = @ProductId)
    ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
    OFFSET ((@PageIndex - 1) * @PageSize) ROWS
    FETCH NEXT @PageSize ROWS ONLY
    '

    EXEC sp_executesql @SqlQuery, N'@UserId BIGINT, @ProductId INT, @PageIndex INT, @PageSize INT', 
                        @UserId, @ProductId, @PageIndex, @PageSize

    SELECT COUNT(1) AS TotalRecords
    FROM [dbo].[RecentItem] RI
    INNER JOIN [dbo].[Product] P ON RI.[ProductId] = P.[Id]
    INNER JOIN [dbo].[User] U ON RI.[UserId] = U.[Id]
    WHERE (@UserId IS NULL OR RI.[UserId] = @UserId)
      AND (@ProductId IS NULL OR RI.[ProductId] = @ProductId)

END
