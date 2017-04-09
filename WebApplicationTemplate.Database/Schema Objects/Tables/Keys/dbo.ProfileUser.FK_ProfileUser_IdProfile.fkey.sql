ALTER TABLE [dbo].[ProfileUser]
	ADD CONSTRAINT [FK_ProfileUser_IdProfile] 
	FOREIGN KEY (IdProfile)
	REFERENCES [Profile] (IdProfile)	

