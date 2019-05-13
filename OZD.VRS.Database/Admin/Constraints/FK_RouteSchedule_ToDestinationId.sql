ALTER TABLE [Admin].[RouteSchedule]
	ADD CONSTRAINT [FK_RouteSchedule_ToDestinationId]
	FOREIGN KEY (ToDestinationId)
	REFERENCES [Admin].[Destination] (Id)