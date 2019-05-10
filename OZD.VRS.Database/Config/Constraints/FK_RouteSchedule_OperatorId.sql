ALTER TABLE [Admin].[RouteSchedule]
	ADD CONSTRAINT [FK_RouteSchedule_OperatorId]
	FOREIGN KEY (OperatorId)
	REFERENCES [Admin].[Operator] (Id)
