ALTER TABLE [Admin].[RouteSchedule]
	ADD CONSTRAINT [FK_RouteSchedule_FromDestinationId]
	FOREIGN KEY (FromDestinationId)
	REFERENCES [Admin].[Destination] (Id)
