CREATE TABLE [dbo].[Address] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [UserId]          BIGINT         NOT NULL,
    [FullName]        NVARCHAR (255) NOT NULL,
    [MobileNumber]    VARCHAR (15)   NOT NULL,
    [AlternateNumber] VARCHAR (15)   NULL,
    [AddressLine]     NVARCHAR (500) NOT NULL,
    [Landmark]        NVARCHAR (255) NULL,
    [CityId]          INT            NOT NULL,
    [StateId]         INT            NOT NULL,
    [PinCode]         VARCHAR (10)   NOT NULL,
    [AddressType]     VARCHAR (50)   NOT NULL,
    [IsDefault]       BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Address_City] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]),
    CONSTRAINT [FK_Address_State] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]),
    CONSTRAINT [FK_Address_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);


