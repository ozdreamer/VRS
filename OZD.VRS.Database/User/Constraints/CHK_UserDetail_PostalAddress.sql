ALTER TABLE [User].[UserDetail]
	ADD CONSTRAINT [CHK_UserDetails_PostalAddress]
	CHECK (UseAddressAsPostal > 0 OR (UseAddressAsPostal = 0 AND PostalLine1 IS NOT NULL AND PostalCity IS NOT NULL AND PostalState IS NOT NULL AND PostalPostCode IS NOT NULL AND PostalCountry IS NOT NULL))
