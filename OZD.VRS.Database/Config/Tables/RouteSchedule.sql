﻿CREATE TABLE [Admin].[RouteSchedule]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[OperatorId] BIGINT NOT NULL,
	[FromDestinationId] BIGINT NOT NULL,
	[ToDestinationId] BIGINT NOT NULL,
	[Day] INT NOT NULL,
	[Time] TIME NOT NULL
)