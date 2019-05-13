ALTER TABLE [Admin].[DropOffPoint]
	ADD CONSTRAINT [UK_DropOffPoint_Composite]
	UNIQUE (RouteId, WaypointId)
