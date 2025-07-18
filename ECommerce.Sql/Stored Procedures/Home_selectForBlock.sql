CREATE PROCEDURE Home_selectForBlock
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Name, 
        Content
    FROM 
        Block
    WHERE 
        IsActive = 1;
END;