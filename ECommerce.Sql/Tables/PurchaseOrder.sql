CREATE TABLE [dbo].[PurchaseOrder]
(
	[Id]                    INT IDENTITY (1, 1) NOT NULL, 
    [OrderNumber]           INT NOT NULL, 
    [CreatedBy]             BIGINT NOT NULL, 
    [CreatedOn]             DATETIME NOT NULL, 
    [LastUpdatedBy]         INT NULL, 
    [LastUpdatedOn]         DATETIME NULL,
    [VendorId]              INT NULL, 
    [Description]            NVARCHAR(MAX) NULL, 
    [TotalQuantity]         FLOAT NULL, 
    [TotalAmount]           FLOAT NULL, 
    [TotalDiscount]         FLOAT NULL, 
    [TotalTax]              FLOAT NULL, 
    [TotalFinalAmount]      FLOAT NULL,
    CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrder_Vender] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[Vendor] ([Id]),
)
