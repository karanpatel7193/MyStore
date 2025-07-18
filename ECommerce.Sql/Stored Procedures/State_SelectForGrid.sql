CREATE PROCEDURE [dbo].[State_SelectForGrid]
	@Name varchar(50) = null, 
	@CountryId smallint = null, 
	@SortExpression varchar(50),
	@SortDirection varchar(5),
	@PageIndex int,
	@PageSize int
AS
/***********************************************************************************************
	 NAME     :  State_SelectForGrid
	 PURPOSE  :  This SP select records from table State for bind State page grid.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/15/2019					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	DECLARE @SqlQuery NVARCHAR(MAX)

	SET @SqlQuery = N'
	SELECT S.[Id], S.[Name], S.[SortName], S.[CountryId]
	FROM [State] S
	WHERE   S.[Name] LIKE ''%'' + COALESCE(@Name, S.[Name]) + ''%''
		AND S.[CountryId] = COALESCE(@CountryId, S.[CountryId])
	ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS
	FETCH NEXT @PageSize ROWS ONLY
	'

	EXEC sp_executesql @SqlQuery, N'@Name varchar(50), @CountryId smallint, @PageIndex int, @PageSize int', @Name, @CountryId, @PageIndex, @PageSize
	
	SELECT	COUNT(1) AS TotalRecords
	FROM	[State] S
	WHERE   S.[Name] LIKE '%' + COALESCE(@Name, S.[Name]) + '%'
		AND S.[CountryId] = COALESCE(@CountryId, S.[CountryId])

END