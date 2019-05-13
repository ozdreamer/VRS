ALTER TABLE [Admin].[VehicleSchedule]
	ADD CONSTRAINT [FK_VehicleSchedule_VehicleId]
	FOREIGN KEY (VehicleId)
	REFERENCES [Admin].[Vehicle] (Id)
