CREATE TABLE [dbo].[ConfiguracionResultados]
(
	[IdConfiguracionResultados] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdCarrera] INT NOT NULL,
	[NombreCampo] VARCHAR(20) NOT NULL,
	[Visible] BIT NOT NULL
)
