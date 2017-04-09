CREATE TABLE [dbo].[CategoriaXCarrera]
(
	[IdCategoriaXCarrera] INT NOT NULL PRIMARY KEY,
	[IdCarrera] INT NULL FOREIGN KEY REFERENCES [Carrera](IdCarrera),
	[IdCategoria] INT NOT NULL FOREIGN KEY REFERENCES [Categoria](IdCategoria)
)
