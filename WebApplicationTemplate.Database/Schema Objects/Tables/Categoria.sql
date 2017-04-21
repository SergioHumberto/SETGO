CREATE TABLE [dbo].[Categoria]
(
	[IdCategoria] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Nombre] VARCHAR(50) NOT NULL,
	[Precio] DECIMAL(18,2) NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [IdCarrera] INT NOT NULL, 
    CONSTRAINT [FK_Categoria_Carrera] FOREIGN KEY ([IdCarrera]) REFERENCES [Carrera]([IdCarrera])
)
