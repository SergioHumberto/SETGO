CREATE TABLE [dbo].[ControlXCarrera]
(
	[IdControlXCarrera] INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	, [IdControl] INT NOT NULL
	, [IdCarrera] INT NOT NULL
	, [Etiqueta] VARCHAR(255) NOT NULL
	, [Requerido] BIT NOT NULL

	, CONSTRAINT [FK_ControlXCarrera_Control_IdControl] FOREIGN KEY (IdControl) REFERENCES [Control] (IdControl)
	, CONSTRAINT [FK_ControlXCarrera_Carrera_IdCarrera] FOREIGN KEY (IdCarrera) REFERENCES [Carrera] (IdCarrera)
)
