CREATE TABLE [dbo].[Resultados]
(
	[IdResultados] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdParticipante] INT NULL FOREIGN KEY REFERENCES [Participante](IdParticipante),
	[Nombres] VARCHAR(100) NOT NULL,
	[ApellidoPaterno] VARCHAR(100) NOT NULL,
	[ApellidoMaterno] VARCHAR(100) NOT NULL,
	[Genero] VARCHAR(1) NULL,
	[Tiempo] VARCHAR(100) NULL,
	[PosicionGeneral] INT NULL,
	[PosicionCategoria] INT NULL,
	[PosicionRama] INT NULL,
	[Velocidad] VARCHAR(100) NULL,
	[Folio] INT NULL,
	[Dorsal] INT NULL,
	[Chip] INT NULL,
	[Grupo] VARCHAR(100) NULL

)
