CREATE TABLE [dbo].[ClasificacionXParticipante]
(
	IdClasificacionXParticipante INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_ClasificacionXParticipante_IdClasificacionXParticipante PRIMARY KEY

	, IdValorClasificacion INT NOT NULL 
		CONSTRAINT FK_ClasificacionXParticipante_ValorClasificacion_IdValorClasificacion 
		FOREIGN KEY REFERENCES ValorClasificacion(IdValorClasificacion)

	, IdParticipante INT NOT NULL 
		CONSTRAINT FK_ClasificacionXParticipante_Participante_IdParticipante 
		FOREIGN KEY REFERENCES Participante(IdParticipante)
)
