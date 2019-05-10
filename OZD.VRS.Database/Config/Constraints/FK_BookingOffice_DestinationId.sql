ALTER TABLE [Admin].[BookingOffice]
	ADD CONSTRAINT [FK_BookingOffice_DestinationId]
	FOREIGN KEY (DestinationId)
	REFERENCES [Admin].[Destination] (Id)
