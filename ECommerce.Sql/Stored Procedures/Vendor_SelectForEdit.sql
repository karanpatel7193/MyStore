CREATE PROCEDURE [dbo].[Vendor_SelectForEdit]
	@Id bigint
AS
BEGIN
	DECLARE @CountryId SMALLINT
	SELECT	@CountryId = V.CountryId
	FROM	Vendor V
	WHERE	V.Id		= @Id

	EXEC Vendor_SelectForRecord @Id

	EXEC Vendor_SelectForAdd 

	EXEC State_SelectForLOV @CountryId

END
