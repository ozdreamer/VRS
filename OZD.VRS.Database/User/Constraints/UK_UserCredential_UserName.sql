ALTER TABLE [User].[UserCredential]
	ADD CONSTRAINT [UK_UserCredential_UserName]
	UNIQUE (UserName)
