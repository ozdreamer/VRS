ALTER TABLE [Route].[VehicleRoute]
	ADD CONSTRAINT [FK_VehicleRoute_ToDestinationId]
	FOREIGN KEY (ToDestinationId)
	REFERENCES [Location].[Destination] (Id)