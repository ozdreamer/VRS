ALTER TABLE [Config].[Route]
	ADD CONSTRAINT [FK_Route_FromDestinationId]
	FOREIGN KEY (FromDestinationId)
	REFERENCES [Config].[Destination] (Id)
