create PROCEDURE [dbo].[Home_selectForSlider]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Name, 
        Content
    FROM 
        Block
    WHERE 
        IsActive = 0;
END;