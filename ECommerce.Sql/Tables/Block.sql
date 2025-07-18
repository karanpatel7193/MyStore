CREATE TABLE [dbo].[Block]
(
	[Id]			INT             IDENTITY (1, 1) NOT NULL, 
    [Name]          NVARCHAR(100)   NOT NULL, 
    [Description]   NVARCHAR(200)   NULL, 
    [IsActive]      BIT             NOT NULL, 
    [Content]       NVARCHAR(MAX)   NULL, 
    CONSTRAINT [PK_Block] PRIMARY KEY CLUSTERED ([Id] ASC)
)
