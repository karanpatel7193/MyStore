CREATE PROCEDURE [dbo].[Address_Delete]
    @Id     INT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- Ensure the address exists for the given UserId
    IF EXISTS (SELECT 1 FROM [dbo].[Address] WHERE Id = @Id AND UserId = @UserId)
    BEGIN
        DELETE FROM [dbo].[Address] 
        WHERE Id = @Id AND UserId = @UserId;

        SELECT 1 AS Deleted;  -- Success
    END
    ELSE
    BEGIN
        SELECT 0 AS Deleted;  -- Address not found
    END
END;
