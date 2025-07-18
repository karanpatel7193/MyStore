CREATE TABLE [dbo].[City] (
    [Id]       INT           IDENTITY(1, 1) NOT NULL,
    [Name]     VARCHAR(50)   NOT NULL,
    [StateId]  INT           NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_City_State] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id])
);
