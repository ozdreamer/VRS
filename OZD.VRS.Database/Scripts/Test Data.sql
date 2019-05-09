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

DECLARE @LastId BIGINT;
INSERT INTO [User].[UserCredential] (UserName, Password) VALUES('ozdreamer.it@gmail.com', 'pass1');
SELECT @LastId = SCOPE_IDENTITY();
INSERT INTO [User].[UserDetail](UserId, FirstName, LastName, DateOfBirth, PrimaryContact, AddressLine1, AddressCity, AddressState, AddressPostCode, AddressCountry, UseAddressAsPostal) VALUES (@LastId, 'Kamrul','Hasan','9/11/1980','0411342791','58 Portree Cres','Heathwood','QLD','4110','Australia',1);
INSERT INTO [User].[UserCredential] (UserName, Password) VALUES('k.h.rajeeb@gmail.com', 'pass2');
SELECT @LastId = SCOPE_IDENTITY();
INSERT INTO [User].[UserDetail](UserId, FirstName, LastName, DateOfBirth, PrimaryContact, AddressLine1, AddressCity, AddressState, AddressPostCode, AddressCountry, PostalLine1, PostalCity, PostalState, PostalPostCode, PostalCountry, UseAddressAsPostal) VALUES (@LastId, 'S M ','Hasan','9/11/1980','0738799029','39 Harrison Cres','Forest Lake','QLD','4078','Australia','39 Harrison Cres','Forest Lake','QLD','4078','Australia',0);
INSERT INTO [User].[UserCredential] (UserName, Password) VALUES('rajeeb1000@gmail.com', 'pass3');

INSERT INTO [Vehicle].[VehicleDetail] (VehicleType, Manufacturer, Model, Year, RegistrationState, RegistrationNumber, RegistrationExpiry, TotalSeats) VALUES('A/C','Volvo','VR-200',2018,'Dhaka Metro','KA 2345','10/10/2023',42);
INSERT INTO [Vehicle].[VehicleDetail] (VehicleType, Manufacturer, Model, Year, RegistrationState, RegistrationNumber, RegistrationExpiry, TotalSeats) VALUES('Standard','Toyota','B-1100',2014,'Dhaka Metro','KA 1166','5/11/2020',36);

INSERT INTO [Location].[Destination] (City, State, PostCode) VALUES('Dhaka', 'Dhaka', 1000);
SELECT @LastId = SCOPE_IDENTITY();
INSERT INTO [Location].[BookingOffice] (DestinationId, AddressLine1, Area, Email, PrimaryContact) VALUES(@LastId, '151 Mirpur Road', 'Kalyanpur', 'kalyanpur@vrs.com.bd', '01711665877'); 
INSERT INTO [Location].[Destination] (City, State, PostCode) VALUES('Rajshahi', 'Rajshahi', 6000);
SELECT @LastId = SCOPE_IDENTITY();
INSERT INTO [Location].[BookingOffice] (DestinationId, AddressLine1, Area, Email, PrimaryContact) VALUES(@LastId, '2/109 Shaheb Bazar Road', 'Rajshahi Sadar', 'rajshahi.sadar@vrs.com.bd', '01711665878'); 
INSERT INTO [Location].[Destination] (City, State, PostCode) VALUES('Chittagong', 'Chittagong', 4000);

DECLARE @OutgoingRouteId BIGINT;
DECLARE @IncomingRouteId BIGINT;
EXEC [Route].[AddRoute] 1, 2, @OutgoingRouteId OUT, @IncomingRouteId OUT;
EXEC [Route].[AddRoute] 1, 3, @OutgoingRouteId OUT, @IncomingRouteId OUT;