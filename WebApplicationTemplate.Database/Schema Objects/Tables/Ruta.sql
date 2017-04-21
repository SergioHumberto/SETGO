CREATE TABLE [dbo].[Ruta]
(
	[IdRuta] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Nombre] VARCHAR(100) NOT NULL,
	[DistanciaKM] DECIMAL(18,2) NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [IdCategoria] INT NOT NULL, 
    CONSTRAINT [FK_Ruta_Categoria] FOREIGN KEY ([IdCategoria]) REFERENCES [Categoria]([IdCategoria])
)
