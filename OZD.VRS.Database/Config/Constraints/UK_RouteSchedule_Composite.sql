ALTER TABLE [Config].[RouteSchedule]
	ADD CONSTRAINT [UK_RouteSchedule_Composite]
	UNIQUE (OperatorId, FromDestinationId, ToDestinationId, Day, Time);
