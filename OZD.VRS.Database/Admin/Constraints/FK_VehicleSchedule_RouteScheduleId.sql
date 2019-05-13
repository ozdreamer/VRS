ALTER TABLE [Admin].[VehicleSchedule]
	ADD CONSTRAINT [FK_VehicleSchedule_RouteScheduleId]
	FOREIGN KEY (RouteScheduleId)
	REFERENCES [Admin].[RouteSchedule] (Id)
