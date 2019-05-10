ALTER TABLE [Config].[UserDetail]
	ADD CONSTRAINT [FK_UserDetails_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [Config].[UserCredential] (Id)
