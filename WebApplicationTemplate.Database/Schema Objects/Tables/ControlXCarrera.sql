CREATE TABLE [dbo].[ControlXCarrera]
(
	[IdControlXCarrera] INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	, [IdControl] INT NOT NULL
	, [IdCarrera] INT NOT NULL
	, [Etiqueta] VARCHAR(255) NOT NULL
	, [Requerido] BIT NOT NULL
	, [EtiquetaRequerido] VARCHAR(100) NULL
	, [RegularExpression] BIT NOT NULL CONSTRAINT [DF_ControlXCarrera_RegularExpression] DEFAULT (0)
	, [RegularErrorMessage] VARCHAR(100) NULL
	, [ValidationExpression] VARCHAR(MAX) NULL

	, CONSTRAINT [FK_ControlXCarrera_Control_IdControl] FOREIGN KEY (IdControl) REFERENCES [Control] (IdControl)
	, CONSTRAINT [FK_ControlXCarrera_Carrera_IdCarrera] FOREIGN KEY (IdCarrera) REFERENCES [Carrera] (IdCarrera)
)
