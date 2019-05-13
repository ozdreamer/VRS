ALTER TABLE [Admin].[PickupPoint]
	ADD CONSTRAINT [UK_PickupPoint_Composite]
	UNIQUE (RouteId, WaypointId)
