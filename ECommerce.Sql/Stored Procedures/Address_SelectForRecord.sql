CREATE PROCEDURE [dbo].[Address_SelectForRecord]
    @Id INT
AS
/***********************************************************************************************
	 NAME     :  Address_SelectForRecord
	 PURPOSE  :  This SP selects a particular address record for viewing or editing.
	 REVISIONS:
	 Ver        Date            Author             Description
	 ---------  ----------      ---------------    -------------------------------
	 1.0        31/03/2025      Kirtikumar Raval   1. Initial Version.	 
************************************************************************************************/
BEGIN
    SET NOCOUNT ON;

    SELECT 
        A.[Id], 
        A.[UserId], 
        A.[FullName], 
        A.[MobileNumber], 
        A.[AlternateNumber], 
        A.[AddressLine], 
        A.[Landmark], 
        A.[CityId], 
        A.[StateId], 
        A.[PinCode], 
        A.[AddressType], 
        A.[IsDefault]
    FROM [dbo].[Address] A WITH (NOLOCK)
    WHERE A.[Id] = @Id;
END;
