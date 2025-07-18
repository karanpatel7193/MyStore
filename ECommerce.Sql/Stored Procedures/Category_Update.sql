CREATE PROCEDURE [dbo].[Category_Update]
	@Id				INT,
	@Name			NVARCHAR(255),
	@ParentId		INT = NULL,
	@ImageUrl		NVARCHAR(255) = NULL,
	@Description	VARCHAR(255) = NULL,
	@IsVisible		BIT

AS
BEGIN
    IF NOT EXISTS(SELECT [Id] FROM [Category] WHERE [Name] = @Name AND [Id] != @Id)
    BEGIN
        UPDATE [Category]
        SET [Name] = @Name,
            [ParentId] = @ParentId,
            [ImageUrl] = @ImageUrl,
            [Description] = @Description,
            [IsVisible] = @IsVisible

        WHERE [Id] = @Id;

        IF(@ImageUrl IS NOT NULL)
        BEGIN
            SET @ImageUrl = @ImageUrl + CAST(@Id AS NVARCHAR(10)) + '.jpg';

            UPDATE [Category]
            SET [ImageUrl] = @ImageUrl
            WHERE [Id] = @Id;
        END
    END
    ELSE
        SET @Id = 0;

    SELECT @Id;
END
