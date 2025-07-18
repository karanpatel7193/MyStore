CREATE PROCEDURE [dbo].[Product_SelectForList]
    @Name               NVARCHAR(100)    = NULL, 
    @Description        NVARCHAR(500)    = NULL, 
    @CategoryId         INT              = NULL, 
    @SortExpression     VARCHAR(50),
    @SortDirection      VARCHAR(5),
    @PageIndex          INT,
    @PageSize           INT
AS

BEGIN
   EXEC Category_SelectForLOV

    EXEC Product_SelectForGrid 
        @Name, 
        @Description, 
        @CategoryId, 
        @SortExpression, 
        @SortDirection, 
        @PageIndex, 
        @PageSize 
END
