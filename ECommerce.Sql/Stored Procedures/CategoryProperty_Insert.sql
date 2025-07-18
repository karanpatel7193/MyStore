CREATE PROCEDURE [dbo].[CategoryProperty_Insert]
    @CategoryId INT,
    @PropertyId INT,
    @Unit       VARCHAR(50)
AS
BEGIN
    DECLARE @Id INT;

    IF NOT EXISTS (
        SELECT [Id] 
        FROM [CategoryProperty] 
        WHERE [CategoryId] = @CategoryId 
          AND [PropertyId] = @PropertyId
    )
    BEGIN
        INSERT INTO [CategoryProperty] ([CategoryId], [PropertyId], [Unit])
        VALUES (@CategoryId, @PropertyId, @Unit);

        SET @Id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SET @Id = 0; -- Record already exists with the same details
    END

    SELECT @Id;
END;