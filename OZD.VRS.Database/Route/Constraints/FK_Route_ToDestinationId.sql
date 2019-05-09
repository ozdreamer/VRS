ALTER TABLE [Route].[Route]
	ADD CONSTRAINT [FK_Route_ToDestinationId]
	FOREIGN KEY (ToDestinationId)
	REFERENCES [Location].[Destination] (Id)