CREATE TABLE [dbo].[PurchaseInvoiceItem] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [PurchaseInvoiceId]  INT            NOT NULL,
    [ProductId]          INT            NOT NULL,
    [ProductName]        NVARCHAR (MAX) NOT NULL,
    [Quantity]           INT            NOT NULL,
    [Price]              FLOAT (53)     NOT NULL,
    [Amount]             FLOAT (53)     NOT NULL,
    [DiscountPercentage] FLOAT (53)     NULL,
    [DiscountedAmount]   FLOAT (53)     NULL,
    [Tax]                FLOAT (53)     NULL,
    [FinalAmount]        FLOAT (53)     NOT NULL,
    [ExpiryDate]         DATETIME       NULL,
    [IsExpiry]           BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PurchaseInvoiceItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseInvoiceItem_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_PurchaseInvoiceItem_PurchaseInvoice] FOREIGN KEY ([PurchaseInvoiceId]) REFERENCES [dbo].[PurchaseInvoice] ([Id])
);

