CREATE TABLE [dbo].[CategoriaXRuta]
(
	[IdCategoriaXRuta] INT NOT NULL PRIMARY KEY,
	[IdCategoria] INT NOT NULL FOREIGN KEY REFERENCES [Categoria](IdCategoria),
	[IdRuta] INT NOT NULL FOREIGN KEY REFERENCES [Ruta](IdRuta)
)
