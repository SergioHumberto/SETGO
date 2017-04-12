CREATE TABLE [dbo].[Participante]
(
	[IdParticipante] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nombre] VARCHAR(50) NOT NULL,
	[ApellidoPaterno] VARCHAR(50) NOT NULL,
	[ApellidoMaterno] VARCHAR(50) NULL,
	[Edad] INT NOT NULL,
	[Domicilio] VARCHAR(255) NOT NULL,
	[Invitado] VARCHAR(2) NOT NULL,
	[NumeroAccion] INT NOT NULL,
	[Telefono] VARCHAR(12) NULL,
	[Email] VARCHAR(100) NULL,
	[TelefonoEmergencia] VARCHAR(12) NULL,
	[IdEquipo] INT NULL  FOREIGN KEY REFERENCES [Equipo](IdEquipo),
	[Pagado] BIT NOT NULL,
	[Socio] VARCHAR(100) NULL
)
