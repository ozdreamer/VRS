ALTER TABLE [Admin].[Route]
	ADD CONSTRAINT [CHK_Route_SameDestinations]
	CHECK (DepartureId <> ArrivalId)
