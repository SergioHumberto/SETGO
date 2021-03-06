﻿CREATE TABLE [dbo].[Rama]
(
	[IdRama] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Nombre] VARCHAR(100) NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [IdCarrera] INT NOT NULL, 
    CONSTRAINT [FK_Rama_Carrera] FOREIGN KEY ([IdCarrera]) REFERENCES [Carrera]([IdCarrera])
)
