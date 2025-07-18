CREATE PROCEDURE [dbo].[Category_Insert]
    @Name           NVARCHAR(255),
    @ParentId       INT = NULL,
    @Description    VARCHAR(MAX) = NULL,
    @ImageUrl       NVARCHAR(MAX) = NULL,
    @IsVisible      BIT 
AS
BEGIN
    DECLARE @Id INT;

    IF NOT EXISTS (SELECT [Id] FROM [Category] WHERE [Name] = @Name)
    BEGIN
         INSERT INTO [Category] ([Name], [ParentId], [Description], [ImageUrl], [IsVisible])
              VALUES (@Name, @ParentId, @Description, @ImageUrl, @IsVisible);

        SET @Id = SCOPE_IDENTITY();

        IF(@ImageUrl IS NOT NULL)
            BEGIN
                SET @ImageUrl = @ImageUrl + CAST(@Id AS NVARCHAR(10)) + '.jpg';

                UPDATE  [Category]
                SET     [ImageUrl]  = @ImageUrl
                WHERE   [Id]        = @Id;
            END
            
    END
    ELSE
        SET @Id = 0;
    
    SELECT @Id;
END
