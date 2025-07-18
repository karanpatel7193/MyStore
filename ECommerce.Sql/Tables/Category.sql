CREATE TABLE [dbo].[Category] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (500) NULL,
    [ImageUrl]    VARCHAR (500) NULL,
    [ParentId]    INT           NULL,
    [IsVisible]   BIT           NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Category_Category] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Category] ([Id])
);



