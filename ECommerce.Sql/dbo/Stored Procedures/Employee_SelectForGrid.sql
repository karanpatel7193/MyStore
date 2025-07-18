
CREATE PROCEDURE [dbo].[Employee_SelectForGrid]
	@StateId int = NULL, 	@CountryId smallint = NULL, 
	@SortExpression varchar(50),
	@SortDirection varchar(5),
	@PageIndex int,
	@PageSize int
AS
/***********************************************************************************************
	 NAME     :  Employee_SelectForGrid
	 PURPOSE  :  This SP select records from table Employee for bind Employee page grid.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        06/18/2025					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	DECLARE @SqlQuery NVARCHAR(MAX)

	SET @SqlQuery = N'
	SELECT E.[Id], E.[FirstName], E.[MiddleName], E.[LastName], E.[Gender], E.[Email], E.[PhoneNumber], E.[DOB], E.[DateOfJoin], E.[Education], E.[CityId], E.[StateId], E.[CountryId]
	FROM [Employee] E
	WHERE   (@StateId IS NULL OR E.[StateId] = @StateId)		AND (@CountryId IS NULL OR E.[CountryId] = @CountryId)
	ORDER BY ' + @SortExpression + ' ' + @SortDirection + '
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS
	FETCH NEXT @PageSize ROWS ONLY
	'

	EXEC sp_executesql @SqlQuery, N'@StateId int, 	@CountryId smallint , @PageIndex int, @PageSize int', @StateId, @CountryId, @PageIndex, @PageSize
	
	SELECT COUNT(1) AS TotalRecords
	FROM [Employee] E
	WHERE   (@StateId IS NULL OR  E.[StateId] = @StateId)	AND		(@CountryId IS NULL OR E.[CountryId] = @CountryId)

END