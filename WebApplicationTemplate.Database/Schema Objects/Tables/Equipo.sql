CREATE TABLE [dbo].[Equipo]
(
	[IdEquipo] INT NOT NULL PRIMARY KEY,
	[IdTipoEquipo] INT NULL FOREIGN KEY REFERENCES [TipoEquipo](IdTipoEquipo)
)
