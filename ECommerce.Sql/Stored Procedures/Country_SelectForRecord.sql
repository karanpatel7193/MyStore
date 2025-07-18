CREATE PROCEDURE [dbo].[Country_SelectForRecord]
	@Id smallint
AS
/***********************************************************************************************
	 NAME     :  Country_SelectForRecord
	 PURPOSE  :  This SP select perticular record from table Country for open page in edit / view mode.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT C.[Id], C.[Name], C.[SortName], C.[CurrencySign], C.[CurrencyCode], C.[CurrencyName], C.[FlagImagePath]
	FROM [Country] C
	WHERE C.[Id] = @Id;
END