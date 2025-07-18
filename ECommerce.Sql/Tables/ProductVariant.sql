CREATE TABLE [dbo].[ProductVariant] (
    [Id]                        INT                 IDENTITY(1,1)     NOT NULL,
    [ProductId]                 INT                                 NOT NULL,
    [VariantPropertyId]         INT                                 NOT NULL,
    [VariantPropertyValue]      VARCHAR(255)                        NOT NULL,

    CONSTRAINT [PK_ProductVariant]           PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductVariant_Product]   FOREIGN KEY ([ProductId])            REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_ProductVariant_Property]  FOREIGN KEY ([VariantPropertyId])    REFERENCES [dbo].[Property] ([Id])
);
