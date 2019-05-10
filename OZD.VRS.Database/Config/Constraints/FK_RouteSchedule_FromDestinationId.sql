ALTER TABLE [Config].[RouteSchedule]
	ADD CONSTRAINT [FK_RouteSchedule_FromDestinationId]
	FOREIGN KEY (FromDestinationId)
	REFERENCES [Config].[Destination] (Id)
