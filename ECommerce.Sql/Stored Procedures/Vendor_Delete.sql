CREATE PROCEDURE [dbo].[Vendor_Delete]
	@Id		BIGINT
AS
	BEGIN 
		DELETE FROM [Vendor]
		WHERE [Id] = @Id;
	END
