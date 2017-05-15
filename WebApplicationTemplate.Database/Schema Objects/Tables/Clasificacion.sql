CREATE TABLE [dbo].[Clasificacion]
(
	IdClasificacion INT NOT NULL IDENTITY(1,1) CONSTRAINT [PK_Clasificacion_IdClasificacion] PRIMARY KEY
	, Nombre VARCHAR(100) NOT NULL
	, IdCarrera INT NOT NULL CONSTRAINT [FK_Clasificacion_Carrera_IdCarrera] FOREIGN KEY REFERENCES Carrera(IdCarrera)
)