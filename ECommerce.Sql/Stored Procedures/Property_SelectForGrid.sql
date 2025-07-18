CREATE PROCEDURE [dbo].[Property_SelectForGrid]
	@Name varchar(50) = NULL, 
	@SortExpression varchar(50),
	@SortDirection varchar(5),
	@PageIndex int,
	@PageSize int
AS
/***********************************************************************************************
	 NAME     :  Property_SelectForGrid
	 PURPOSE  :  This SP select records from table Property for bind Property page grid.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/03/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	DECLARE @SqlQuery NVARCHAR(MAX)

	SET @SqlQuery = N'
	SELECT R.[Id], R.[Name], R.[Description]
	FROM [Property] R
	WHERE  R.[Name] like COALESCE(@Name, R.[Name]) + ''%''
	ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS
	FETCH NEXT @PageSize ROWS ONLY
	'

	EXEC sp_executesql @SqlQuery, N'@Name varchar(50), @PageIndex int, @PageSize int', @Name, @PageIndex, @PageSize
	
	SELECT COUNT(1) AS TotalRecords
	FROM [Property] R
	WHERE  R.[Name] LIKE COALESCE(@Name, R.[Name]) + '%'

END

