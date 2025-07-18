CREATE TABLE [dbo].[ProductMedia] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ProductId]         INT             NOT NULL,
    [Type]              INT             NOT NULL,
    [Url]               VARCHAR (500)   NOT NULL,
    [ThumbUrl]          VARCHAR (500)   NOT NULL,
    [Description]       VARCHAR(500)    NULL, 
    CONSTRAINT [PK_ProductMedia] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductMedia_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_ProductMedia_MasterValue] FOREIGN KEY ([Type]) REFERENCES [dbo].[MasterValue] ([Value])
);

