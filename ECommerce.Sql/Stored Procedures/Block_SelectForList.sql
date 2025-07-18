CREATE PROCEDURE [dbo].[Block_SelectForList]
    @Name           NVARCHAR(50)    = NULL,
    @Description    NVARCHAR(500)   = NULL,
    @IsActive       BIT             = NULL,
    @SortExpression VARCHAR(50),
    @SortDirection  VARCHAR(5),
    @PageIndex      INT,
    @PageSize       INT
AS
BEGIN

    EXEC Block_SelectForGrid 
        @Name, 
        @Description, 
        @IsActive,    
        @SortExpression, 
        @SortDirection, 
        @PageIndex, 
        @PageSize
END
GO
