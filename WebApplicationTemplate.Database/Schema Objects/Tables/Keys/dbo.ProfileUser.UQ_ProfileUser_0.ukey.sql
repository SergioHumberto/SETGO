ALTER TABLE [dbo].[ProfileUser]
    ADD CONSTRAINT [UQ_ProfileUser_0]
    UNIQUE (IdProfile, IdUser)