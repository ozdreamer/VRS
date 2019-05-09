﻿CREATE TABLE [Route].[RouteSchedule]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[RouteId] BIGINT NOT NULL,
	[Day] NVARCHAR(3) NOT NULL,
	[Time] TIME NOT NULL
)