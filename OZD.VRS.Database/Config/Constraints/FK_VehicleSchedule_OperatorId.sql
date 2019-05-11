ALTER TABLE [Admin].[VehicleSchedule]
	ADD CONSTRAINT [FK_VehicleSchedule_OperatorId]
	FOREIGN KEY (OperatorId)
	REFERENCES [Admin].[Operator] (Id)