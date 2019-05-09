ALTER TABLE [User].[UserCredential]
	ADD CONSTRAINT [FK_UserCredential_UserDetailId]
	FOREIGN KEY (UserDetailId)
	REFERENCES [User].[UserDetail] (Id)
