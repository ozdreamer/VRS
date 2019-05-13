CREATE TABLE [Admin].[Destination]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [City] NVARCHAR(128) NOT NULL, 
    [State] NVARCHAR(128) NULL, 
    [PostCode] INT NULL,	
	[Active] BIT NOT NULL
)
