CREATE TABLE [dbo].[PurchaseInvoice] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [InvoiceNumber]    INT            NOT NULL,
    [CreatedBy]        BIGINT         NOT NULL,
    [CreatedOn]        DATETIME2 (7)  NOT NULL,
    [LastUpdatedBy]    INT            NULL,
    [LastUpdatedOn]    DATETIME       NULL,
    [VendorId]         INT            NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    [TotalQuantity]    FLOAT (53)     NOT NULL,
    [TotalAmount]      FLOAT (53)     NOT NULL,
    [TotalDiscount]    FLOAT (53)     NOT NULL,
    [TotalTax]         FLOAT (53)     NOT NULL,
    [TotalFinalAmount] FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_PurchaseInvoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseInvoice_Vender] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[Vendor] ([Id])
);


