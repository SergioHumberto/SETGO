CREATE TABLE [dbo].[CategoriaXRuta]
(
	[IdCategoriaXRuta] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdCategoria] INT NOT NULL FOREIGN KEY REFERENCES [Categoria](IdCategoria),
	[IdRuta] INT NOT NULL FOREIGN KEY REFERENCES [Ruta](IdRuta)
)
