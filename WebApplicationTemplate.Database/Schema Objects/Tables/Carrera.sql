CREATE TABLE [dbo].[Carrera]
(
	[IdCarrera] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Nombre] VARCHAR(100) NOT NULL,
	[Fecha] DATE NOT NULL,
	[Hora] VARCHAR(5) NULL,
	[Precio] DECIMAL(18,2) NULL,
	[CategoriaEvento] VARCHAR(100) NULL,
	[PaginaWeb] VARCHAR(100) NULL,
	[PalabrasClave] VARCHAR(255) NULL,
	[URLMapa] VARCHAR(MAX) NULL,
	[Ubicacion] VARCHAR(255) NULL,
	[ContenidoHtml] VARCHAR(MAX) NOT NULL,
	[PayPalEmail] VARCHAR(MAX) NULL,
	[TokenPaypalTDP] VARCHAR(MAX) NULL,
	[DescripcionPoliticas] VARCHAR(MAX) NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [CC] VARCHAR(MAX) NULL, 
    [BCC] VARCHAR(MAX) NULL
)
