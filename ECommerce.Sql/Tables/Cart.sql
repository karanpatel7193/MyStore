CREATE TABLE [dbo].[Cart] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [UserId]     BIGINT          NOT NULL,
    [ProductId]  INT             NOT NULL,
    [Quantity]   INT             NOT NULL,
    [Price]      DECIMAL (18, 2) NOT NULL,
    [OfferPrice] DECIMAL (18, 2) NOT NULL,
    [IsActive]   BIT             NOT NULL,
    [AddedDate]  DATE            NOT NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cart_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_Cart_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);


