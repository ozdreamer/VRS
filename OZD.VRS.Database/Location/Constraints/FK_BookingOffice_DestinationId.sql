ALTER TABLE [Location].[BookingOffice]
	ADD CONSTRAINT [FK_BookingOffice_DestinationId]
	FOREIGN KEY (DestinationId)
	REFERENCES [Location].[Destination] (Id)
