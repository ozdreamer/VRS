ALTER TABLE [Admin].[RouteSchedule]
	ADD CONSTRAINT [CHK_RouteSchedule_SameDestinations]
	CHECK (FromDestinationId <> ToDestinationId)
