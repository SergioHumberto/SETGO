CREATE TABLE [dbo].[User]
(
	[IdUser]		INT NOT NULL IDENTITY,
	[Username]		VARCHAR(50) NOT NULL,
	[Nombre] VARCHAR(50) NOT NULL,
	[ApellidoPaterno] VARCHAR(50) NOT NULL,
	[ApellidoMaterno] VARCHAR(50) NOT NULL,
	[Email]			VARCHAR(MAX) NOT NULL,
	[Password]		VARCHAR(255) NOT NULL,
	[SessionToken]  VARBINARY(50) NULL,
	[IsSuperUser]	BIT NOT NULL DEFAULT 0,
	CONSTRAINT UQ_UserName UNIQUE(Username)
)
