CREATE TABLE [Vehicle].[VehicleDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[VIN] NVARCHAR(50) NULL, 
	[VehicleType]  NVARCHAR(50) NOT NULL, 
    [Manufacturer] NVARCHAR(128) NOT NULL, 
    [Model] NVARCHAR(50) NULL, 
    [Year] INT NULL, 
    [RegistrationState] NVARCHAR(128) NOT NULL, 
    [RegistrationNumber] NVARCHAR(50) NOT NULL, 
    [RegistrationExpiry] DATETIME NULL, 
    [TotalSeats] INT NULL, 
    [DriveType] NVARCHAR(16) NULL, 
    [BaseStation] NVARCHAR(128) NULL
)