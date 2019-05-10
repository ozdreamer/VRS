ALTER TABLE [Config].[Vehicle]
	ADD CONSTRAINT [UK_Vehicle_Registration]
	UNIQUE (RegistrationState, RegistrationNumber)
