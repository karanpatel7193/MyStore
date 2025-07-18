CREATE PROCEDURE [dbo].[Country_SelectForGrid]
	@Name varchar(50) = NULL, 
	@SortName varchar(10) = NULL, 
	@SortExpression varchar(50),
	@SortDirection varchar(5),
	@PageIndex int,
	@PageSize int
AS
/***********************************************************************************************
	 NAME     :  Country_SelectForGrid
	 PURPOSE  :  This SP select records from table Country for bind Country page grid.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	DECLARE @SqlQuery NVARCHAR(MAX)

	SET @SqlQuery = N'
	SELECT C.[Id], C.[Name], C.[SortName], C.[CurrencySign], C.[CurrencyCode], C.[CurrencyName], C.[FlagImagePath]
	FROM [Country] C
	WHERE  C.[Name] LIKE COALESCE(@Name, C.[Name]) + ''%''
	   AND C.[SortName] LIKE COALESCE(@SortName, C.[SortName]) + ''%''
	ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS
	FETCH NEXT @PageSize ROWS ONLY
	'

	EXEC sp_executesql @SqlQuery, N'@Name varchar(50), @SortName varchar(10), @PageIndex int, @PageSize int', @Name, @SortName, @PageIndex, @PageSize
	
	SELECT COUNT(1) AS TotalRecords
	FROM [Country] C
	WHERE  C.[Name] LIKE COALESCE(@Name, C.[Name]) + '%'
	   AND C.[SortName] LIKE COALESCE(@SortName, C.[SortName]) + '%'

END