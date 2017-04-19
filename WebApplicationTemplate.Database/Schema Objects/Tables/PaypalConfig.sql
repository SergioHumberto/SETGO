CREATE TABLE [dbo].[PaypalConfig]
(
	[IdPaypalConfig] INT NOT NULL PRIMARY KEY IDENTITY,
	[PaypalURL] VARCHAR(255) NOT NULL,
	[Descripcion] VARCHAR(255) NOT NULL,
	[Activo] BIT NOT NULL
)
