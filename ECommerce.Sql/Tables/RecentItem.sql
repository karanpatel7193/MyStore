CREATE TABLE [dbo].[RecentItem]
(
	[Id]        INT IDENTITY (1, 1) NOT NULL, 
    [ProductId] INT NOT NULL, 
    [UserId]    BIGINT NOT NULL 
    CONSTRAINT [PK_RecentItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RecentItems_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_RecentItems_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);
