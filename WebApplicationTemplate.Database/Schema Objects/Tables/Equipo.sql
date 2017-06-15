CREATE TABLE [dbo].[Equipo]
(
	[IdEquipo] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Nombre] VARCHAR(255) NOT NULL,
	[IdTipoEquipo] INT NULL FOREIGN KEY REFERENCES [TipoEquipo](IdTipoEquipo), 
    [EmailsParticipantes] VARCHAR(MAX) NULL, 
    [IdCarrera] INT NULL, 
    [CantidadRegistrados] INT NULL,
	[Guid] UNIQUEIDENTIFIER NOT NULL
)