CREATE TABLE [dbo].[PurchaseOrderItem]
(
	[Id]                    INT   IDENTITY (1, 1) NOT NULL,
	[PurchaseOrderId]		INT   NOT NULL, 
	[ProductId]		        INT   NOT NULL, 
	[ProductName]		    NVARCHAR(MAX)   NOT NULL, 
    [Quantity]              INT   NOT NULL, 
    [Price]                 FLOAT NOT NULL, 
    [Amount]                FLOAT NOT NULL, 
    [DiscountPercentage]    FLOAT NULL, 
    [DiscountedAmount]      FLOAT NULL, 
    [Tax]                   FLOAT NULL, 
    [FinalAmount]           FLOAT NOT NULL, 
    [ExpiryDate]            DATETIME NULL,
    CONSTRAINT [PK_PurchaseOrderItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrderItem_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_PurchaseOrderItem_PurchaseOrder] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [dbo].[PurchaseOrder] ([Id])

);
