ALTER TABLE [dbo].[ProfileElement]
    ADD CONSTRAINT [UQ_ProfileElement_0]
    UNIQUE (IdProfile, IdElement)