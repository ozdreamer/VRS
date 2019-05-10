ALTER TABLE [Config].[RouteSchedule]
	ADD CONSTRAINT [FK_RouteSchedule_ToDestinationId]
	FOREIGN KEY (ToDestinationId)
	REFERENCES [Config].[Destination] (Id)