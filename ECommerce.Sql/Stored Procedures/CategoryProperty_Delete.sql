CREATE PROCEDURE [dbo].[CategoryProperty_Delete]
    @Id INT
AS
BEGIN

    IF EXISTS (SELECT [Id] FROM [CategoryProperty] WHERE [Id] = @Id)
    BEGIN
        DELETE FROM [CategoryProperty]
        WHERE [Id] = @Id;

        SELECT 1; 
    END
    ELSE
    BEGIN
        SELECT 0; 
    END
END;