CREATE TABLE [dbo].[Country] (
    [Id]             SMALLINT      IDENTITY(1, 1) NOT NULL,
    [Name]           VARCHAR(50)   NOT NULL,
    [SortName]       VARCHAR(10)   NOT NULL,
    [CurrencySign]   NVARCHAR(1)   NOT NULL,
    [CurrencyCode]   NVARCHAR(3)   NOT NULL,
    [CurrencyName]   VARCHAR(20)   NOT NULL,
    [FlagImagePath]  VARCHAR(500)  NOT NULL,
    [PhoneCode]      SMALLINT      NULL,

 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([Id] ASC)
)
