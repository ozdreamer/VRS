ALTER TABLE [Config].[UserCredential]
	ADD CONSTRAINT [FK_UserCredential_UserDetailId]
	FOREIGN KEY (UserDetailId)
	REFERENCES [Config].[UserDetail] (Id)
