CREATE TABLE [dbo].[BlockProduct]
(
	[Id]			INT             IDENTITY (1, 1) NOT NULL, 
	[BlockId]		INT             NOT NULL, 
	[ProductId]		INT             NULL, 
    CONSTRAINT [PK_BlockProduct] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BlockProduct_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_BlockProduct_Block] FOREIGN KEY ([BlockId]) REFERENCES [dbo].[Block] ([Id])
)
