﻿CREATE TABLE [Route].[VehicleRoute]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[FromDestinationId] BIGINT NOT NULL,
	[ToDestinationId] BIGINT NOT NULL,
)
