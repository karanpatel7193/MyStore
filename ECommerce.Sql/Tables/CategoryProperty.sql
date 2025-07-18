CREATE TABLE [dbo].[CategoryProperty] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [CategoryId]  INT           NOT NULL,
    [PropertyId]  INT           NOT NULL,
    [Unit]        VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_CategoryProperty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CategoryProperty_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    CONSTRAINT [FK_CategoryProperty_Property] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id])
);