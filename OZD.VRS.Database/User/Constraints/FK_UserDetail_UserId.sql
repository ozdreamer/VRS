ALTER TABLE [User].[UserDetail]
	ADD CONSTRAINT [FK_UserDetails_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [User].[UserCredential] (Id)
