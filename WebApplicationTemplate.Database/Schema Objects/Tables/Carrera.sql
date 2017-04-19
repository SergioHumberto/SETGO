﻿CREATE TABLE [dbo].[Carrera]
(
	[IdCarrera] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Nombre] VARCHAR(100) NOT NULL,
	[Fecha] DATE NOT NULL,
	[Hora] VARCHAR(5) NOT NULL,
	[Precio] DECIMAL(18,2) NOT NULL,
	[CategoriaEvento] VARCHAR(100) NOT NULL,
	[PaginaWeb] VARCHAR(100) NOT NULL,
	[PalabrasClave] VARCHAR(255) NOT NULL,
	[URLMapa] VARCHAR(MAX) NOT NULL,
	[Ubicacion] VARCHAR(255) NOT NULL,
	[ContenidoHtml] VARCHAR(MAX) NOT NULL,
	[PayPalEmail] VARCHAR(MAX) NULL,
	[TokenPaypalTDP] VARCHAR(MAX) NULL,
	[DescripcionPoliticas] VARCHAR(MAX) NULL, 
    [Activo] BIT NOT NULL DEFAULT 1
)
