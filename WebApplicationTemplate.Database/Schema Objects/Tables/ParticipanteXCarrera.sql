CREATE TABLE [dbo].[ParticipanteXCarrera]
(
	[IdParticipanteXCarrera] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdParticipante] INT NULL FOREIGN KEY REFERENCES [Participante](IdParticipante),
	[IdCarrera] INT NULL FOREIGN KEY REFERENCES [Carrera](IdCarrera)
)
