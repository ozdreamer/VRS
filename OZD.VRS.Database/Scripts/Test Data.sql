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

PRINT N'=============== Inserting the test date ===============';

INSERT INTO [User].[UserCredential] (UserName, Password, Active) VALUES('ozdreamer.it@gmail.com', 'pass1', 1);
INSERT INTO [User].[UserDetail](UserId, FirstName, LastName, DateOfBirth, PrimaryContact, AddressLine1, AddressCity, AddressState, AddressPostCode, AddressCountry, UseAddressAsPostal) VALUES (SCOPE_IDENTITY(), 'Kamrul','Hasan','9/11/1980','0411342791','58 Portree Cres','Heathwood','QLD','4110','Australia',1);
UPDATE [User].[UserCredential] SET UserDetailId = SCOPE_IDENTITY() WHERE UserName = 'ozdreamer.it@gmail.com';
INSERT INTO [User].[UserCredential] (UserName, Password, Active) VALUES('k.h.rajeeb@gmail.com', 'pass2', 1);
INSERT INTO [User].[UserDetail](UserId, FirstName, LastName, DateOfBirth, PrimaryContact, AddressLine1, AddressCity, AddressState, AddressPostCode, AddressCountry, PostalLine1, PostalCity, PostalState, PostalPostCode, PostalCountry, UseAddressAsPostal) VALUES (SCOPE_IDENTITY(), 'S M ','Hasan','9/11/1980','0738799029','39 Harrison Cres','Forest Lake','QLD','4078','Australia','39 Harrison Cres','Forest Lake','QLD','4078','Australia',0);
UPDATE [User].[UserCredential] SET UserDetailId = SCOPE_IDENTITY() WHERE UserName = 'k.h.rajeeb@gmail.com';
INSERT INTO [User].[UserCredential] (UserName, Password, Active) VALUES('rajeeb1000@gmail.com', 'pass3', 0);

INSERT INTO[Admin].[SeatLayout](Rows, Columns, Layout, Active) VALUES(10, 4, 'A1,A2;B1,B2;C1,C2;', 1);
INSERT INTO [Admin].[Vehicle] (SeatLayoutId, VehicleType, Manufacturer, Model, Year, RegistrationState, RegistrationNumber, RegistrationExpiry, TotalSeats, Active) VALUES(SCOPE_IDENTITY(), 'A/C','Volvo','VR-200',2018,'Dhaka Metro','KA 2345','10/10/2023',42,1);
INSERT INTO[Admin].[SeatLayout](Rows, Columns, Layout, Active) VALUES(8, 4, 'A1,A2;B1,B2;C1,C2;', 1);
INSERT INTO [Admin].[Vehicle] (SeatLayoutId, VehicleType, Manufacturer, Model, Year, RegistrationState, RegistrationNumber, RegistrationExpiry, TotalSeats, Active) VALUES(SCOPE_IDENTITY(), 'Non-A/C', 'Toyota','B-1100',2014,'Dhaka Metro','KA 1166','5/11/2020',36,1);

INSERT INTO [Admin].[Destination] (City, State, PostCode, Active) VALUES('Dhaka', 'Dhaka', 1000, 1);
INSERT INTO [Admin].[BookingOffice] (DestinationId, AddressLine1, Area, Email, PrimaryContact, Active) VALUES(SCOPE_IDENTITY(), '151 Mirpur Road', 'Kalyanpur', 'kalyanpur@vrs.com.bd', '01711665877', 1); 
INSERT INTO [Admin].[Destination] (City, State, PostCode, Active) VALUES('Rajshahi', 'Rajshahi', 6000, 1);
INSERT INTO [Admin].[BookingOffice] (DestinationId, AddressLine1, Area, Email, PrimaryContact, Active) VALUES(SCOPE_IDENTITY(), '2/109 Shaheb Bazar Road', 'Rajshahi Sadar', 'rajshahi.sadar@vrs.com.bd', '01711665878', 1); 
INSERT INTO [Admin].[Destination] (City, State, PostCode, Active) VALUES('Chittagong', 'Chittagong', 4000, 0);

DECLARE @FromDestinationId BIGINT;
DECLARE @ToDestinationId BIGINT;
SELECT @FromDestinationId = Id FROM [Admin].[Destination] WHERE City = 'Dhaka';
SELECT @ToDestinationId = Id FROM [Admin].[Destination] WHERE City = 'Rajshahi';

INSERT INTO [Admin].[Operator] (Name, AddressLine1, AddressCity, AddressState, AddressPostCode, AddressCountry, PrimaryContact, PrimaryEmail, Active) VALUES('Greyhound', 'Ann St', 'Brisbane', 'QLD', '4000', 'Australia', '07 1122 3344', 'greyhound@email.com', 1);
INSERT INTO [Admin].[RouteSchedule] (OperatorId, FromDestinationId, ToDestinationId, Day, Time, Active) VALUES(SCOPE_IDENTITY(), @FromDestinationId, @ToDestinationId, 1, '10:30:00', 1);