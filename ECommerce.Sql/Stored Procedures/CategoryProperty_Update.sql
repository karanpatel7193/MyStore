CREATE PROCEDURE [dbo].[CategoryProperty_Update]
    @Id         INT,
    @CategoryId INT,
    @PropertyId INT,
    @Unit       VARCHAR(50)
AS
BEGIN
    IF EXISTS (SELECT [Id] FROM [CategoryProperty] WHERE [Id] = @Id)
    BEGIN
        UPDATE [CategoryProperty]
        SET [CategoryId] = @CategoryId,
            [PropertyId] = @PropertyId,
            [Unit] = @Unit
        WHERE [Id] = @Id;
        
        SELECT 1; -- Success
    END
    ELSE
    BEGIN
        SELECT 0; -- Failure: Record not found
    END
END;