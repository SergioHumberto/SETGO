CREATE TABLE [dbo].[TipoEquipo]
(
	[IdTipoEquipo] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CantidadParticipantes] INT NOT NULL,
	[Precio] DECIMAL(18,2) NOT NULL, 
    [IdCategoria] INT NOT NULL, 
    CONSTRAINT [FK_TipoEquipo_Categoria] FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria) 
)
