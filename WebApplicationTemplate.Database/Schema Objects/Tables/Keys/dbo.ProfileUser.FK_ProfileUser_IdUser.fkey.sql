ALTER TABLE [dbo].[ProfileUser]
	ADD CONSTRAINT [FK_ProfileUser_IdUser] 
	FOREIGN KEY (IdUser)
	REFERENCES [User] (IdUser)	

