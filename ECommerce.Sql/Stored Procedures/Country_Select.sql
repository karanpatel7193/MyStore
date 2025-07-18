CREATE PROCEDURE [dbo].[Country_Select]
	@Id smallint = NULL, 
	@Name varchar(20) = NULL, 
	@SortName varchar(10) = NULL, 
	@CurrencySign nvarchar(1) = NULL, 
	@CurrencyCode nvarchar(3) = NULL, 
	@CurrencyName varchar(20) = NULL, 
	@FlagImagePath varchar(50) = NULL 
AS
/***********************************************************************************************
	 NAME     :  Country_Select
	 PURPOSE  :  This SP select records from table Country
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        02/07/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
	SELECT C.[Id], C.[Name], C.[SortName], C.[CurrencySign], C.[CurrencyCode], C.[CurrencyName], C.[FlagImagePath] 
	FROM [Country] C
	WHERE	 C.[Id] = COALESCE(@Id, C.[Id])
		 AND C.[Name] = COALESCE(@Name, C.[Name])
		 AND C.[SortName] = COALESCE(@SortName, C.[SortName])
		 AND C.[CurrencySign] = COALESCE(@CurrencySign, C.[CurrencySign])
		 AND C.[CurrencyCode] = COALESCE(@CurrencyCode, C.[CurrencyCode])
		 AND C.[CurrencyName] = COALESCE(@CurrencyName, C.[CurrencyName])
		 AND C.[FlagImagePath] = COALESCE(@FlagImagePath, C.[FlagImagePath]) 
END