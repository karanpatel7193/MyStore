CREATE PROCEDURE [dbo].[ProductVariant_SelectForGrid]
    @ProductId INT
AS
BEGIN
    SELECT 
            PV.[Id],
            PV.[ProductId],
            PV.[VariantPropertyId],
            P.[Name] AS VariantPropertyName,
            PV.[VariantPropertyValue]
    FROM            [ProductVariant] PV WITH (NOLOCK)
        INNER JOIN  [Property] P        ON  PV.[VariantPropertyId] = P.[Id]
                                        AND PV.[ProductId] = @ProductId;
END
GO
