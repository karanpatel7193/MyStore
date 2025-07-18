CREATE TABLE [dbo].[ReviewMedia] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ReviewId]  INT            NOT NULL,
    [MediaType] NVARCHAR (250) NULL,
    [MediaURL]  NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ReviewId]) REFERENCES [dbo].[Review] ([Id]) ON DELETE CASCADE
);

