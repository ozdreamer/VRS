﻿CREATE TABLE [Admin].[VehicleSchedule]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[OperatorId] BIGINT NOT NULL,
	[RouteScheduleId] BIGINT NOT NULL,
	[VehicleId] BIGINT NOT NULL,
	[Date] DATE NOT NULL
)
