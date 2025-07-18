CREATE TABLE [dbo].[CategoryPropertyValue] (
    [CategoryId] INT           NOT NULL,
    [PropertyId] INT           NOT NULL,
    [Value]      VARCHAR (500) NOT NULL,
    [Unit]       VARCHAR (50)  NOT NULL,
    CONSTRAINT [FK_CategoryPropertyValue_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    CONSTRAINT [FK_CategoryPropertyValue_Property] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id])
);


