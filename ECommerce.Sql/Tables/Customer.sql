CREATE TABLE [dbo].[Customer] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50)  NOT NULL,
    [TotalBuy]      INT           NULL,
    [TotalInvoices] VARCHAR (MAX) NULL,
    [Status]        INT           NOT NULL,
    [UserId]        BIGINT        NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customer_MasterValue] FOREIGN KEY ([Status]) REFERENCES [dbo].[MasterValue] ([Value]),
    CONSTRAINT [FK_Customer_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);


