CREATE TABLE [dbo].[ValorClasificacion]
(
	IdValorClasificacion INT NOT NULL IDENTITY(1,1) 
		CONSTRAINT PK_ValorClasificacion_IdValorClasificacion PRIMARY KEY

	, IdClasificacion INT NOT NULL 
		CONSTRAINT FK_ValorClasificacion_Clasificacion_IdClasificacion 
		FOREIGN KEY REFERENCES Clasificacion(IdClasificacion)
	
	, Etiqueta VARCHAR(100) NOT NULL
)