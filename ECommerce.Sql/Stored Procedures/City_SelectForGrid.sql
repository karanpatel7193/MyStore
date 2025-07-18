CREATE PROCEDURE [dbo].[City_SelectForGrid]
	@Name varchar(50) = null, 
	@StateId int = null, 
	@SortExpression varchar(50),
	@SortDirection varchar(5),
	@PageIndex int,
	@PageSize int
AS
/***********************************************************************************************
	 NAME     :  City_SelectForGrid
	 PURPOSE  :  This SP select records from table City for bind City page grid.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	DECLARE @SqlQuery NVARCHAR(MAX)

	SET @SqlQuery = N'
	SELECT C.[Id], C.[Name], C.[StateId]
	FROM [City] C
	WHERE	C.[Name] LIKE ''%'' + COALESCE(@Name, C.[Name]) + ''%''
	 AND C.[StateId] = COALESCE(@StateId, C.[StateId])
	ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS
	FETCH NEXT @PageSize ROWS ONLY
	'

	EXEC sp_executesql @SqlQuery, N'@Name varchar(50), @StateId int, @PageIndex int, @PageSize int', @Name, @StateId, @PageIndex, @PageSize
	
	SELECT	COUNT(1) AS TotalRecords
	FROM	[City] C
	WHERE	C.[Name] LIKE '%' + COALESCE(@Name, C.[Name]) + '%'
		AND C.[StateId] = COALESCE(@StateId, C.[StateId])

END