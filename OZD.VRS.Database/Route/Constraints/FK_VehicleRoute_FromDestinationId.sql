ALTER TABLE [Route].[VehicleRoute]
	ADD CONSTRAINT [FK_VehicleRoute_FromDestinationId]
	FOREIGN KEY (FromDestinationId)
	REFERENCES [Location].[Destination] (Id)
