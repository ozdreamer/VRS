ALTER TABLE [Admin].[PickupPoint]
	ADD CONSTRAINT [FK_PickupPoint_WaypointId]
	FOREIGN KEY (WaypointId)
	REFERENCES [Admin].[Waypoint] (Id)
