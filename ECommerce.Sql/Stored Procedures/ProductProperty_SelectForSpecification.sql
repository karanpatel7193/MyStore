CREATE PROCEDURE [dbo].[ProductProperty_SelectForSpecification]
    @Id INT
AS
BEGIN
    SELECT
        P.[Name] AS PropertyName,
        CP.[Unit],
        PP.[Value]
    FROM 
        ProductProperty PP
        INNER JOIN CategoryProperty CP ON CP.[PropertyId] = PP.[PropertyId]
        INNER JOIN Property P ON CP.[PropertyId] = P.[Id]
        INNER JOIN Product PR ON PR.[Id] = PP.[ProductId] AND PR.[CategoryId] = CP.[CategoryId]
    WHERE 
        PP.[ProductId] = @Id;
END;