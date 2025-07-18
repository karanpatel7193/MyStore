CREATE TABLE [dbo].[ProductInventory] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [ProductId]             INT             NOT NULL,
    [OpeningQty]            INT             NOT NULL,
    [BuyQty]                INT             NOT NULL,
    [LockQty]               INT             NOT NULL,
    [OrderQty]              INT             NOT NULL,
    [SellQty]               INT             NOT NULL,
    [ClosingQty]            INT             NOT NULL,
    [CostPrice]             FLOAT           NOT NULL,
    [SellPrice]             FLOAT           NOT NULL,
    [DiscountPercentage]    FLOAT           NOT NULL,
    [DiscountAmount]        FLOAT           NOT NULL,
    [FinalSellPrice]        FLOAT           NOT NULL,
    CONSTRAINT [PK_ProductInventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductInventory_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

