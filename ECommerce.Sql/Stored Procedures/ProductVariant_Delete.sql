CREATE PROCEDURE [dbo].[ProductVariant_Delete]
    @Id INT
AS
BEGIN
    DELETE FROM [ProductVariant]
    WHERE [Id] = @Id;
END
GO
