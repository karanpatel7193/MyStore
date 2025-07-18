CREATE PROCEDURE [dbo].[Vendor_SelectForLOV]
AS
BEGIN
	SELECT V.[Id], V.[Name], V.[Email], V.[Phone]
	FROM [Vendor] V WITH (NOLOCK)
END
GO
