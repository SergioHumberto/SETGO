﻿CREATE TABLE [dbo].[Equipo]
(
	[IdEquipo] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdTipoEquipo] INT NULL FOREIGN KEY REFERENCES [TipoEquipo](IdTipoEquipo)
)
