ALTER TABLE [Admin].[DropOffPoint]
	ADD CONSTRAINT [FK_DropOffPoint_WaypointId]
	FOREIGN KEY (WaypointId)
	REFERENCES [Admin].[Waypoint] (Id)
