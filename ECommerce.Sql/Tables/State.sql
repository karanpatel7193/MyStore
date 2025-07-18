CREATE TABLE [dbo].[State] (
    [Id]       INT           IDENTITY(1, 1) NOT NULL,
    [Name]     VARCHAR(50)   NOT NULL,
    [SortName] VARCHAR(10)   NULL,
    [CountryId] SMALLINT     NOT NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_State_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id])
);
