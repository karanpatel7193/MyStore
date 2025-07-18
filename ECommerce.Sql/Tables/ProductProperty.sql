CREATE TABLE [dbo].[ProductProperty] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [ProductId]     INT           NOT NULL,
    [PropertyId]    INT           NOT NULL,
    [Value]         VARCHAR (500)  NOT NULL,
    [Unit]          VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_ProductProperty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductProperty_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_ProductProperty_Property] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id])
);