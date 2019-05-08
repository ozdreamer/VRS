CREATE PROCEDURE [Route].[AddVehicleRoute]
	@FromDestinationId BIGINT,
	@ToDestinationId BIGINT,
	@OutgoingRouteId BIGINT OUT,
	@IncomingRouteId BIGINT OUT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM [Route].[VehicleRoute] WHERE FromDestinationId = @FromDestinationId AND ToDestinationId = @ToDestinationId)
	BEGIN
		INSERT INTO [Route].[VehicleRoute] (FromDestinationId, ToDestinationId) VALUES(@FromDestinationId, @ToDestinationId);
		SET @OutgoingRouteId = SCOPE_IDENTITY();
	END

	IF NOT EXISTS (SELECT 1 FROM [Route].[VehicleRoute] WHERE FromDestinationId = @ToDestinationId AND ToDestinationId = @FromDestinationId)
	BEGIN
		INSERT INTO [Route].[VehicleRoute] (FromDestinationId, ToDestinationId) VALUES(@ToDestinationId, @FromDestinationId);
		SET @IncomingRouteId = SCOPE_IDENTITY();
	END
END