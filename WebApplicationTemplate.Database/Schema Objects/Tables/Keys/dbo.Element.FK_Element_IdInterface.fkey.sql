ALTER TABLE [dbo].[Element]
	ADD CONSTRAINT [FK_Element_IdInterface] 
	FOREIGN KEY (IdInterface)
	REFERENCES Interface(IdInterface)	

