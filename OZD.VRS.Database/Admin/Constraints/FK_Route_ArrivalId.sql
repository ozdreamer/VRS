ALTER TABLE [Admin].[Route]
	ADD CONSTRAINT [FK_Route_ArrivalId]
	FOREIGN KEY (ArrivalId)
	REFERENCES [Admin].[Destination] (Id)