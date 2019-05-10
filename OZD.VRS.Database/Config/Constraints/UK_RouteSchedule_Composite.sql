ALTER TABLE [Admin].[RouteSchedule]
	ADD CONSTRAINT [UK_RouteSchedule_Composite]
	UNIQUE (OperatorId, FromDestinationId, ToDestinationId, Day, Time);
