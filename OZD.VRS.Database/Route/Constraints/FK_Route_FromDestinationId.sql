ALTER TABLE [Route].[Route]
	ADD CONSTRAINT [FK_Route_FromDestinationId]
	FOREIGN KEY (FromDestinationId)
	REFERENCES [Location].[Destination] (Id)
