ALTER TABLE [Config].[UserCredential]
	ADD CONSTRAINT [UK_UserCredential_UserName]
	UNIQUE (UserName)
