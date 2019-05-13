ALTER TABLE [Admin].[PickupPoint]
	ADD CONSTRAINT [FK_PickupPoint_RouteId]
	FOREIGN KEY (RouteId)
	REFERENCES [Admin].[Route] (Id)
