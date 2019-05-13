ALTER TABLE [Admin].[Route]
	ADD CONSTRAINT [FK_Route_DepartureId]
	FOREIGN KEY (DepartureId)
	REFERENCES [Admin].[Destination] (Id)
