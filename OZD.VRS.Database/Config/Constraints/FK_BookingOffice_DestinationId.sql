ALTER TABLE [Config].[BookingOffice]
	ADD CONSTRAINT [FK_BookingOffice_DestinationId]
	FOREIGN KEY (DestinationId)
	REFERENCES [Config].[Destination] (Id)
