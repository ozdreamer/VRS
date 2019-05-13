ALTER TABLE [Admin].[DropOffPoint]
	ADD CONSTRAINT [FK_DropOffPoint_RouteId]
	FOREIGN KEY (RouteId)
	REFERENCES [Admin].[Route] (Id)
