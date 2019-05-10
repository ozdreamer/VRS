CREATE TABLE [Admin].[BookingOffice]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[DestinationId] BIGINT NOT NULL,
	[AddressLine1] NVARCHAR(128) NOT NULL,
	[AddressLine2] NVARCHAR(128),
	[Area] NVARCHAR(128) NOT NULL, 
    [Email] NVARCHAR(128) NOT NULL, 
    [PrimaryContact] NVARCHAR(128) NOT NULL,
	[SecondaryContact] NVARCHAR(128),	
)
