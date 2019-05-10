/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO [Config].[UserCredential] (UserName, Password) VALUES('ozdreamer.it@gmail.com', 'pass1');
INSERT INTO [Config].[UserDetail](UserId, FirstName, LastName, DateOfBirth, PrimaryContact, AddressLine1, AddressCity, AddressState, AddressPostCode, AddressCountry, UseAddressAsPostal) VALUES (SCOPE_IDENTITY(), 'Kamrul','Hasan','9/11/1980','0411342791','58 Portree Cres','Heathwood','QLD','4110','Australia',1);
UPDATE [Config].[UserCredential] SET UserDetailId = SCOPE_IDENTITY() WHERE UserName = 'ozdreamer.it@gmail.com';
INSERT INTO [Config].[UserCredential] (UserName, Password) VALUES('k.h.rajeeb@gmail.com', 'pass2');
INSERT INTO [Config].[UserDetail](UserId, FirstName, LastName, DateOfBirth, PrimaryContact, AddressLine1, AddressCity, AddressState, AddressPostCode, AddressCountry, PostalLine1, PostalCity, PostalState, PostalPostCode, PostalCountry, UseAddressAsPostal) VALUES (SCOPE_IDENTITY(), 'S M ','Hasan','9/11/1980','0738799029','39 Harrison Cres','Forest Lake','QLD','4078','Australia','39 Harrison Cres','Forest Lake','QLD','4078','Australia',0);
UPDATE [Config].[UserCredential] SET UserDetailId = SCOPE_IDENTITY() WHERE UserName = 'k.h.rajeeb@gmail.com';
INSERT INTO [Config].[UserCredential] (UserName, Password) VALUES('rajeeb1000@gmail.com', 'pass3');

INSERT INTO [Config].[Vehicle] (VehicleType, Manufacturer, Model, Year, RegistrationState, RegistrationNumber, RegistrationExpiry, TotalSeats) VALUES('A/C','Volvo','VR-200',2018,'Dhaka Metro','KA 2345','10/10/2023',42);
INSERT INTO [Config].[Vehicle] (VehicleType, Manufacturer, Model, Year, RegistrationState, RegistrationNumber, RegistrationExpiry, TotalSeats) VALUES('Standard','Toyota','B-1100',2014,'Dhaka Metro','KA 1166','5/11/2020',36);

INSERT INTO [Config].[Destination] (City, State, PostCode) VALUES('Dhaka', 'Dhaka', 1000);
INSERT INTO [Config].[BookingOffice] (DestinationId, AddressLine1, Area, Email, PrimaryContact) VALUES(SCOPE_IDENTITY(), '151 Mirpur Road', 'Kalyanpur', 'kalyanpur@vrs.com.bd', '01711665877'); 
INSERT INTO [Config].[Destination] (City, State, PostCode) VALUES('Rajshahi', 'Rajshahi', 6000);
INSERT INTO [Config].[BookingOffice] (DestinationId, AddressLine1, Area, Email, PrimaryContact) VALUES(SCOPE_IDENTITY(), '2/109 Shaheb Bazar Road', 'Rajshahi Sadar', 'rajshahi.sadar@vrs.com.bd', '01711665878'); 
INSERT INTO [Config].[Destination] (City, State, PostCode) VALUES('Chittagong', 'Chittagong', 4000);

DECLARE @FromDestinationId BIGINT;
DECLARE @ToDestinationId BIGINT;
SELECT @FromDestinationId = Id FROM [Config].[Destination] WHERE City = 'Dhaka';
SELECT @ToDestinationId = Id FROM [Config].[Destination] WHERE City = 'Rajshahi';

INSERT INTO [Config].[Operator] (Name, AddressLine1, AddressCity, AddressState, AddressPostCode, AddressCountry, PrimaryContact, PrimaryEmail) VALUES('Greyhound', 'Ann St', 'Brisbane', 'QLD', '4000', 'Australia', '07 1122 3344', 'greyhound@email.com');
INSERT INTO [Config].[RouteSchedule] (OperatorId, FromDestinationId, ToDestinationId, Day, Time) VALUES(SCOPE_IDENTITY(), @FromDestinationId, @ToDestinationId, 'Friday', '10:30:00');