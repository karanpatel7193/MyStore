CREATE TABLE [dbo].[Product] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (200)  NOT NULL,
    [Description]       VARCHAR (500)  NULL,
    [LongDescription]   VARCHAR (5000) NULL,
    [CategoryId]        INT            NULL,
    [AllowReturn]       BIT            NOT NULL,
    [ReturnPolicy]      VARCHAR (500)  NULL,
    [IsExpiry]          BIT            NOT NULL,
    [CreatedBy]         BIGINT         NOT NULL,
    [CreatedOn]         DATETIME       NOT NULL,
    [LastUpdatedBy]     BIGINT         NULL,
    [LastUpdatedOn]     DATETIME       NULL,
    [SKU]               VARCHAR (20)   NOT NULL,
    [UPC]               INT            NOT NULL,
    [ParentProductId]   INT            NULL,
    [ProductVariantIds] VARCHAR (300)  NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);















