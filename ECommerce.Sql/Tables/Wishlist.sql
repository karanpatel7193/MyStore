CREATE TABLE [dbo].[Wishlist] (
    [Id]         INT IDENTITY(1,1) PRIMARY KEY,
    [UserId]     BIGINT NOT NULL,  
    [ProductId]  INT NOT NULL,
    [CreatedTime]  DATETIME DEFAULT GETDATE() NOT NULL,
    
    CONSTRAINT [FK_Wishlist_User]       FOREIGN KEY ([UserId])      REFERENCES [dbo].[User]([Id]),
    CONSTRAINT [FK_Wishlist_Product]    FOREIGN KEY ([ProductId])   REFERENCES [dbo].[Product]([Id])
);
