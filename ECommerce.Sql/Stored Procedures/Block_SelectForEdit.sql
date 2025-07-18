CREATE PROCEDURE [dbo].[Block_SelectForEdit]
	@Id int
AS
/***********************************************************************************************
	 NAME     :  Block_SelectForEdit
	 PURPOSE  :  This SP use for fill all LOV in Block page for edit mode
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        03/10/2018					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
    EXEC [dbo].[Block_SelectForRecord] @Id;

    SELECT			PM.Id, PM.[BlockId], PM.[ProductId], P.[Name] AS ProductName
    FROM			[dbo].[BlockProduct] PM
	INNER JOIN		[dbo].[Product] P
			ON		P.[Id] = PM.[ProductId]
    WHERE   PM.BlockId = @Id;

    EXEC [dbo].[Block_SelectForAdd];

END

