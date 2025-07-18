CREATE TABLE [dbo].[CartDiscountCoupon] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [CartId]           INT          NOT NULL,
    [CouponDiscountId] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CartDiscountCoupons] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CartDiscountCoupons_Cart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Cart] ([Id])
);


