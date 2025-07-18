CREATE PROCEDURE [dbo].[Address_SelectForUser]
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT A.[Id], A.[UserId], A.[FullName], A.[MobileNumber], A.[AlternateNumber], 
           A.[AddressLine], A.[Landmark], A.[CityId], A.[StateId], 
           A.[PinCode], A.[AddressType], A.[IsDefault],
           S.[Name] AS StateName,  -- State name from the State table
           C.[Name] AS CityName    -- City name from the City table
    FROM [dbo].[Address] A WITH (NOLOCK)
    INNER JOIN [dbo].[State] S ON A.StateId = S.Id   -- Joining State table
    INNER JOIN [dbo].[City] C ON A.CityId = C.Id     -- Joining City table
    WHERE A.[UserId] = @UserId;
END;
