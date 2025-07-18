CREATE  PROCEDURE [dbo].[Wishlist_CheckIfProductInWishlist]
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- Fetch product details along with wishlist status
    SELECT 
        P.*, 
        CASE 
            WHEN EXISTS (SELECT 1 FROM dbo.[Wishlist] W WHERE P.Id = W.ProductId AND W.UserId = @UserId) 
            THEN 1 
            ELSE 0 
        END AS IsWishlisted
    FROM dbo.[Product] P
END;
