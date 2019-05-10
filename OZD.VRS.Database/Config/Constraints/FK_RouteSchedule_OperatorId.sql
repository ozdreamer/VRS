ALTER TABLE [Config].[RouteSchedule]
	ADD CONSTRAINT [FK_RouteSchedule_OperatorId]
	FOREIGN KEY (OperatorId)
	REFERENCES [Config].[Operator] (Id)
