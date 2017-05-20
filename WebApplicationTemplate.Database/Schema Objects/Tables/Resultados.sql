﻿CREATE TABLE [dbo].[Resultados]
(
	[IdResultado]	INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdParticipante] INT NULL CONSTRAINT [FK_Participante_Resultados] FOREIGN KEY REFERENCES [Participante](IdParticipante),
	[IdConfiguracionResultados] INT NOT NULL CONSTRAINT [FK_ConfiguracionResultados_Resultados] FOREIGN KEY REFERENCES [ConfiguracionResultados](IdConfiguracionResultados),
	[Numero]		INT NULL,
	[Paterno]		VARCHAR(50) NULL,
	[Materno]		VARCHAR(50) NULL,
	[Nombres]		VARCHAR(50) NULL,
	[Folio]			INT NULL,
	[Sexo]			VARCHAR(10) NULL,
	[Categoria]		VARCHAR(10) NULL,
	[Procedencia]	VARCHAR(50) NULL,
	[Equipo]		VARCHAR(50) NULL,
	[Telefono]		VARCHAR(20) NULL,
	[T_Chip]		VARCHAR(10) NULL,
	[T_Oficial]		VARCHAR(10) NULL,
	[Lug_Cat]		INT NULL,
	[Lug_Rama]		INT NULL,
	[Vel]			VARCHAR(10) NULL,
	[Lug_Gral]		INT NULL,
	[Rama]			VARCHAR(20) NULL
)
