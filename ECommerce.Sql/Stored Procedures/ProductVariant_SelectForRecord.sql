CREATE PROCEDURE [dbo].[ProductVariant_SelectForRecord]
    @Id INT
AS
BEGIN
    SELECT 
        PV.[Id],
        PV.[ProductId],
        PV.[VariantPropertyId],
        P.[Name] AS VariantPropertyName,
        PV.[VariantPropertyValue]
    FROM [ProductVariant] PV WITH (NOLOCK)
    INNER JOIN [Property] P ON PV.[VariantPropertyId] = P.[Id]
    WHERE PV.[Id] = @Id;
END
GO
