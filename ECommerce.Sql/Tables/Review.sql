CREATE TABLE [dbo].[Review] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserId]    BIGINT         NOT NULL,
    [ProductId] INT            NOT NULL,
    [Rating]    DECIMAL (2, 1) NOT NULL,
    [Comments]  NVARCHAR (MAX) NULL,
    [Date]      DATETIME       CONSTRAINT [DF_Review_Date] DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



