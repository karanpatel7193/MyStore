CREATE TABLE [dbo].[Employee] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50)  NOT NULL,
    [MiddleName]  NVARCHAR (50)  NULL,
    [LastName]    NVARCHAR (50)  NOT NULL,
    [Gender]      NVARCHAR (10)  NOT NULL,
    [Email]       NVARCHAR (100) NOT NULL,
    [PhoneNumber] NVARCHAR (20)  NULL,
    [DOB]         DATE           NOT NULL,
    [DateOfJoin]  DATE           NOT NULL,
    [Education]   NVARCHAR (50)  NULL,
    [CityId]      INT            NULL,
    [StateId]     INT            NULL,
    [CountryId]   SMALLINT       NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_City] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]),
    CONSTRAINT [FK_Employee_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]),
    CONSTRAINT [FK_Employee_State] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id])
);

