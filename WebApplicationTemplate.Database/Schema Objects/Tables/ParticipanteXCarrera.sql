CREATE TABLE [dbo].[ParticipanteXCarrera]
(
	[IdParticipanteXCarrera] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdParticipante] INT NULL FOREIGN KEY REFERENCES [Participante](IdParticipante),
	[IdCarrera] INT NULL FOREIGN KEY REFERENCES [Carrera](IdCarrera),
	[IdRama] INT NULL FOREIGN KEY REFERENCES [Rama](IdRama),
	[IdCategoria] INT NULL FOREIGN KEY REFERENCES [Categoria](IdCategoria),
	[IdRuta] INT NULL FOREIGN KEY REFERENCES [Ruta](IdRuta)
)
