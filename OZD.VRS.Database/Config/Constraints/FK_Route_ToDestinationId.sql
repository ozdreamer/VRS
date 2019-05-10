ALTER TABLE [Config].[Route]
	ADD CONSTRAINT [FK_Route_ToDestinationId]
	FOREIGN KEY (ToDestinationId)
	REFERENCES [Config].[Destination] (Id)