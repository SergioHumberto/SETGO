﻿CREATE TABLE [dbo].[ConfiguracionResultados]
(
	[IdConfiguracionResultados] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdCarrera]		INT NOT NULL,
	[Numero]		BIT NOT NULL,
	[Paterno]		BIT NOT NULL,
	[Materno]		BIT NOT NULL,
	[Nombres]		BIT NOT NULL,
	[Folio]			BIT NOT NULL,
	[Sexo]			BIT NOT NULL,
	[Categoria]		BIT NOT NULL,
	[Procedencia]	BIT NOT NULL,
	[Equipo]		BIT NOT NULL,
	[Telefono]		BIT NOT NULL,
	[T_Chip]		BIT NOT NULL,
	[T_Oficial]		BIT NOT NULL,
	[Lug_Cat]		BIT NOT NULL,
	[Lug_Rama]		BIT NOT NULL,
	[Vel]			BIT NOT NULL,
	[Lug_Gral]		BIT NOT NULL,
	[Rama]			BIT NOT NULL
)
