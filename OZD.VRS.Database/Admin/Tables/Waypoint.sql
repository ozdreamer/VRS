﻿CREATE TABLE [Admin].[Waypoint]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(128) NOT NULL,
	[City] NVARCHAR(128) NULL,
	[Active] BIT NOT NULL
)