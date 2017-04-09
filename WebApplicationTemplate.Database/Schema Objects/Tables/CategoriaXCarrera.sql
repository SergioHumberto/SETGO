CREATE TABLE [dbo].[CategoriaXCarrera]
(
	[IdCategoriaXCarrera] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdCarrera] INT NULL FOREIGN KEY REFERENCES [Carrera](IdCarrera),
	[IdCategoria] INT NOT NULL FOREIGN KEY REFERENCES [Categoria](IdCategoria)
)
