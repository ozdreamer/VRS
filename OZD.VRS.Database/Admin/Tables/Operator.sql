CREATE TABLE [Admin].[Operator]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(128) NOT NULL,
	[AddressLine1] NVARCHAR(255) NOT NULL,
	[AddressLine2] NVARCHAR(255),
	[AddressArea] NVARCHAR(64),
	[AddressCity] NVARCHAR(64) NOT NULL,
	[AddressState] NVARCHAR(64) NOT NULL,
	[AddressPostCode] NVARCHAR(12) NOT NULL,
	[AddressCountry] NVARCHAR(64) NOT NULL,
	[PrimaryContact] NVARCHAR(128) NOT NULL,
	[SecondaryContact] NVARCHAR(128),
	[PrimaryEmail] NVARCHAR(128) NOT NULL,
	[Active] BIT NOT NULL
)
