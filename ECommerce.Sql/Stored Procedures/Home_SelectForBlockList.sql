CREATE PROCEDURE Home_SelectForBlockList
AS
BEGIN
    SELECT 
				B.Id,
				B.Name,
				B.Description,
				B.Content
        
    FROM		dbo.[Block] B
    WHERE		B.IsActive = 1;

    SELECT 
				BP.BlockId,
				BP.ProductId,
				P.Name AS ProductName,
				PM.ThumbUrl,
				PI.FinalSellPrice,
				PI.DiscountPercentage,
				PI.DiscountAmount

    FROM		dbo.BlockProduct BP
    LEFT JOIN	dbo.Product P				ON BP.ProductId = P.Id
    LEFT JOIN	dbo.ProductMedia PM		ON P.Id = PM.ProductId 
    LEFT JOIN	dbo.ProductInventory PI	ON P.Id = PI.ProductId
    WHERE BP.BlockId IN (SELECT B.Id FROM dbo.Block B WHERE B.IsActive = 1);
END
