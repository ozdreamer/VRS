ALTER TABLE [Vehicle].[VehicleDetail]
	ADD CONSTRAINT [UK_Vehicle_Registration]
	UNIQUE (RegistrationState, RegistrationNumber)
