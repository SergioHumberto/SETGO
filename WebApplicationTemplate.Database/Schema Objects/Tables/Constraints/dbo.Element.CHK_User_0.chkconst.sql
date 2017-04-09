ALTER TABLE [dbo].[Element]
	ADD CONSTRAINT [CHK_User_0] 
	CHECK  (IsSelect = 1 OR IsInsert = 1 OR IsUpdate = 1 OR IsDelete = 1)
